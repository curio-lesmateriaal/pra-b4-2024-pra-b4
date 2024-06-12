using PRA_B4_FOTOKIOSK.magie;
using PRA_B4_FOTOKIOSK.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRA_B4_FOTOKIOSK.controller
{
    public class SearchController
    {
        // De window die we laten zien op het scherm
        public static Home Window { get; set; }

        public List<KioskPhoto> PicturesToDisplay { get; set; } = new List<KioskPhoto>();


        // Start methode die wordt aangeroepen wanneer de zoek pagina opent.
        public void Start()
        {
            SearchManager.Instance = Window;
        }

        // Wordt uitgevoerd wanneer er op de Zoeken knop is geklikt
        public void SearchButtonClick()
        {
            // Verkrijg de zoekinput van de gebruiker
            string found = "";

            DateTime now = DateTime.Now;
            int day = (int)now.DayOfWeek;

            string dir = "";
            if (day == 0) dir = "../../../fotos/0_Zondag";
            else if (day == 1) dir = "../../../fotos/1_Maandag";
            else if (day == 2) dir = "../../../fotos/2_Dinsdag";
            else if (day == 3) dir = "../../../fotos/3_Woensdag";
            else if (day == 4) dir = "../../../fotos/4_Donderdag";
            else if (day == 5) dir = "../../../fotos/5_Vrijdag";
            else if (day == 6) dir = "../../../fotos/6_Zaterdag";

            // loopt door de dag van 'fotos'
            foreach (string photo in Directory.GetFiles(dir))
            {
                string[] search = SearchManager.GetSearchInput().Split("-");

                int hour = int.Parse(search[0]);
                int minute = int.Parse(search[1]);
                int second = int.Parse(search[2]);

                // om de datum te formaten
                string timeDate = string.Format("{0:D2}_{1:D2}_{2:D2}_", hour, minute, second);
                if (photo.Contains(timeDate))
                {
                    found = photo;
                    SearchManager.SetPicture(found);
                    string[] fileInfo = Path.GetFileNameWithoutExtension(photo).Split('_');
                    string imageInfo = $"Tijd: {fileInfo[0]}:{fileInfo[1]}:{fileInfo[2]}\nId: {fileInfo[3]}\nTijd: {DateTime.Today}";
                    SearchManager.SetSearchImageInfo(imageInfo);
                }
            }
        }
    }
}
    


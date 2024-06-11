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

        }

        // Wordt uitgevoerd wanneer er op de Zoeken knop is geklikt
        public void SearchButtonClick()
        {
            // Verkrijg de zoekinput van de gebruiker
            string searchInput = SearchManager.GetSearchInput();
            if (string.IsNullOrEmpty(searchInput))
            {
                // Handle empty input scenario
                return;
            }

            // Parse de input om dag en tijd te krijgen
            string[] inputParts = searchInput.Split(' ');
            if (inputParts.Length < 2)
            {
                // Handle incorrect input format scenario
                return;
            }

            if (!int.TryParse(inputParts[0], out int day) || !DateTime.TryParse(inputParts[1], out DateTime searchTime))
            {
                // Handle invalid input scenario
                return;
            }

            // Definieer de grenswaarden voor de zoekperiode
            DateTime lowerBound = searchTime.AddMinutes(-1);
            DateTime upperBound = searchTime.AddMinutes(1);

            // Pad naar de fotos map
            string photosPath = Path.Combine(Directory.GetCurrentDirectory(), "fotos");

            // Loop door de dagenmappen
            foreach (string dir in Directory.GetDirectories(photosPath))
            {
                string dayFolder = Path.GetFileName(dir);
                if (int.TryParse(dayFolder.Split('_')[0], out int fotoDay) && day == fotoDay)
                {
                    // Loop door de bestanden in de map
                    foreach (string file in Directory.GetFiles(dir))
                    {
                        var fileName = Path.GetFileNameWithoutExtension(file);
                        var parts = fileName.Split('_');

                        // Controleer of de bestandnaam het juiste formaat heeft
                        if (parts.Length >= 3 && int.TryParse(parts[0], out int hour) && int.TryParse(parts[1], out int minute) && int.TryParse(parts[2], out int second))
                        {
                            // Maak een DateTime object van de tijd
                            var fileTime = new DateTime(searchTime.Year, searchTime.Month, searchTime.Day, hour, minute, second);

                            // Controleer of de file tijd binnen de grenswaarden ligt
                            if (fileTime >= lowerBound && fileTime <= upperBound)
                            {
                                KioskPhoto photo = new KioskPhoto() { Id = 0, Source = file };
                                PicturesToDisplay.Add(photo);
                                SearchManager.SetPicture(file);
                            }
                        }
                    }
                }
            }
        }
    }
}

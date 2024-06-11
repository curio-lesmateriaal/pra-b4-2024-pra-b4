using PRA_B4_FOTOKIOSK.magie;
using PRA_B4_FOTOKIOSK.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace PRA_B4_FOTOKIOSK.controller
{
    public class PictureController
    {
        // De window die we laten zien op het scherm
        public static Home Window { get; set; }

        // De lijst met fotos die we laten zien
        public List<KioskPhoto> PicturesToDisplay = new List<KioskPhoto>();

        // Start methode die wordt aangeroepen wanneer de foto pagina opent.
        public void Start()
        {
            // Vandaag
            var now = DateTime.Now;

            int day = (int)now.DayOfWeek;




            var lowerBound = now.AddMinutes(-30); // 30 minuten geleden
            var upperBound = now.AddMinutes(-2); // 2 minuten geleden

            // Initializeer de lijst met fotos
            // WAARSCHUWING. ZONDER FILTER LAADT DIT ALLES!
            // foreach is een for-loop die door een array loopt
            foreach (string dir in Directory.GetDirectories(@"../../../fotos"))
            {
                /**
                 * dir string is de map waar de fotos in staan. Bijvoorbeeld:
                 * \fotos\0_Zondag
                 */
                foreach (char d in dir)
                {
                    if (int.TryParse(d.ToString(), out int fotoDay))
                    {
                        string input = SearchManager.GetSearchInput();
                        SearchManager.SetPicture(input);

                        System.Diagnostics.Debug.WriteLine("\n\n" + fotoDay + "\n\n");
                        if (day == fotoDay)
                        {
                            foreach (string file in Directory.GetFiles(dir))
                            {
                                /**
                                 * file string is de file van de foto. Bijvoorbeeld:
                                 * \fotos\0_Zondag\10_05_30_id8824.jpg
                                 */
                                var fileName = Path.GetFileNameWithoutExtension(file);
                                var parts = fileName.Split('_');


                                // Controleer of de file tijd binnen de grenswaarden ligt
                                //PicturesToDisplay.Add(new KioskPhoto() { Id = 0, Source = file });




                                // dit haalt de tijd van de bestandnaam
                                var hour = int.Parse(parts[0]);
                                var minute = int.Parse(parts[1]);
                                var second = int.Parse(parts[2]);

                                // Maak een DateTime object van de tijd
                                var fileTime = new DateTime(now.Year, now.Month, now.Day, hour, minute, second);

                                // Controleer of de file tijd binnen de grenswaarden ligt
                                if (fileTime >= lowerBound && fileTime <= upperBound)
                                {
                                    KioskPhoto photo = new KioskPhoto() { Id = 0, Source = file };

                                    PicturesToDisplay.Add(photo);
                                    AddPhotoIfWithinOneMinute(photo);
                                }





                            }
                        }
                    }
                }
            }

            // Update de fotos
            PictureManager.UpdatePictures(PicturesToDisplay);
        }

        // Wordt uitgevoerd wanneer er op de Refresh knop is geklikt
        public void RefreshButtonClick()
        {

        }

        public void AddPhotoIfWithinOneMinute(KioskPhoto newPhoto)
        {
            if (!PicturesToDisplay.Contains(newPhoto))
            {
                foreach (var existingPhoto in PicturesToDisplay)
                {
                    if (existingPhoto.Created.AddMinutes(1) == newPhoto.Created)
                    {
                        PicturesToDisplay.Add(newPhoto);
                        break;
                    }
                }
            }
        }
    }
}

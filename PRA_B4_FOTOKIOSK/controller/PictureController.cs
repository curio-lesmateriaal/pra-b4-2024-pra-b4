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
                                PicturesToDisplay.Add(new KioskPhoto() { Id = 0, Source = file });
                                
                                /*
                                 * Mert dit werk niet man
                                 * 
                                // Controleer of de file naam het juiste formaat heeft
                                // Maak een DateTime object van de file tijd
                                DateTime fileTime = new DateTime(now.Year, now.Month, now.Day, hour, minute, second);
                                if (parts.Length >= 4 && int.TryParse(parts[1], out int hour) &&
                                    int.TryParse(parts[2], out int minute) && int.TryParse(parts[3], out int second))
                                {

                                }
                                */
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
    }
}

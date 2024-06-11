using PRA_B4_FOTOKIOSK.magie;
using PRA_B4_FOTOKIOSK.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Media;

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


            //lowerbound en upperbound zijn tijd die ingesteld zijn om terug te kijken


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
                                //PicturesToDisplay.Add(new KioskPhoto() { Id = 0, Source = file });




                                // dit haalt de tijd van de bestandnaam
                                var hour = int.Parse(parts[0]);
                                var minute = int.Parse(parts[1]);
                                var second = int.Parse(parts[2]);

                                //int.parse zorgt ervoor dat de tijd omgezet word naar integers en dit heb ik gebruikt om de tijdwaarden naar uur minuut en seconden te zetten



                                // Maak een DateTime object van de tijd
                                var fileTime = new DateTime(now.Year, now.Month, now.Day, hour, minute, second);

                                //Hier wordt een DateTime object gemaakt voor vandaag, met de tijd uit het bestand.dit zorg ervoor dat we de tijd van nu pakken

                                // Controleer of de file tijd binnen de grenswaarden ligt
                                if (fileTime >= lowerBound && fileTime <= upperBound)
                                {
                                    KioskPhoto photo = new KioskPhoto() { Id = 0, Source = file };

                                    PicturesToDisplay.Add(photo);
                                    AddPhotoIfWithinOneMinute(photo);
                                }

                                //deze if statement zorg ervoor dat het tijd binnen het grens valt en zo ja moet het dit uitvoeren



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
//deze method zorg ervoor dat je fotos die 1 minuut geleden zijn genomen door de zelfde persoon nog een keer in beeld komt 


//Deze methode controleert of de nieuwe foto  binnen één minuut van een bestaande foto  is gemaakt. Als dat zo is, wordt de nieuwe foto toegevoegd aan de lijst PicturesToDisplay.

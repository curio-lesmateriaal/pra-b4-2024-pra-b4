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
    public class ShopController
    {

        public static Home Window { get; set; }

        public void Start()
        {
            // Stel de prijslijst in aan de rechter kant.
            ShopManager.SetShopPriceList("Prijzen:\n");

            // Stel de bon in onderaan het scherm
            ShopManager.SetShopReceipt("Eindbedrag\n€");

            // Vul de productlijst met producten
            ShopManager.Products.Add(new KioskProduct() { Name = "Foto 10x15" , Price = 2.55f });
            ShopManager.Products.Add(new KioskProduct() { Name = "Foto 20x30" , Price = 4.59f });
            ShopManager.Products.Add(new KioskProduct() { Name = "Mok met Foto" , Price = 9.95f });
            ShopManager.Products.Add(new KioskProduct() { Name = "Sleutelhanger" , Price = 6.12f });
            ShopManager.Products.Add(new KioskProduct() { Name = "T-Shirt" ,  Price = 11.99f });
            // Update dropdown met producten
            ShopManager.UpdateDropDownProducts();
                foreach (KioskProduct product in ShopManager.Products)
                {
                    
                }
        }

        // Wordt uitgevoerd wanneer er op de Toevoegen knop is geklikt
        public void AddButtonClick()
        {
            
        }

        // Wordt uitgevoerd wanneer er op de Resetten knop is geklikt
        public void ResetButtonClick()
        {

        }

        // Wordt uitgevoerd wanneer er op de Save knop is geklikt
        public void SaveButtonClick()
        {

        }

    }
}

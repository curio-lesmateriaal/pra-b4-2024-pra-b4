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
            ShopManager.Products.Add(new KioskProduct() { Name = "Foto 10x15", Price = 2.55f });
            ShopManager.Products.Add(new KioskProduct() { Name = "Foto 20x30", Price = 4.59f });
            ShopManager.Products.Add(new KioskProduct() { Name = "Mok met Foto", Price = 9.95f });
            ShopManager.Products.Add(new KioskProduct() { Name = "Sleutelhanger", Price = 6.12f });
            ShopManager.Products.Add(new KioskProduct() { Name = "T-Shirt", Price = 11.99f });
            // Update dropdown met producten
            ShopManager.UpdateDropDownProducts();
            foreach (KioskProduct product in ShopManager.Products)
            {
                ShopManager.AddShopPriceList(product.Name + " : " + product.Price + "\n");
            }
        }

        // Wordt uitgevoerd wanneer er op de Toevoegen knop is geklikt
        public void AddButtonClick()
        {
            int? fotoId = ShopManager.GetFotoId();
            int? amount = ShopManager.GetAmount();
            KioskProduct product = ShopManager.GetSelectedProduct();

            ShopManager.OrderedProducts.Add(new OrderedProduct() { FotoID = fotoId, ProductName = product.Name, Amount = amount, TotalPrice = product.Price * (float)amount });

            // voor narek pro opdracht

            float totalPrice = 0;
            foreach (OrderedProduct op in ShopManager.OrderedProducts)
            {
                totalPrice += op.TotalPrice;
            }

            ShopManager.SetShopReceipt("Eindbedrag\n€" + totalPrice.ToString() + "\n\n\n");

            foreach (OrderedProduct orderedProduct in ShopManager.OrderedProducts)
            {
                ShopManager.AddShopReceipt("FotoNummer: " + orderedProduct.FotoID + "\n");
                ShopManager.AddShopReceipt("Product Naam: " + orderedProduct.ProductName + "\n");
                ShopManager.AddShopReceipt("Aantal: " + orderedProduct.Amount + "\n");
                ShopManager.AddShopReceipt("totale bedrag: " + orderedProduct.TotalPrice + "\n\n");
            }
            //Deze pad moet je zelf veranderen naar eigen directory! ANDERS WERKT DIE NIET!
            using (StreamWriter writetext = new StreamWriter("F:\\laragon\\www\\pra-b4-2024-pra-b4\\PRA_B4_FOTOKIOSK\\models\\receipt.txt"))
            {
                foreach (OrderedProduct orderedProduct in ShopManager.OrderedProducts)
                {

                    writetext.WriteLine("Product Naam: " + orderedProduct.ProductName);
                    writetext.WriteLine("Foto Nummer: " + orderedProduct.FotoID);
                    writetext.WriteLine("Aantal: " + orderedProduct.Amount);
                    writetext.WriteLine("totale bedrag: " + orderedProduct.TotalPrice + "\n\n");

                }
            }

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

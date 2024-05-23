using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRA_B4_FOTOKIOSK.models
{
    public class KioskProduct
    {

        public string Name { get; set; }

        public float Price { get; set; } 
        
        public string Description { get; set; }

        public KioskProduct() { }
    }
}

/*
 * Als klant wil ik een prijslijst hebben, en een dropdown in het formulier. Met kloppende prijzen en beschrijvingen

o Bewerk de KioskProduct Class. Zodat deze een prijs en beschrijving heeft.
o Loop door middel van een foreach loop door de lijst met producten. En voeg deze toe
aan de prijslijst.
 foreach (KioskProduct product in ShopManager.Products)
 ShopManager.SetShopPriceList(“string”);
 ShopManager.AddShopPriceList(“string”);
 ShopManager.GetShopPriceList(“string”);*/

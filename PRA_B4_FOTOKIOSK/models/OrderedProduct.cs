using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRA_B4_FOTOKIOSK.models
{
    public class OrderedProduct
    {
        public static int FotoID {  get; set; }
        public static string ProductName { get; set; }
        public static int Amount { get; set; }
        public static float TotalPrice { get; set; }
    }
}

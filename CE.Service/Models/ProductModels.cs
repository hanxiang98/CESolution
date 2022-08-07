using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CE.Service.Models
{
    public class ProductSoldModel
    {
        public string ProductName { get; set; }
        public string MerchantProductNo { get; set; }
        public string GTIN { get; set; }
        public int TotalQuantity { get; set; }
        public int Stock { get; set; }
    }


}

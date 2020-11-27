using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace BeTechTestwork
{
    public partial class Product
    {
        [Key]
        public string BarcodeNumber { get; set; }
        public string Name { get; set; }
        public int? BaseCurrencyPrice { get; set; }
        public float? PriceEur { get; set; }
        public float? PriceUah { get; set; }
        public string ProdCategory { get; set; }
        public string Currency { get; set; }
    }
}

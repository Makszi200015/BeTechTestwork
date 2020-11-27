using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace BeTechTestwork
{
    public partial class WarehouseProduct
    {
        [Key]
        public string id { get; set; }
        public string WarehouseName { get; set; }
        public string ProdBarcodeNumber { get; set; }
        public int? Count { get; set; }
    }
}

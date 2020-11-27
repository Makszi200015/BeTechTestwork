using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace BeTechTestwork
{
    public partial class Warehouse
    {
        [Key]
        public string WarehouseName { get; set; }
        public string Address { get; set; }
    }
}

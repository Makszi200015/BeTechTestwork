using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace BeTechTestwork
{
    public partial class Currency
    {
        [Key]
        public string CurrencyName { get; set; }
        public string Code { get; set; }
        public float Course { get; set; }
        public string UpdateDate { get; set; }
    }
}

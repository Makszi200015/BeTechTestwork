using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace BeTechTestwork
{
    public partial class Category
    {
        [Key]
        public string ProdCategory { get; set; }
    }
}

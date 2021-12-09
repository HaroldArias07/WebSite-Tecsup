using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WEBSiteTecsup.Models
{
    public class Gaseosas
    {
        [Key]
        public int GaseosaID { get; set; }
        [StringLength(100)]
        public string GaseosaNombre { get; set; }
        [StringLength(100)]
        public string GaseosaMarca { get; set; }
    }
}
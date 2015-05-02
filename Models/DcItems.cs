using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EWC.Models
{
    [Table("DcItems")]
    public class DcItems
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int DcItemsID { get; set; }
        public String Partno { get; set; }
        public bool scrapreturn { get; set; }
        public int DcId { get; set; }
        public String Desc { get; set; }
        public String Material { get; set; }
        public Double Quantity { get; set; }
    }
}
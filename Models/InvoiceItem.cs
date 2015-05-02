using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EWC.Models
{
    [Table("InvoiceItems")]
    public class InvoiceItem
    {
        public InvoiceItem() { }
        public InvoiceItem(DcItems di) {
            this.Material = di.Material;
            this.Partno = di.Partno;
            this.Quantity = di.Quantity;
            this.Desc = di.Desc;
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int InvoiceItemID { get; set; }
        public String Partno { get; set; }
        public int InvoiceId { get; set; }
        public String Desc { get; set; }
        public String Material { get; set; }
        [Required()]
        public Double Quantity { get; set; }
        [Required()]
        public Double UnitPrice { get; set; }

    }
}
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;
using System.ComponentModel;

namespace EWC.Models
{
    [Table("WayBill")]
    public class WayBill
    {
        public WayBill()
        {
            this.Items = new List<DcItems>();
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int DeliveryChalanID { get; set; }

        public DateTime Date { get; set; }
        public String DCNo { get; set; }
        [Required(ErrorMessage = "Service Tax required")]
        [DisplayName("Service Tax")]
        public String ServiceTax { get; set; }
        [Required(ErrorMessage = "TIN No Required")]
        [DisplayName("TIN No.")]
        public String TinNo { get; set; }
        [Required(ErrorMessage = "IncomeTax PAN No required")]
        [DisplayName("IT PAN/GIR No")]
        public String IncomeTaxPan { get; set; }
        public Customer customer { get; set; }
        public List<DcItems> Items { get; set; }
        [Column("isActive")]
        [DefaultValue(1)]
        public Boolean IsActive{ get; set; }
        [DefaultValue(1)]
        public int Company { get; set; }

        [DefaultValue(false)]
        public bool invoiceCreated { get; set; }

    }
}
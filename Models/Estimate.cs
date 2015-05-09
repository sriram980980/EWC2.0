using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EWC.Models
{
    [Table("Estimate")]
    public class Estimate
    {
        
        public Estimate() {
            this.Items = new List<InvoiceItem>();
        }
        public Estimate(WayBill dc)
        {
            this.Date = dc.Date;
            this.DCNo = dc.DCNo;
            this.DeliveryChalanID = dc.DeliveryChalanID;
            this.IncomeTaxPan = dc.IncomeTaxPan;
            this.isActive = true;
            this.TinNo = dc.TinNo;
            this.ServiceTax = dc.ServiceTax;
            this.Items = new List<InvoiceItem>();
            this.Company = dc.Company;
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
        public List<InvoiceItem> Items { get; set; }
        public Double Educess { get; set; }
        public Double SheCess { get; set; }
        public Double ServiceTaxAmt { get; set; }
        public bool isActive { get; set; }
        public String addedby { get; set; }
        public bool fullypaid { get; set; }
        public Double paidamount { get; set; }
        public Double totalamount { get; set; }
        public String paymode { get; set; }
        public Double labourcharges { get; set; }
        [DefaultValue(1)]
        public int Company { get; set; }
        public Double getTotal()
        {
            Double tot = 0;
            foreach (InvoiceItem i in Items)
            {
                tot += (i.Quantity * i.UnitPrice);
            }
            tot = tot + ((ServiceTaxAmt * tot) / 100) + ((Educess * tot) / 100) + ((SheCess * tot) / 100);
            return tot;
        }
    }
}
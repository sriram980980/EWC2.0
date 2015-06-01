using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EWC.Models
{
    public class DCDBset:DbContext
    {
        public DCDBset() : base("MyConnection") { 
        }
        public DbSet<Estimate> estimates { get; set; }
        public DbSet<WayBill> waybills { get; set; }
        public DbSet<DeliveryChalan> deliveryChalans { get; set; }
        public DbSet<OdeliveryChalan> odeliveryChalans { get; set; }
        public DbSet<Oinvoice> oInvoices { get; set; }
        public DbSet<Customer> customers { get; set; }
        public DbSet<CustomerDb> customersdb { get; set; }
        public DbSet<DcItems> dcItems { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }
        public DbSet<Quotation> Quotations { get; set; }
    }
}
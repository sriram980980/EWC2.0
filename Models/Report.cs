using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EWC.Models
{
    public class Report
    {
        public List<Invoice> invoices;
        public List<Estimate> estimates;
        public List<Oinvoice> oldInvoice;
        public DateTime start;
        public DateTime end;
    }
}
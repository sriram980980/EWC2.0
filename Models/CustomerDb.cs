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
    [Table("CustomerDb")]
    public class CustomerDb
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int CustomerID { get; set; }
        [DisplayName("Name of Company")]
        [Required]
        public String NameofCompany { get; set; }
        [DisplayName("Inward Dc No")]
        public String InwardDcNo { get; set; }
        [Required]
        public String Address { get; set; }
        public DateTime Dated { get; set; }
        [DisplayName("Customer TIN")]
        public String CustomerTin { get; set; }
        [DisplayName("Contact Person")]
        public String ContactPerson { get; set; }
        [DisplayName("Work Order No")]
        public String CustomerWorkOrderNo { get; set; }
        public String ContactNo { get; set; }
        public String ContactNo2 { get; set; }
        public String ContactNo3 { get; set; }
    }
}
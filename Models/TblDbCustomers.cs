using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerMaster.Models
{
    [Table("TblDb_Customers")]
    public partial class TblDbCustomers
    {
        [Key]
        [Display(Name = "ID")]
        public int Id { get; private set; }

        public int AcctNumber { get; private set; }
        public string CustName { get; private set; }
        public string CustBillAddress { get; private set; }
        public string CustBillAddr2 { get; private set; }
        public string CustBillCity { get; private set; }
        public string CustBillState { get; private set; }
        public string CustBillZipCode { get; private set; }
        public string CustContact { get; private set; }
        public string CustPhone { get; private set; }
        public string CustFax { get; private set; }
        public string CustPlant { get; private set; }
        public string CustAlpha { get; private set; }
        public string OutsideRep { get; private set; }
        public int? CompanyId { get; private set; }
        public string CustomerTerms { get; private set; }
        public int? CustActive { get; private set; }
        public string InsideRep { get; private set; }
        public int? ShipToCheck { get; private set; }
        public bool? Deleted { get; private set; }
        public DateTime? UpdatedDateTime { get; private set; }
        public string UpdatedUser { get; private set; }
        public decimal? AllowedOverRunPercentage { get; private set; }
        public string BillToEmailAddress { get; private set; }
        public int? MasterAcctNumber { get; private set; }
    }
}

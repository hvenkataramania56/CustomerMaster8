using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerMaster.Models
{
    [Table("TblDb_Customers_ShipTo")]
    public partial class TblDbCustomersShipTo
    {
        [Key]
        [Display(Name = "ID")]
        public int Id { get; private set; }

        public int? AcctNumber { get; private set; }
        public string CustId { get; private set; }
        public string CustContact { get; private set; }
        public string CustPhone { get; private set; }
        public string CustFax { get; private set; }
        public string CustPlant { get; private set; }
        public int? CompanyId { get; private set; }
        public string InsideRep { get; private set; }
        public string CustShipName { get; private set; }
        public string CustName { get; private set; }
        public string CustShipAddress { get; private set; }
        public string CustShipAddr2 { get; private set; }
        public string CustShipCity { get; private set; }
        public string CustShipState { get; private set; }
        public string CustShipZipCode { get; private set; }
        public string CustAlpha { get; private set; }
        public string OutsideRep { get; private set; }
        public int? CustShipActive { get; private set; }
        public bool? Deleted { get; private set; }
        public DateTime? UpdatedDateTime { get; private set; }
        public string UpdatedUser { get; private set; }
    }
}

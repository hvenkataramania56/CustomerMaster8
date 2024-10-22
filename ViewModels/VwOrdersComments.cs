using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CustomerMaster.ViewModels
{
    public class VwOrdersComments
    {
        public int Id { get; set; }

        [Display(Name = "Shipping Comment")]
        public string Comment { get; set; }

        [Display(Name = "Created Date")]
        public DateTime? CreatedDate { get; set; }

        [Display(Name = "Company Id")]
        public int? CompanyId { get; set; }

        [Display(Name = "Created User")]
        public string CreatedUser { get; set; }

        [Display(Name = "Created Workstation")]
        public string CreatedWorkStation { get; set; }

        [Display(Name = "Acct#")]
        public int? AcctNumber { get; set; }

        [Display(Name = "Ship to Customer Id")]
        public string CustId { get; set; }

        [Display(Name = "Updated User")]
        public string UpdatedUser { get; set; }

        [Display(Name = "Updated Date")]
        public string UpdatedDateTime { get; set; }

        [Display(Name = "Bill to Cust Alpha")]
        public string BillCustAlpha { get; set; }
        public string BillCustName { get; set; }
        public string CustBillAddress { get; set; }
        public string CustBillAddr2 { get; set; }
        public string CustBillCity { get; set; }
        public string CustBillState { get; set; }
        public string CustBillZipCode { get; set; }
        public bool CustBillActive { get; set; }
        public string CustBillContact { get; set; }
        public string CustBillPlant { get; set; }
        public string CustBillPhone { get; set; }
        public string CustBillFax { get; set; }
        public string BillInsideRep { get; set; }
        public string BillOutsideRep { get; set; }
        public string BillCustomerTerms { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}")]
        public decimal? BillAllowedOverRunPercentage { get; set; }

        public string BillToEmailAddress { get; set; }
        public int? BillMasterAcctNumber { get; set; }
        public string BillUpdatedUser { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy hh:mm tt}")]
        public DateTime? BillUpdatedDateTime { get; set; }
        public bool ShipToSameAsBillTo { get; set; }
        public int TblDbCustomersShipToId { get; set; }
        public bool ShipCustActive { get; set; }
        public string ShipCustAlpha { get; set; }
        public string CustShipName { get; set; }
        public string CustShipAddress { get; set; }
        public string CustShipAddr2 { get; set; }
        public string CustShipCity { get; set; }
        public string CustShipState { get; set; }
        public string CustShipZipCode { get; set; }
        public string CustShipContact { get; set; }
        public string CustShipPlant { get; set; }
        public string CustShipPhone { get; set; }
        public string CustShipFax { get; set; }
        public string ShipInsideRep { get; set; }
        public string ShipOutsideRep { get; set; }
        public string ShipUpdatedUser { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy hh:mm tt}")]
        public DateTime? ShipUpdatedDateTime { get; set; }
    }
}

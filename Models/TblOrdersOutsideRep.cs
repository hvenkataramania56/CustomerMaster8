using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerMaster.Models
{
    [Table("tblOrders_OutsideRep")]
    public partial class TblOrdersOutsideRep
    {
        [Key]
        [Display(Name = "ID")]
        public int Id { get; private set; }

        public string Initials { get; private set; }
        public string RepName { get; private set; }
        public int? CompanyId { get; private set; }
        public bool? Deleted { get; private set; }
    }
}

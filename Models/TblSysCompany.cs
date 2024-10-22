using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerMaster.Models
{
    [Table("TblSys_Company")]
    public class TblSysCompany
    {
        [Key]
        [Display(Name = "Company Id")]
        public int CompanyId { get; private set; }

        [Display(Name = "Company Name")]
        public string CompanyName { get; private set; }
    }
}

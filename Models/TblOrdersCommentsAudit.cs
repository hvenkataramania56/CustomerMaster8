using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerMaster.Models
{
    [Table("TblOrders_CommentsAudit")]
    public class TblOrdersCommentsAudit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TblOrdersCommentsAuditID { get; set; }

        [StringLength(50)]
        public string AuditOperation { get; set; }

        [StringLength(50)]
        public string AuditUser { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime AuditDateTime { get; set; }

        [Display(Name = "Comment Id")]
        public int Id { get; set; }

        [Display(Name = "Shipping Comment")]
        [StringLength(250)]
        [Column(TypeName = "varchar(250)")]
        public string Comment { get; set; }

        [Display(Name = "Created Date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:d}")]
        [Column("Created_Date", TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }

        [Display(Name = "Company Id")]
        public int? CompanyId { get; set; }

        [Display(Name = "Created User")]
        [Column("CreatedUser", TypeName = "varchar(50)")]
        public string CreatedUser { get; set; }

        [Display(Name = "Created Workstation")]
        [Column("CreatedWorkStation", TypeName = "varchar(50)")]
        public string CreatedWorkStation { get; set; }

        [Display(Name = "Acct#")]
        [Column("AcctNumber", TypeName = "int")]
        public int? AcctNumber { get; set; }

        [Display(Name = "Ship to Customer Id")]
        [Column("CustId", TypeName = "varchar(50)")]
        public string CustId { get; set; }

        [Display(Name = "Updated User")]
        [Column("UpdatedUser", TypeName = "varchar(50)")]
        public string UpdatedUser { get; set; }

        [Display(Name = "Updated Date")]
        [Column("UpdatedDateTime", TypeName = "varchar(50)")]
        public string UpdatedDateTime { get; set; }
    }
}

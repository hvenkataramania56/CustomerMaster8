using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CustomerMaster.Models
{
    public class CustomerMasterContext : DbContext
    {
        public CustomerMasterContext (DbContextOptions<CustomerMasterContext> options)
            : base(options)
        {
        }

        public DbSet<CustomerMaster.Models.TblOrdersComments> TblOrdersComments { get; set; }
        public DbSet<CustomerMaster.Models.TblSysCompany> TblSysCompany { get; set; }
        public DbSet<CustomerMaster.Models.TblDbCustomers> TblDbCustomers { get; set; }
        public DbSet<CustomerMaster.Models.TblDbCustomersShipTo> TblDbCustomersShipTo { get; set; }
        public DbSet<CustomerMaster.Models.TblOrdersInsideRep> TblOrdersInsideRep { get; set; }
        public DbSet<CustomerMaster.Models.TblOrdersOutsideRep> TblOrdersOutsideRep { get; set; }
        public DbSet<CustomerMaster.Models.TblOrdersCommentsAudit> TblOrdersCommentsAudit { get; set; }
    }
}

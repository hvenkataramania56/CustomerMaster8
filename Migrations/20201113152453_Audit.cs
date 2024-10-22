using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CustomerMaster.Migrations
{
    public partial class Audit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "TblDb_Customers",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        AcctNumber = table.Column<int>(nullable: false),
            //        CustName = table.Column<string>(nullable: true),
            //        CustBillAddress = table.Column<string>(nullable: true),
            //        CustBillAddr2 = table.Column<string>(nullable: true),
            //        CustBillCity = table.Column<string>(nullable: true),
            //        CustBillState = table.Column<string>(nullable: true),
            //        CustBillZipCode = table.Column<string>(nullable: true),
            //        CustContact = table.Column<string>(nullable: true),
            //        CustPhone = table.Column<string>(nullable: true),
            //        CustFax = table.Column<string>(nullable: true),
            //        CustPlant = table.Column<string>(nullable: true),
            //        CustAlpha = table.Column<string>(nullable: true),
            //        OutsideRep = table.Column<string>(nullable: true),
            //        CompanyId = table.Column<int>(nullable: true),
            //        CustomerTerms = table.Column<string>(nullable: true),
            //        CustActive = table.Column<int>(nullable: true),
            //        InsideRep = table.Column<string>(nullable: true),
            //        ShipToCheck = table.Column<int>(nullable: true),
            //        Deleted = table.Column<bool>(nullable: true),
            //        UpdatedDateTime = table.Column<DateTime>(nullable: true),
            //        UpdatedUser = table.Column<string>(nullable: true),
            //        AllowedOverRunPercentage = table.Column<decimal>(nullable: true),
            //        BillToEmailAddress = table.Column<string>(nullable: true),
            //        MasterAcctNumber = table.Column<int>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_TblDb_Customers", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "TblDb_Customers_ShipTo",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        AcctNumber = table.Column<int>(nullable: true),
            //        CustId = table.Column<string>(nullable: true),
            //        CustContact = table.Column<string>(nullable: true),
            //        CustPhone = table.Column<string>(nullable: true),
            //        CustFax = table.Column<string>(nullable: true),
            //        CustPlant = table.Column<string>(nullable: true),
            //        CompanyId = table.Column<int>(nullable: true),
            //        InsideRep = table.Column<string>(nullable: true),
            //        CustShipName = table.Column<string>(nullable: true),
            //        CustName = table.Column<string>(nullable: true),
            //        CustShipAddress = table.Column<string>(nullable: true),
            //        CustShipAddr2 = table.Column<string>(nullable: true),
            //        CustShipCity = table.Column<string>(nullable: true),
            //        CustShipState = table.Column<string>(nullable: true),
            //        CustShipZipCode = table.Column<string>(nullable: true),
            //        CustAlpha = table.Column<string>(nullable: true),
            //        OutsideRep = table.Column<string>(nullable: true),
            //        CustShipActive = table.Column<int>(nullable: true),
            //        Deleted = table.Column<bool>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_TblDb_Customers_ShipTo", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "TblOrders_Comments",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        Comment = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
            //        Created_Date = table.Column<DateTime>(type: "datetime", nullable: true),
            //        CompanyId = table.Column<int>(nullable: true),
            //        CreatedUser = table.Column<string>(type: "varchar(50)", nullable: true),
            //        CreatedWorkStation = table.Column<string>(type: "varchar(50)", nullable: true),
            //        AcctNumber = table.Column<int>(type: "int", nullable: true),
            //        CustId = table.Column<string>(type: "varchar(50)", nullable: true),
            //        UpdatedUser = table.Column<string>(type: "varchar(50)", nullable: true),
            //        UpdatedDateTime = table.Column<string>(type: "varchar(50)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_TblOrders_Comments", x => x.Id);
            //    });

            migrationBuilder.CreateTable(
                name: "TblOrders_CommentsAudit",
                columns: table => new
                {
                    TblOrdersCommentsAuditID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuditOperation = table.Column<string>(maxLength: 50, nullable: true),
                    AuditUser = table.Column<string>(maxLength: 50, nullable: true),
                    AuditDateTime = table.Column<DateTime>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true),
                    Created_Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    CompanyId = table.Column<int>(nullable: true),
                    CreatedUser = table.Column<string>(type: "varchar(50)", nullable: true),
                    CreatedWorkStation = table.Column<string>(type: "varchar(50)", nullable: true),
                    AcctNumber = table.Column<int>(type: "int", nullable: true),
                    CustId = table.Column<string>(type: "varchar(50)", nullable: true),
                    UpdatedUser = table.Column<string>(type: "varchar(50)", nullable: true),
                    UpdatedDateTime = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblOrders_CommentsAudit", x => x.TblOrdersCommentsAuditID);
                });

            //migrationBuilder.CreateTable(
            //    name: "tblOrders_InsideRep",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        Initials = table.Column<string>(nullable: true),
            //        RepName = table.Column<string>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_tblOrders_InsideRep", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "tblOrders_OutsideRep",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        Initials = table.Column<string>(nullable: true),
            //        RepName = table.Column<string>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_tblOrders_OutsideRep", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "TblSys_Company",
            //    columns: table => new
            //    {
            //        CompanyId = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            //        CompanyName = table.Column<string>(nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_TblSys_Company", x => x.CompanyId);
            //    });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "TblDb_Customers");

            //migrationBuilder.DropTable(
            //    name: "TblDb_Customers_ShipTo");

            //migrationBuilder.DropTable(
            //    name: "TblOrders_Comments");

            migrationBuilder.DropTable(
                name: "TblOrders_CommentsAudit");

            //migrationBuilder.DropTable(
            //    name: "tblOrders_InsideRep");

            //migrationBuilder.DropTable(
            //    name: "tblOrders_OutsideRep");

            //migrationBuilder.DropTable(
            //    name: "TblSys_Company");
        }
    }
}

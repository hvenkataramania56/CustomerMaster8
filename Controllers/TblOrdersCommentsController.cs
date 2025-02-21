using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CustomerMaster.Models;
using CustomerMaster.ViewModels;
using CustomerMaster.Util;
using Microsoft.AspNetCore.Authorization;

namespace CustomerMaster.Controllers
{
    ////[Authorize(Policy = "RequireCMViewRole")]
    public class TblOrdersCommentsController : Controller
    {
        private readonly CustomerMasterContext _context;

        public TblOrdersCommentsController(CustomerMasterContext context)
        {
            _context = context;
        }

        // GET: TblOrdersComments
        public async Task<IActionResult> Index(
            string sortOrder,
            int? companyIdFilter, int? acctNumberFilter, int? tblDbCustomersShipToIdFilter,
            string custNameContainsFilter,
            int? companyIdSearch, int? acctNumberSearch, int? tblDbCustomersShipToIdSearch,
            string custNameContainsSearch,
            int? pageNumber
        )
        {
            ViewData["CreatedDateSortParm"] = string.IsNullOrEmpty(sortOrder) ? "createdDate_desc" : "";
            ViewData["AcctNumberSortParm"] = sortOrder == "AcctNumber" ? "acctNumber_desc" : "AcctNumber";

            if (
                companyIdSearch != null || acctNumberSearch != null
                ||
                tblDbCustomersShipToIdSearch != null
                ||
                custNameContainsSearch != null
            )
            {
                pageNumber = 1;
            }
            else
            {
                companyIdSearch = companyIdFilter;
                acctNumberSearch = acctNumberFilter;
                tblDbCustomersShipToIdSearch = tblDbCustomersShipToIdFilter;
                custNameContainsSearch = custNameContainsFilter;
            }

            ViewData["CompanyIdFilter"] = companyIdSearch;
            PopulateCompanyIdDropDown();

            ViewData["AcctNumberFilter"] = acctNumberSearch;
            PopulateAcctNumberDropDown(companyIdSearch);

            ViewData["TblDbCustomersShipToIdFilter"] = tblDbCustomersShipToIdSearch;
            PopulateTblDbCustomersShipToDropDown(companyIdSearch, acctNumberSearch);

            ViewData["CustNameContainsFilter"] = custNameContainsSearch;

            var someFieldCollection =
                (
                    from s in _context.TblOrdersComments
                    join ship in _context.TblDbCustomersShipTo
                        on
                            new { s.CompanyId, s.AcctNumber, s.CustId }
                            equals
                            new { ship.CompanyId, ship.AcctNumber, ship.CustId }
                    where
                        ship.Deleted == false && ship.CustShipName != null && ship.CustShipActive == -1
                    join cust in _context.TblDbCustomers
                        on
                            new { ship.CompanyId, AcctNumber = (ship.AcctNumber ?? 0) }
                            equals
                            new { cust.CompanyId, cust.AcctNumber }
                    where
                        cust.Deleted == false && cust.CustActive == -1
                        &&
                        (
                            string.IsNullOrEmpty(custNameContainsSearch)
                            ||
                            (
                                !string.IsNullOrEmpty(custNameContainsSearch)
                                &&
                                cust.CustName.Contains(custNameContainsSearch)
                            )
                        )
                        &&
                        (cust.CompanyId == 1 || cust.CompanyId == 2 || cust.CompanyId == 4)
                    join rep in _context.TblOrdersInsideRep
                        on
                            new { Initials = cust.InsideRep, Company = cust.CompanyId.ToString(), cust.Deleted }
                            equals
                            new { rep.Initials, rep.Company, rep.Deleted } into pp1
                    from rep in pp1.DefaultIfEmpty()
                    join rep1 in _context.TblOrdersOutsideRep
                        on
                            new { Initials = cust.OutsideRep, cust.CompanyId, cust.Deleted }
                            equals
                            new { rep1.Initials, rep1.CompanyId, rep1.Deleted } into pp2
                    from rep1 in pp2.DefaultIfEmpty()
                    join reps in _context.TblOrdersInsideRep
                        on
                            new { Initials = ship.InsideRep, Company = ship.CompanyId.ToString(), ship.Deleted }
                            equals
                            new { reps.Initials, reps.Company, reps.Deleted } into pp1s
                    from reps in pp1s.DefaultIfEmpty()
                    join rep1s in _context.TblOrdersOutsideRep
                        on
                            new { Initials = ship.OutsideRep, ship.CompanyId, ship.Deleted }
                            equals
                            new { rep1s.Initials, rep1s.CompanyId, rep1s.Deleted } into pp2s
                    from rep1s in pp2s.DefaultIfEmpty()
                    select new CustomerMaster.ViewModels.VwOrdersComments
                    {
                        Id = s.Id,
                        Comment = s.Comment,
                        CreatedDate = s.CreatedDate,
                        CompanyId = s.CompanyId,
                        CreatedUser = s.CreatedUser,
                        CreatedWorkStation = s.CreatedWorkStation,
                        AcctNumber = s.AcctNumber,
                        CustId = s.CustId,
                        UpdatedUser = s.UpdatedUser,
                        UpdatedDateTime = s.UpdatedDateTime,
                        BillCustAlpha = cust.CustAlpha,
                        BillCustName = cust.CustName,
                        CustBillAddress = cust.CustBillAddress,
                        CustBillAddr2 = cust.CustBillAddr2,
                        CustBillCity = cust.CustBillCity,
                        CustBillState = cust.CustBillState,
                        CustBillZipCode = cust.CustBillZipCode,
                        CustBillActive = (cust.CustActive == -1 ? true : false),
                        CustBillContact = cust.CustContact,
                        CustBillPlant = cust.CustPlant,
                        CustBillPhone = cust.CustPhone,
                        CustBillFax = cust.CustFax,
                        BillInsideRep = rep.RepName,
                        BillOutsideRep = rep1.RepName,
                        BillCustomerTerms = cust.CustomerTerms,
                        BillAllowedOverRunPercentage = cust.AllowedOverRunPercentage,
                        BillToEmailAddress = cust.BillToEmailAddress,
                        BillMasterAcctNumber = cust.MasterAcctNumber,
                        BillUpdatedUser = cust.UpdatedUser,
                        BillUpdatedDateTime = cust.UpdatedDateTime,
                        ShipToSameAsBillTo = (cust.ShipToCheck == -1 ? true : false),
                        TblDbCustomersShipToId = ship.Id,
                        ShipCustActive = (ship.CustShipActive == -1 ? true : false),
                        ShipCustAlpha = ship.CustAlpha,
                        CustShipName = ship.CustShipName,
                        CustShipAddress = ship.CustShipAddress,
                        CustShipAddr2 = ship.CustShipAddr2,
                        CustShipCity = ship.CustShipCity,
                        CustShipState = ship.CustShipState,
                        CustShipZipCode = ship.CustShipZipCode,
                        CustShipContact = ship.CustContact,
                        CustShipPlant = ship.CustPlant,
                        CustShipPhone = ship.CustPhone,
                        CustShipFax = ship.CustFax,
                        ShipInsideRep = reps.RepName,
                        ShipOutsideRep = rep1s.RepName,
                        ShipUpdatedUser = ship.UpdatedUser,
                        ShipUpdatedDateTime = ship.UpdatedDateTime
                    }
                );
            IQueryable<CustomerMaster.ViewModels.VwOrdersComments> fields =
                someFieldCollection.Cast<CustomerMaster.ViewModels.VwOrdersComments>();

            var someFieldCollection1 =
                (
                    from ship in _context.TblDbCustomersShipTo
                    join cust in _context.TblDbCustomers
                        on
                            new { ship.CompanyId, AcctNumber = (ship.AcctNumber ?? 0) }
                            equals
                            new { cust.CompanyId, cust.AcctNumber }
                    where
                        ship.Deleted == false && ship.CustShipName != null
                        &&
                        ship.CustShipActive == -1
                        &&
                        cust.Deleted == false && cust.CustActive == -1
                        &&
                        (
                            string.IsNullOrEmpty(custNameContainsSearch)
                            ||
                            (
                                !string.IsNullOrEmpty(custNameContainsSearch)
                                &&
                                cust.CustName.Contains(custNameContainsSearch)
                            )
                        )
                        &&
                        (cust.CompanyId == 1 || cust.CompanyId == 2 || cust.CompanyId == 4)
                    join comm in _context.TblOrdersComments
                        on
                            new { ship.CompanyId, ship.AcctNumber, ship.CustId }
                            equals
                            new { comm.CompanyId, comm.AcctNumber, comm.CustId }
                        into pp
                    from comm in pp.DefaultIfEmpty()
                    where comm == null
                    join rep in _context.TblOrdersInsideRep
                        on
                            new { Initials = cust.InsideRep, Company = cust.CompanyId.ToString(), cust.Deleted }
                            equals
                            new { rep.Initials, rep.Company, rep.Deleted } into pp1
                    from rep in pp1.DefaultIfEmpty()
                    join rep1 in _context.TblOrdersOutsideRep
                        on
                            new { Initials = cust.OutsideRep, cust.CompanyId, cust.Deleted }
                            equals
                            new { rep1.Initials, rep1.CompanyId, rep1.Deleted } into pp2
                    from rep1 in pp2.DefaultIfEmpty()
                    join reps in _context.TblOrdersInsideRep
                        on
                            new { Initials = ship.InsideRep, Company = ship.CompanyId.ToString(), ship.Deleted }
                            equals
                            new { reps.Initials, reps.Company, reps.Deleted } into pp1s
                    from reps in pp1s.DefaultIfEmpty()
                    join rep1s in _context.TblOrdersOutsideRep
                        on
                            new { Initials = ship.OutsideRep, ship.CompanyId, ship.Deleted }
                            equals
                            new { rep1s.Initials, rep1s.CompanyId, rep1s.Deleted } into pp2s
                    from rep1s in pp2s.DefaultIfEmpty()
                    select new CustomerMaster.ViewModels.VwOrdersComments
                    {
                        Id = 0,
                        Comment = null,
                        CreatedDate = null,
                        CompanyId = ship.CompanyId,
                        CreatedUser = null,
                        CreatedWorkStation = null,
                        AcctNumber = ship.AcctNumber,
                        CustId = ship.CustId,
                        UpdatedUser = null,
                        UpdatedDateTime = null,
                        BillCustAlpha = cust.CustAlpha,
                        BillCustName = cust.CustName,
                        CustBillAddress = cust.CustBillAddress,
                        CustBillAddr2 = cust.CustBillAddr2,
                        CustBillCity = cust.CustBillCity,
                        CustBillState = cust.CustBillState,
                        CustBillZipCode = cust.CustBillZipCode,
                        CustBillActive = (cust.CustActive == -1 ? true : false),
                        CustBillContact = cust.CustContact,
                        CustBillPlant = cust.CustPlant,
                        CustBillPhone = cust.CustPhone,
                        CustBillFax = cust.CustFax,
                        BillInsideRep = rep.RepName,
                        BillOutsideRep = rep1.RepName,
                        BillCustomerTerms = cust.CustomerTerms,
                        BillAllowedOverRunPercentage = cust.AllowedOverRunPercentage,
                        BillToEmailAddress = cust.BillToEmailAddress,
                        BillMasterAcctNumber = cust.MasterAcctNumber,
                        BillUpdatedUser = cust.UpdatedUser,
                        BillUpdatedDateTime = cust.UpdatedDateTime,
                        ShipToSameAsBillTo = (cust.ShipToCheck == -1 ? true : false),
                        TblDbCustomersShipToId = ship.Id,
                        ShipCustActive = (ship.CustShipActive == -1 ? true : false),
                        ShipCustAlpha = ship.CustAlpha,
                        CustShipName = ship.CustShipName,
                        CustShipAddress = ship.CustShipAddress,
                        CustShipAddr2 = ship.CustShipAddr2,
                        CustShipCity = ship.CustShipCity,
                        CustShipState = ship.CustShipState,
                        CustShipZipCode = ship.CustShipZipCode,
                        CustShipContact = ship.CustContact,
                        CustShipPlant = ship.CustPlant,
                        CustShipPhone = ship.CustPhone,
                        CustShipFax = ship.CustFax,
                        ShipInsideRep = reps.RepName,
                        ShipOutsideRep = rep1s.RepName,
                        ShipUpdatedUser = ship.UpdatedUser,
                        ShipUpdatedDateTime = ship.UpdatedDateTime
                    }
                );
            IQueryable<CustomerMaster.ViewModels.VwOrdersComments> fields1 =
                someFieldCollection1.Cast<CustomerMaster.ViewModels.VwOrdersComments>();

            var comments =
                (from s in fields select s)
                .Union
                (from s in fields1 select s)
            ;
            if (companyIdSearch != null)
            {
                comments = comments.Where(s => s.CompanyId == companyIdSearch);
            }
            if (acctNumberSearch != null)
            {
                comments = comments.Where(s => s.AcctNumber == acctNumberSearch);
            }
            if (tblDbCustomersShipToIdSearch != null)
            {
                var shipData =
                    _context.TblDbCustomersShipTo
                    .Where(s => s.Id == tblDbCustomersShipToIdSearch)
                    .FirstOrDefault()
                ;
                comments =
                    comments
                    .Where(
                        s =>
                            s.CompanyId == shipData.CompanyId && s.AcctNumber == shipData.AcctNumber
                            &&
                            s.CustId == shipData.CustId
                    )
                ;
            }


            comments =
                comments.OrderBy(s => s.CompanyId).ThenBy(s => s.AcctNumber).ThenBy(s => s.TblDbCustomersShipToId);

            //  int pageSize = 3;
            int pageSize = 1;
            //  return View(await comments.AsNoTracking().ToListAsync());
            return View(await PaginatedList<VwOrdersComments>.CreateAsync(comments.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        public void PopulateCompanyIdDropDown()
        {
            var gageQuery1 =
                from d in _context.TblSysCompany
                where (d.CompanyId == 1 || d.CompanyId == 2 || d.CompanyId == 4)
                select new
                {
                    d.CompanyId,
                    d.CompanyName
                };
            var gageQuery = gageQuery1.OrderBy(m => m.CompanyId);
            ViewData["CompanyIdDropDown"] = new SelectList(gageQuery, "CompanyId", "CompanyName");
        }

        public void PopulateAcctNumberDropDown(int? companyId)
        {
            var gageQuery1 =
                from d in _context.TblDbCustomers
                join comp in _context.TblSysCompany on d.CompanyId equals comp.CompanyId
                where
                    (d.CompanyId == 1 || d.CompanyId == 2 || d.CompanyId == 4) && d.Deleted == false
                    &&
                    ((companyId == null) || (companyId != null && d.CompanyId == companyId))
                    &&
                    d.CustActive == -1
                join ship in _context.TblDbCustomersShipTo
                    on
                        new { d.CompanyId, d.AcctNumber }
                        equals
                        new { ship.CompanyId, AcctNumber = (ship.AcctNumber ?? 0) }
                where
                    ship.Deleted == false && ship.CustShipName != null && ship.CustShipActive == -1
                select new
                {
                    d.AcctNumber,
                    d.CustName,
                    d.CustBillCity,
                    d.CustBillState,
                    comp.CompanyName
                };

            var gageQuery = gageQuery1
                .AsEnumerable() // Switch to client-side evaluation after initial data retrieval
                .GroupBy(x => new { x.AcctNumber }) // Group by AcctNumber in memory
                .Select(g => g.First()) // Select the first item from each group
                .Select(x => new // Project into the desired format after grouping
                {
                    x.AcctNumber,
                    MyCustName = $"{x.AcctNumber} - {x.CustName} - {(x.CustBillCity ?? String.Empty)}, {(x.CustBillState ?? String.Empty)} - {x.CompanyName}"
                })
                .OrderBy(m => m.AcctNumber)
                .ToList(); // Finally, convert to a list for SelectList

            ViewData["AcctNumberDropDown"] = new SelectList(gageQuery, "AcctNumber", "MyCustName");
        }

        public void PopulateTblDbCustomersShipToDropDown(int? companyId, int? acctNumber)
        {
            var gageQuery1 =
                from d in _context.TblDbCustomersShipTo
                join comp in _context.TblSysCompany on d.CompanyId equals comp.CompanyId
                join cust in _context.TblDbCustomers
                    on
                        new { d.CompanyId, AcctNumber = (d.AcctNumber ?? 0) }
                        equals
                        new { cust.CompanyId, cust.AcctNumber }
                where
                    (d.CompanyId == 1 || d.CompanyId == 2 || d.CompanyId == 4) && d.Deleted == false
                    &&
                    ((companyId == null) || (companyId != null && d.CompanyId == companyId))
                    &&
                    ((acctNumber == null) || (acctNumber != null && d.AcctNumber == acctNumber))
                    &&
                    cust.Deleted == false && cust.CustActive == -1
                    &&
                    d.CustShipName != null && d.CustShipActive == -1
                select new
                {
                    d.Id,
                    d.CustShipName,
                    d.CustId,
                    d.CustShipAddress,
                    d.CustShipCity,
                    d.CustShipState,
                    comp.CompanyName
                };

            var gageQuery = gageQuery1
                .AsEnumerable() // Switch to client-side evaluation
                .GroupBy(x => x.Id) // Group by Id in memory
                .Select(g => g.First()) // Select the first item from each group
                .Select(x => new  // Project into the desired format after grouping
                {
                    x.Id,
                    CustShipName =
                        $"{x.CustShipName?.Trim()}({x.CustId})" + " - " +
                        $"{(x.CustShipAddress ?? String.Empty)} - " +
                        $"{(x.CustShipCity ?? String.Empty)}, {(x.CustShipState ?? String.Empty)} - " +
                        x.CompanyName,
                    x.CustShipState
                })
                .OrderBy(m => m.CustShipState)
                .ThenBy(m => m.CustShipName)
                .ToList(); // Convert to list for SelectList

            ViewData["TblDbCustomersShipToDropDown"] =
                new SelectList(gageQuery, "Id", "CustShipName");
        }

        public JsonResult IndexCustBillVM(string id)
        {
            int intId;
            bool success = Int32.TryParse(id, out intId);
            if (success)
            {
                intId = int.Parse(id);
            }
            else
            {
                intId = 0;
            }
            var custBillVMCollection =
                _context.TblDbCustomers
                    .Join(
                        _context.TblSysCompany,
                        cust => cust.CompanyId,
                        comp => comp.CompanyId,
                        (cust, comp) =>
                            new {
                                CompanyId = cust.CompanyId,
                                Deleted = cust.Deleted,
                                AcctNumber = cust.AcctNumber,
                                CustName = cust.CustName,
                                CompanyName = comp.CompanyName,
                                CustActive = cust.CustActive,
                                CustBillCity = (cust.CustBillCity ?? String.Empty),
                                CustBillState = (cust.CustBillState ?? String.Empty)
                            }
                    )
                    .Where(
                        mdl =>
                            ((intId != 0 && mdl.CompanyId == intId) || (intId == 0))
                            &&
                            mdl.Deleted == false
                            &&
                            (mdl.CompanyId == 1 || mdl.CompanyId == 2 || mdl.CompanyId == 4)
                            &&
                            mdl.CustActive == -1
                    )
                    .Join(
                         _context.TblDbCustomersShipTo,
                         cust => new { cust.CompanyId, cust.AcctNumber },
                         ship => new { ship.CompanyId, AcctNumber = (ship.AcctNumber ?? 0) },
                         (cust, ship) =>
                            new
                            {
                                cust.AcctNumber,
                                cust.CustName,
                                cust.CustBillCity,
                                cust.CustBillState,
                                cust.CompanyName,
                                ship.CustShipActive,
                                ship.CustShipName,
                                ShipDeleted = ship.Deleted
                            }
                     )
                     .Where(
                         ship => ship.ShipDeleted == false && ship.CustShipName != null
                         &&
                         ship.CustShipActive == -1
                     )
                     .Select(
                         x =>
                             new CustBillVM
                             {
                                 Value = x.AcctNumber.ToString(),
                                 Text = x.AcctNumber.ToString() + " - " + x.CustName + " - " + x.CustBillCity + ", " + x.CustBillState + " - " + x.CompanyName,
                                 CompanyId = id
                             }
                     )
                     .GroupBy(x => x.Text).Select(g => g.First())
                     .OrderBy(m => int.Parse(m.Value))
                     .ToList();
            //  return custBillVMCollection;
            //  return Json(new { CustBillVM = custBillVMCollection });
            return Json(custBillVMCollection);
        }

        [HttpPost]
        [Authorize(Policy = "RequireCMShippingCommentsRole")]
        public IActionResult CommentUpdate(int shipToId, string comment, int id)
        {
            TblOrdersComments temp = _context.TblOrdersComments.FirstOrDefault(e => e.Id == id);
            string myMode = String.Empty;
            if (temp != null)
            {
                temp.Comment = comment;
                temp.UpdatedUser = User.Identity.Name;
                //  temp.UpdatedDateTime = DateTime.Now.ToLongDateString();
                temp.UpdatedDateTime = DateTime.Now.ToString("M/d/yyyy h:mm:ss tt");
                _context.Attach(temp).State = EntityState.Modified;
                myMode = "UPDATE";
            }
            else
            {
                TblDbCustomersShipTo tempS =
                    _context.TblDbCustomersShipTo.FirstOrDefault(e => e.Id == shipToId);
                temp = new TblOrdersComments();
                temp.Comment = comment;
                temp.CreatedDate = DateTime.Now;
                temp.CompanyId = tempS.CompanyId;
                temp.CreatedUser = User.Identity.Name;

                string IP = HttpContext.Connection.RemoteIpAddress.ToString();
                string compName = CompNameHelper.DetermineCompName(IP);
                temp.CreatedWorkStation = compName;

                temp.AcctNumber = tempS.AcctNumber;
                temp.CustId = tempS.CustId;

                _context.TblOrdersComments.Add(temp);
                myMode = "INSERT";
            }
            _context.SaveChanges();

            int newId = temp.Id;

            TblOrdersCommentsAudit ca = new TblOrdersCommentsAudit();
            PropertyCopier<TblOrdersComments, TblOrdersCommentsAudit>
                .Copy(temp, ca);
            ca.AuditDateTime = DateTime.Now;
            ca.AuditOperation = myMode;
            ca.AuditUser = User.Identity.Name;
            _context.TblOrdersCommentsAudit.Add(ca);
            _context.SaveChanges();

            return Json(new { newId = newId });
        }

        public JsonResult IndexCustShipVM(string id, string acctNum)
        {
            int intId;
            bool success = Int32.TryParse(id, out intId);
            if (success)
            {
                intId = int.Parse(id);
            }
            else
            {
                intId = 0;
            }

            int intAcctNum;
            success = Int32.TryParse(acctNum, out intAcctNum);
            if (success)
            {
                intAcctNum = int.Parse(acctNum);
            }
            else
            {
                intAcctNum = 0;
            }

            var custShipVMCollection =
                from d in _context.TblDbCustomersShipTo
                join comp in _context.TblSysCompany on d.CompanyId equals comp.CompanyId
                join cust in _context.TblDbCustomers
                    on
                        new { d.CompanyId, AcctNumber = (d.AcctNumber ?? 0) }
                        equals
                        new { cust.CompanyId, cust.AcctNumber }
                where
                    (d.CompanyId == 1 || d.CompanyId == 2 || d.CompanyId == 4) && d.Deleted == false
                    &&
                    ((intId == 0) || (intId != 0 && d.CompanyId == intId))
                    &&
                    ((intAcctNum == 0) || (intAcctNum != 0 && d.AcctNumber == intAcctNum))
                    &&
                    cust.Deleted == false && cust.CustActive == -1
                    &&
                    d.CustShipName != null && d.CustShipActive == -1
                orderby d.CustShipState, d.CustShipName
                select new
                {
                    d.Id,
                    CustShipName =
                        d.CustShipName.Trim() + "(" + d.CustId.ToString() + ")" + " - " +
                            (d.CustShipAddress ?? String.Empty) + " - " +
                            (d.CustShipCity ?? String.Empty) + ", " + (d.CustShipState ?? String.Empty) + " - " +
                            comp.CompanyName,
                    d.CustShipState
                }
            ;
            custShipVMCollection = custShipVMCollection.GroupBy(x => x.Id).Select(g => g.First());
            //  return custBillVMCollection;
            //  return Json(new { CustBillVM = custBillVMCollection });
            return Json(custShipVMCollection);
        }

        // GET: TblOrdersComments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblOrdersComments = await _context.TblOrdersComments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblOrdersComments == null)
            {
                return NotFound();
            }

            return View(tblOrdersComments);
        }

        // GET: TblOrdersComments/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CustomerComments()
        //  public void CustomerComments()
        {
            string myId = Request.Form["myId"];
            int intMyId = 0;
            bool ifSuccess = int.TryParse(myId, out intMyId);

            string myShipToId = Request.Form["myShipToId"];
            int intMyShipToId = 0;
            ifSuccess = int.TryParse(myShipToId, out intMyShipToId);

            string companyIdFilterStr = Request.Form["companyIdFilter"];
            int companyIdFilter;
            ifSuccess = int.TryParse(companyIdFilterStr, out companyIdFilter);
            int? companyIdFilter1 = null;
            if (ifSuccess) companyIdFilter1 = companyIdFilter;

            string acctNumberFilterStr = Request.Form["acctNumberFilter"];
            int acctNumberFilter;
            ifSuccess = int.TryParse(acctNumberFilterStr, out acctNumberFilter);
            int? acctNumberFilter1 = null;
            if (ifSuccess) acctNumberFilter1 = acctNumberFilter;

            string tblDbCustomersShipToIdFilterStr = Request.Form["tblDbCustomersShipToIdFilter"];
            int tblDbCustomersShipToIdFilter;
            ifSuccess = int.TryParse(tblDbCustomersShipToIdFilterStr, out tblDbCustomersShipToIdFilter);
            int? tblDbCustomersShipToIdFilter1 = null;
            if (ifSuccess) tblDbCustomersShipToIdFilter1 = tblDbCustomersShipToIdFilter;

            string pageNumber = Request.Form["pageNumber"];
            int intPageNumber = 1;
            ifSuccess = int.TryParse(pageNumber, out intPageNumber);

            string comment = Request.Form["Comment"];

            TblOrdersComments temp = _context.TblOrdersComments.FirstOrDefault(e => e.Id == intMyId);
            string myMode = String.Empty;
            if (temp != null)
            {
                temp.Comment = comment;
                temp.UpdatedUser = User.Identity.Name;
                //  temp.UpdatedDateTime = DateTime.Now.ToLongDateString();
                temp.UpdatedDateTime = DateTime.Now.ToString("M/d/yyyy h:mm:ss tt");
                _context.Attach(temp).State = EntityState.Modified;
                myMode = "UPDATE";
            }
            else
            {
                TblDbCustomersShipTo tempS =
                    _context.TblDbCustomersShipTo.FirstOrDefault(e => e.Id == intMyShipToId);
                temp = new TblOrdersComments();
                temp.Comment = comment;
                temp.CreatedDate = DateTime.Now;
                temp.CompanyId = tempS.CompanyId;
                temp.CreatedUser = User.Identity.Name;

                string IP = HttpContext.Connection.RemoteIpAddress.ToString();
                string compName = CompNameHelper.DetermineCompName(IP);
                temp.CreatedWorkStation = compName;

                temp.AcctNumber = tempS.AcctNumber;
                temp.CustId = tempS.CustId;

                _context.TblOrdersComments.Add(temp);
                myMode = "INSERT";
            }
            _context.SaveChanges();

            TblOrdersCommentsAudit ca = new TblOrdersCommentsAudit();
            PropertyCopier<TblOrdersComments, TblOrdersCommentsAudit>
                .Copy(temp, ca);
            ca.AuditDateTime = DateTime.Now;
            ca.AuditOperation = myMode;
            ca.AuditUser = User.Identity.Name;
            _context.TblOrdersCommentsAudit.Add(ca);
            _context.SaveChanges();


            //  return RedirectToAction(nameof(Index),
            return RedirectToAction("Index",
                    new
                    {
                        pageNumber = intPageNumber,
                        companyIdFilter = companyIdFilter1,
                        acctNumberFilter = acctNumberFilter1,
                        tblDbCustomersShipToIdFilter = tblDbCustomersShipToIdFilter1
                    }
                );
            //  return View("Index");
            //  return View();
        }

        // GET: TblOrdersComments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblOrdersComments = await _context.TblOrdersComments.FindAsync(id);
            if (tblOrdersComments == null)
            {
                return NotFound();
            }
            return View(tblOrdersComments);
        }

        // POST: TblOrdersComments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Comment,CreatedDate,CompanyId,CreatedUser,CreatedWorkStation,AcctNumber,CustId,UpdatedUser,UpdatedDateTime")] TblOrdersComments tblOrdersComments)
        {
            if (id != tblOrdersComments.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblOrdersComments);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblOrdersCommentsExists(tblOrdersComments.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tblOrdersComments);
        }

        // GET: TblOrdersComments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblOrdersComments = await _context.TblOrdersComments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tblOrdersComments == null)
            {
                return NotFound();
            }

            return View(tblOrdersComments);
        }

        // POST: TblOrdersComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblOrdersComments = await _context.TblOrdersComments.FindAsync(id);
            _context.TblOrdersComments.Remove(tblOrdersComments);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblOrdersCommentsExists(int id)
        {
            return _context.TblOrdersComments.Any(e => e.Id == id);
        }
    }
}
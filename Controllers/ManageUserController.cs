using Demo1.Models.PSOrderContext;
using Extensions.Common.STExtension;
using Microsoft.AspNetCore.Mvc;
using Demo1.Models;
using System.Data;
using Extensions.Common.STResultAPI;

namespace PMSS.Controllers.ManageUser
{
    public class ManageUserController : Controller
    {
        private readonly PSOrderContext DB;
        public ManageUserController(PSOrderContext db)
        {
            DB = db;
        }

        public IActionResult ManageUserForm(int? pageNumber)
        {
            ManageUserListClass Model = new ManageUserListClass();
            List<ManageUserClass> lstData = new List<ManageUserClass>();
            lstData = GetData();
            Model.lstData = lstData;
            Model.PageNumber = (pageNumber == null ? 1 : Convert.ToInt32(pageNumber));
            Model.PageSize = 10;

            Model.PagePrevious = Model.PageNumber - 1;
            Model.PageNext = Model.PageNumber + 1;

            if (lstData != null)
            {
                Model.lstData = lstData.OrderBy(x => x.nNo)
                                    .Skip(Model.PageSize * (Model.PageNumber - 1))
                                    .Take(Model.PageSize).ToList();
                Model.TotalCount = lstData.Count;
                Model.PagerCount = ((Model.TotalCount / Model.PageSize) -
                                    (Model.TotalCount % Model.PageSize == 0 ? 1 : 0)) + 1;
            }

            return View(Model);
        }

        public List<ManageUserClass> GetData()
        {
            List<ManageUserClass> lstData = new List<ManageUserClass>();
            var lstUser = DB.Usermains.Where(w => w.IsDelete == false).OrderByDescending(o => o.dUpdateDate).ToList();
            if (lstUser.Count > 0)
            {
                int i = 1;
                foreach (var Item in lstUser)
                {
                    lstData.Add(new ManageUserClass
                    {
                        nNo = i++,
                        nID = (Item.No + "").ToInt(),
                        OAUserID = Item.OAUserID,
                        Name = Item.Name,
                        dStartDate = Item.dStartDate,
                        dEndDate = Item.dEndDate,
                        sStartDate = Item.dStartDate.ToStringFromDate("dd/MM/yyyy", "en-US"),
                        sEndDate = Item.dEndDate.ToStringFromDate("dd/MM/yyyy", "en-US")
                    });
                }
            }

            return lstData;
        }

        [HttpPost]
        public IActionResult ManageUserForm(ManageUserListClass Obj)
        {
            try
            {
                DateTime? dStartDate = Obj.sStartDate.ToDateFromString("dd/MM/yyyy", "en-US");
                DateTime? dEndDate = Obj.sEndDate.ToDateFromString("dd/MM/yyyy", "en-US");

                var UPT = DB.Usermains.FirstOrDefault(f => f.OAUserID == Obj.OAUserID);
                if (UPT != null)
                {
                    if (Obj.IsEdit)
                    {
                        UPT.Role = "";
                        UPT.Name = Obj.Name;
                        UPT.Dep = "";
                        UPT.Email = "";
                        UPT.Tel = "";
                        UPT.dStartDate = Obj.dStartDate;
                        UPT.dEndDate = Obj.dEndDate;
                        UPT.dStartDate = dStartDate.HasValue ? dStartDate.Value : DateTime.Now;
                        UPT.dEndDate = dEndDate.HasValue ? dEndDate.Value : DateTime.Now;
                        UPT.dUpdateDate = DateTime.Now;
                        UPT.sUpdate = null;
                        UPT.IsDelete = false;
                        DB.SaveChanges();

                        TempData["Success"] = "Action Completed', 'Data is already saved.";
                        Redirect("ManageUnitDepartmentForm");
                    }
                    else
                    {
                        var lstDup = DB.Usermains.Where(w => w.OAUserID == Obj.OAUserID && w.IsDelete.Value == false).ToList();
                        if (lstDup.Count > 0)
                        {
                            TempData["Error"] = "OA User is Duplicate.";
                            Redirect("ManageUnitDepartmentForm");
                        }
                        else
                        {
                            UPT.Role = "";
                            UPT.Name = Obj.Name;
                            UPT.Dep = "";
                            UPT.Email = "";
                            UPT.Tel = "";
                            UPT.dStartDate = Obj.dStartDate;
                            UPT.dEndDate = Obj.dEndDate;
                            UPT.dStartDate = dStartDate.HasValue ? dStartDate.Value : DateTime.Now;
                            UPT.dEndDate = dEndDate.HasValue ? dEndDate.Value : DateTime.Now;
                            UPT.dUpdateDate = DateTime.Now;
                            UPT.sUpdate = null;
                            UPT.IsDelete = false;
                            DB.SaveChanges();

                            TempData["Success"] = "Action Completed', 'Data is already saved.";
                            Redirect("ManageUnitDepartmentForm");
                        }
                    }
                }
                else
                {
                    int nID = DB.Usermains.Any() ? (DB.Usermains.Max(m => m.No) + "").ToInt() + 1 : 1;

                    Usermain CRT = new Usermain();
                    //CRT.No = (nID + "").ToDecimal();
                    CRT.OAUserID = Obj.OAUserID;
                    CRT.Role = "";
                    CRT.Name = Obj.Name;
                    CRT.Dep = "";
                    CRT.Email = "";
                    CRT.Tel = "";
                    CRT.dStartDate = Obj.dStartDate;
                    CRT.dEndDate = Obj.dEndDate;
                    CRT.dStartDate = dStartDate.HasValue ? dStartDate.Value : DateTime.Now;
                    CRT.dEndDate = dEndDate.HasValue ? dEndDate.Value : DateTime.Now;
                    CRT.dCreateDate = DateTime.Now;
                    CRT.sCreate = null;
                    CRT.dUpdateDate = DateTime.Now;
                    CRT.sUpdate = null;
                    CRT.IsDelete = false;
                    DB.Usermains.Add(CRT);
                    DB.SaveChanges();

                    TempData["Success"] = "Action Completed', 'Data is already saved.";
                    Redirect("ManageUnitDepartmentForm");
                }

            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return ManageUserForm(0);
        }

        [HttpPost]
        public ResultAPI Delete(string sID)
        {
            ResultAPI Result = new ResultAPI();
            try
            {
                var DEL = DB.Usermains.FirstOrDefault(f => f.OAUserID == sID && f.IsDelete == false);
                if (DEL != null)
                {
                    DEL.dUpdateDate = DateTime.Now;
                    DEL.sUpdate = null;
                    DEL.IsDelete = true;
                    DB.SaveChanges();
                }
                Result.Status = ResultStatus.Success;
                Redirect("ManageUserForm");
            }
            catch (Exception ex)
            {
                Result.Message = ex.Message;
                Result.Status = ResultStatus.Failed;
            }

            return Result;
        }
    }
}

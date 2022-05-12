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
            var lstUser = DB.UserMaintains.Where(w => !w.IsDelete).OrderBy(o => o.nID).ToList();
            if (lstUser.Count > 0)
            {
                int i = 1;
                foreach (var Item in lstUser)
                {
                    lstData.Add(new ManageUserClass
                    {
                        nNo = i++,
                        nID = Item.nID,
                        sOAUserID = Item.sOAUserID,
                        sName = Item.sName,
                        dStartDate = Item.dStartDate,
                        sStartDate = Item.dStartDate.ToStringFromDate("dd/MM/yyyy", "en-US"),
                        sEndDate = Item.dEndDate.ToStringFromDate("dd/MM/yyyy", "en-US"),
                        IsDelete = Item.IsDelete
                    });
                }
            }

            return lstData;
        }

        [HttpPost]
        public ResultAPI SaveToDB(ManageUserClass Obj)
        {
            ResultAPI Result = new ResultAPI();

            try
            {
                DateTime? dStartDate = Obj.sStartDate.ToDateFromString("dd/MM/yyyy", "en-US");
                DateTime? dEndDate = Obj.sEndDate.ToDateFromString("dd/MM/yyyy", "en-US");
                var UPT = DB.UserMaintains.FirstOrDefault(f => f.nID == Obj.nID);
                if (UPT != null)
                {
                    UPT.sOAUserID = Obj.sOAUserID;
                    UPT.sRole = Obj.sRole;
                    UPT.sName = Obj.sName;
                    UPT.sDep = Obj.sDep;
                    UPT.sEmail = Obj.sEmail;
                    UPT.sTel = Obj.sTel;
                    UPT.dStartDate = Obj.dStartDate;
                    UPT.dEndDate = Obj.dEndDate;
                    UPT.dStartDate = dStartDate.HasValue ? dStartDate.Value : DateTime.Now;
                    UPT.dEndDate = dEndDate.HasValue ? dEndDate.Value : DateTime.Now;
                    UPT.dUpdateDate = DateTime.Now;
                    UPT.sUpdate = null;
                    UPT.IsDelete = false;
                    DB.SaveChanges();
                }
                else
                {
                    var lstDup = DB.UserMaintains.Where(w => w.sOAUserID == Obj.sOAUserID && !w.IsDelete).ToList();
                    if (lstDup.Count > 0)
                    {
                        Result.Message = "OA User is Duplicate.";
                        Result.Status = ResultStatus.Failed;
                    }
                    else
                    {
                        int nID = DB.UserMaintains.Any() ? DB.UserMaintains.Max(m => m.nID) + 1 : 1;

                        UserMaintain CRT = new UserMaintain();
                        CRT.nID = nID;
                        CRT.sOAUserID = Obj.sOAUserID;
                        CRT.sRole = Obj.sRole;
                        CRT.sName = Obj.sName;
                        CRT.sDep = Obj.sDep;
                        CRT.sEmail = Obj.sEmail;
                        CRT.sTel = Obj.sTel;
                        CRT.dStartDate = Obj.dStartDate;
                        CRT.dEndDate = Obj.dEndDate;
                        CRT.dStartDate = dStartDate.HasValue ? dStartDate.Value : DateTime.Now;
                        CRT.dEndDate = dEndDate.HasValue ? dEndDate.Value : DateTime.Now;
                        CRT.dCreateDate = DateTime.Now;
                        CRT.sCreate = null;
                        CRT.IsDelete = false;
                        DB.UserMaintains.Add(CRT);
                        DB.SaveChanges();
                    }
                }
                Result.Status = ResultStatus.Success;
            }
            catch (Exception ex)
            {
                Result.Message = ex.Message;
                Result.Status = ResultStatus.Failed;
            }

            return Result;
        }

        [HttpPost]
        public ResultAPI Delete(int nID = 0)
        {
            ResultAPI Result = new ResultAPI();
            try
            {
                var DEL = DB.UserMaintains.FirstOrDefault(f => f.nID == nID);
                if (DEL != null)
                {
                    DEL.dUpdateDate = DateTime.Now;
                    DEL.sUpdate = null;
                    DEL.IsDelete = true;
                    DB.SaveChanges();
                }
                Result.Status = ResultStatus.Success;
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

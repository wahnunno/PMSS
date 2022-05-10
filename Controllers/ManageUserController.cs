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

        public IActionResult ManageUserForm()
        {
            ManageUserListClass Model = new ManageUserListClass();
            Model.lstData = GetData();

            return View(Model);
        }

        [HttpPost]
        public IActionResult ManageUserForm(ManageUserListClass Obj)
        {
            ManageUserListClass Model = new ManageUserListClass();
            //ResultAPI Result = SaveDB(Obj);
            Model.lstData = GetData();

            return View(Model);
        }

        public List<ManageUserClass> GetData()
        {
            List<ManageUserClass> lstData = new List<ManageUserClass>();

            var lstUser = DB.UserMaintains.Where(w => !w.IsDelete).OrderBy(o => o.nNo).ToList();
            if (lstUser.Count > 0)
            {
                int i = 1;
                foreach (var Item in lstUser)
                {
                    lstData.Add(new ManageUserClass
                    {
                        nNo = i++,
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

        public ResultAPI SaveToDB(ManageUserClass Obj)
        {
            ResultAPI Result = new ResultAPI();
            try
            {
                DateTime? dStartDate = Obj.sStartDate.ToDateFromString("dd/MM/yyyy", "en-US");
                DateTime? dEndDate = Obj.sEndDate.ToDateFromString("dd/MM/yyyy", "en-US");
                var lstDup = DB.UserMaintains.Where(w => w.sOAUserID == Obj.sOAUserID && !w.IsDelete).ToList();
                if (lstDup.Count > 0)
                {
                    Result.Message = "OA User is Duplicate.";
                    Result.Status = ResultStatus.Failed;
                }
                else
                {
                    var UPT = DB.UserMaintains.FirstOrDefault(f => f.sOAUserID == Obj.sOAUserID);
                    if (UPT != null)
                    {
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
                        int nID = DB.UserMaintains.Any() ? DB.UserMaintains.Max(m => m.nNo) + 1 : 1;

                        UserMaintain CRT = new UserMaintain();
                        CRT.nNo = nID;
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
                    Result.Status = ResultStatus.Success;
                }
            }
            catch (Exception ex)
            {
                Result.Message = ex.Message;
                Result.Status = ResultStatus.Failed;
            }

            return Result;
        }

        //public ResultAPI SaveDB(ManageUserListClass Obj)
        //{
        //    ResultAPI Result = new ResultAPI();
        //    try
        //    {
        //        var UPT = DB.UserMaintains.FirstOrDefault(f => f.sOAUserID == Obj.sOAUserID);
        //        if (UPT != null)
        //        {
        //            UPT.sRole = Obj.sRole;
        //            UPT.sName = Obj.sName;
        //            UPT.sDep = Obj.sDep;
        //            UPT.sEmail = Obj.sEmail;
        //            UPT.sTel = Obj.sTel;
        //            UPT.dStartDate = Obj.dStartDate;
        //            UPT.dEndDate = Obj.dEndDate;
        //            //UPT.dStartDate = DateTime.Now;
        //            //UPT.dEndDate = DateTime.Now;
        //            UPT.dUpdateDate = DateTime.Now;
        //            UPT.sUpdate = "1";
        //            UPT.IsDelete = false;
        //            DB.SaveChanges();
        //            Result.Status = ResultStatus.Success;
        //        }
        //        else
        //        {
        //            int nID = DB.UserMaintains.Any() ? DB.UserMaintains.Max(m => m.nNo) + 1 : 1;

        //            UserMaintain CRT = new UserMaintain();
        //            CRT.nNo = nID;
        //            CRT.sOAUserID = Obj.sOAUserID;
        //            CRT.sRole = Obj.sRole;
        //            CRT.sName = Obj.sName;
        //            CRT.sDep = Obj.sDep;
        //            CRT.sEmail = Obj.sEmail;
        //            CRT.sTel = Obj.sTel;
        //            CRT.dStartDate = Obj.dStartDate;
        //            CRT.dEndDate = Obj.dEndDate;
        //            //CRT.dStartDate = DateTime.Now;
        //            //CRT.dEndDate = DateTime.Now;
        //            CRT.dCreateDate = DateTime.Now;
        //            CRT.sCreate = "1";
        //            CRT.IsDelete = false;
        //            DB.UserMaintains.Add(CRT);
        //            DB.SaveChanges();
        //            Result.Status = ResultStatus.Success;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Result.Message = ex.Message;
        //        Result.Status = ResultStatus.Failed;
        //    }

        //    return Result;
        //}

        public class cResult
        {
            public string? sStatus { get; set; }
            public string? sMsg { get; set; }
            public string? sContent { get; set; }
            //public List<UserMaintain>? lstData { get; set; }
        }
    }
}

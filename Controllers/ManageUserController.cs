using Demo1.con_db;
using Demo1.Models;
using Demo1.Models.PSOrderContext;
using Microsoft.AspNetCore.Mvc;

namespace Demo1.Controllers
{
    public class ManageUserController : Controller
    {
        private readonly AppDbContext DB;
        public ManageUserController(AppDbContext db)
        {
            DB = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ManageUserForm(SaveData Obj)
        {
            //ManageUserListClass Model = new ManageUserListClass();
            //List<ManageUserClass> lstData = new List<ManageUserClass>();
            //var lst = DB.UserMaintains.Where(w => !w.IsDelete).ToList();
            //if (lst.Count > 0)
            //{
            //    foreach (var Item in lst)
            //    {
            //        lstData.Add(new ManageUserClass
            //        {
            //            nNo = Item.nNo,
            //            sOAUserID = Item.sOAUserID,
            //            sRole = Item.sRole,
            //            sName = Item.sName,
            //            sDep = Item.sDep,
            //            sEmail = Item.sEmail,
            //            sTel = Item.sTel,
            //            dStartDate = Item.dStartDate,
            //            dEndDate = Item.dEndDate,
            //            IsDelete = Item.IsDelete
            //        });
            //    }
            //}
            //Model.lstData = lstData;
            //return View(Model);
            return View();
        }

        [HttpPost]
        public string SaveDB(SaveData Obj)
        {
            string Result = "";
            //var UPT = DB.UserMaintains.FirstOrDefault(f => f.sOAUserID == Obj.sOAUserID);
            //if (UPT != null)
            //{
            //    UPT.sRole = Obj.sRole;
            //    UPT.sName = Obj.sName;
            //    UPT.sDep = Obj.sDep;
            //    UPT.sEmail = Obj.sEmail;
            //    UPT.sTel = Obj.sTel;
            //    UPT.dStartDate = Obj.dStartDate;
            //    UPT.dEndDate = Obj.dEndDate;
            //    UPT.dUpdateDate = DateTime.Now;
            //    UPT.sUpdate = "1";
            //    UPT.IsDelete = true;
            //    DB.SaveChanges();
            //    Result = "Success";
            //}
            //else
            //{
            //    UserMaintain CRT = new UserMaintain();
            //    int nID = DB.UserMaintains.Any() ? DB.UserMaintains.Max(m => m.nNo) + 1 : 1;
            //    CRT.sOAUserID = nID + "";
            //    CRT.sRole = Obj.sRole;
            //    CRT.sName = Obj.sName;
            //    CRT.sDep = Obj.sDep;
            //    CRT.sEmail = Obj.sEmail;
            //    CRT.sTel = Obj.sTel;
            //    CRT.dStartDate = Obj.dStartDate;
            //    CRT.dEndDate = Obj.dEndDate;
            //    CRT.dCreateDate = DateTime.Now;
            //    CRT.sCreate = "1";
            //    CRT.IsDelete = true;
            //    DB.SaveChanges();
            //    Result = "Success";
            //}
            return Result;
        }

        public class cResult
        {
            public string? sStatus { get; set; }
            public string? sMsg { get; set; }
            public string? sContent { get; set; }
            //public List<UserMaintain>? lstData { get; set; }
        }
        public class SaveData
        {

        }
    }
}

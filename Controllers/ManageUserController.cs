using Demo1.con_db;
using Demo1.Models;
using Demo1.Models.PSOrderContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Demo1.Controllers
{
    public class ManageUserController : Controller
    {
        //private readonly string ConnectionString;
        //public ManageUserController()
        //{
        //    IConfigurationRoot configuration = new ConfigurationBuilder()
        //    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
        //    .AddJsonFile("appsettings.json")
        //    .Build();
        //    ConnectionString = configuration.GetConnectionString("DefaultConnection");
        //}

        private readonly PSOrderContext DB;
        public ManageUserController(PSOrderContext db)
        {
            DB = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ManageUserForm(SaveData Obj)
        {
            List<ManageUserClass> lstData = GetData();

            ManageUserListClass Model = new ManageUserListClass();
            Model.lstData = lstData;
            return View(Model);
        }

        public List<ManageUserClass> GetData()
        {
            List<ManageUserClass> lst = new List<ManageUserClass>();
            var lstData = DB.UserMaintains.Where(w => !w.IsDelete).ToList();
            if (lstData.Count > 0)
            {
                foreach (var Item in lstData)
                {
                    lst.Add(new ManageUserClass
                    {
                        nNo = Item.nNo,
                        sOAUserID = Item.sOAUserID,
                        sName = Item.sName,
                        dStartDate = Item.dStartDate,
                        sStartDate = Item.dStartDate.HasValue ? Item.dStartDate.Value.ToString("dd/MM/yyyy") : "",
                        sEndDate = Item.dEndDate.HasValue ? Item.dEndDate.Value.ToString("dd/MM/yyyy") : "",
                        IsDelete = Item.IsDelete
                    });
                }
            }

            return lst;
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

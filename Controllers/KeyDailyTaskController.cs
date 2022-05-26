using Demo1.Models;
using Demo1.Models.PSOrderContext;
using Microsoft.AspNetCore.Mvc;
using static PMSS.Extensions.Common.AllClass;

namespace Demo1.Controllers
{
    public class KeyDailyTaskController : Controller
    {
        private readonly PSOrderContext DB;
        public KeyDailyTaskController(PSOrderContext db)
        {
            DB = db;
        }

        public IActionResult KeyDailyTaskForm()
        {
            KeyDailyTaskListClass Model = new KeyDailyTaskListClass();
            List<cDropDown> lstGroupMail = new List<cDropDown>();
            List<cDropDownTypeMail> lstTypeMail = new List<cDropDownTypeMail>();
            lstGroupMail = GetDropDownGroupMail();
            lstTypeMail = GetDropDownTypeMail();
            Model.lstGroupMail = lstGroupMail;
            Model.lstTypeMail = lstTypeMail;

            return View(Model);
        }

        public List<cDropDownTypeMail> GetDropDownTypeMail()
        {
            List<cDropDownTypeMail> lstData = new List<cDropDownTypeMail>();
            lstData = DB.Type_Mails.Where(w => !w.IsDelete.Value).Select(s =>
                   new cDropDownTypeMail
                   {
                       value = s.ID + "",
                       label = s.Type_Name + "",
                       sTypeMailID = s.Type_Mail1 + ""
                   }).ToList();
            return lstData;
        }

        public List<cDropDown> GetDropDownGroupMail()
        {
            List<cDropDown> lstData = new List<cDropDown>();
            lstData = DB.MT_TypeInOuts.Where(w => w.cActive == "Y" && !w.IsDelete).Select(s =>
                  new cDropDown
                  {
                      value = s.nID + "",
                      label = s.sTypeInOutName + ""
                  }).ToList();
            return lstData;
        }

        [HttpPost]
        public IActionResult KeyDailyTaskForm(KeyDailyTaskListClass Obj)
        {
            return KeyDailyTaskForm();
        }

    }
}

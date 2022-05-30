using System.ComponentModel.DataAnnotations;
using static PMSS.Extensions.Common.AllClass;

namespace Demo1.Models
{
    public class KeyDailyTaskListClass
    {
        public int nID { get; set; }
        public string sDate { get; set; }
        public string sTime { get; set; }
        public string sGroupMail { get; set; }
        public string sTypeMail { get; set; }
        public string sSender { get; set; }
        public string sRef { get; set; }
        public int nQuantity { get; set; }
        public decimal nAmount { get; set; }
        public string sReceiver { get; set; }
        public int nPostal { get; set; }
        public string sRCNumber { get; set; }
        public bool IsEdit { get; set; }
        public List<KeyDailyTask_Normal_Class> lstNormal { get; set; } = new List<KeyDailyTask_Normal_Class>();
        public List<KeyDailyTask_Register_Class> lstRegister { get; set; } = new List<KeyDailyTask_Register_Class>();
        public List<cDropDown> lstGroupMail { get; set; } = new List<cDropDown>();
        public List<cDropDownTypeMail> lstTypeMail { get; set; } = new List<cDropDownTypeMail>();
        public int PagePrevious { get; internal set; }
        public int PageNext { get; internal set; }
        public int PageNumber { get; internal set; }
        public int PageSize { get; internal set; }
        public int PagerCount { get; internal set; }
        public int TotalCount { get; internal set; }
    }
}

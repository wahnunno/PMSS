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
        public bool IsLot { get; set; }
        public string sSender1 { get; set; }
        public string sSender2 { get; set; }
        public string sRef1 { get; set; }
        public string sRef2 { get; set; }
        public int nQuantity { get; set; }
        public decimal nAmount1 { get; set; }
        public decimal nAmount2 { get; set; }
        public string sReceiver { get; set; }
        public string sPostal { get; set; }
        public string sRCNumber { get; set; }
        public string sRCNumber1 { get; set; }
        public string sRCNumber2 { get; set; }
        public string sRCNumber3 { get; set; }
        public bool IsEdit { get; set; }
        public List<KeyDailyTask_Normal_Class> lstNormal { get; set; } = new List<KeyDailyTask_Normal_Class>();
        public List<KeyDailyTask_Register_Class> lstRegister { get; set; } = new List<KeyDailyTask_Register_Class>();
        public List<cDropDown> lstDiv { get; set; } = new List<cDropDown>();
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

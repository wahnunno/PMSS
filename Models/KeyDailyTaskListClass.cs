using System.ComponentModel.DataAnnotations;
using static PMSS.Extensions.Common.AllClass;

namespace Demo1.Models
{
    public class KeyDailyTaskListClass
    {
        [Required]
        public string sDate { get; set; }
        [Required]
        public string sTime { get; set; }
        [Required]
        public string sGroupMail { get; set; }
        [Required]
        public string sTypeMail { get; set; }
        [Required]
        public string sSender { get; set; }
        [Required]
        public string sRef { get; set; }
        [Required]
        public int nQuantity { get; set; }
        [Required]
        public decimal nAmount { get; set; }
        [Required]
        public string sReceiver { get; set; }
        [Required]
        public int nPostal { get; set; }
        [Required]
        public string sRCNumber { get; set; }
        public List<KeyDailyTaskClass> lstData { get; set; } = new List<KeyDailyTaskClass>();
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

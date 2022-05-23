using static PMSS.Extensions.Common.AllClass;

namespace Demo1.Models
{
    public class ManageMailTypeListClass
    {
        public decimal ID { get; set; }
        public string Type_Name { get; set; }
        public decimal Type_Pay { get; set; }
        public string Type_Mail { get; set; }
        public string Type_InOut { get; set; }
        public bool IsEdit { get; set; }
        public List<ManageMailTypeClass> lstData { get; set; } = new List<ManageMailTypeClass>();
        public List<cDropDown> lstTypeMail { get; set; } = new List<cDropDown>();
        public List<cDropDown> lstGroupMail { get; set; } = new List<cDropDown>();
        public int PagePrevious { get; internal set; }
        public int PageNext { get; internal set; }
        public int PageNumber { get; internal set; }
        public int PageSize { get; internal set; }
        public int PagerCount { get; internal set; }
        public int TotalCount { get; internal set; }
    }
}

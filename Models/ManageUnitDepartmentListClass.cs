using static PMSS.Extensions.Common.AllClass;

namespace Demo1.Models
{
    public class ManageUnitDepartmentListClass
    {
        public string SectionCode { get; set; } = null!;
        public string SectionName { get; set; } = null!;
        public string SectionGroup { get; set; } = null!;
        public string? sCreate { get; set; }
        public DateTime? dCreateDate { get; set; }
        public string? sUpdate { get; set; }
        public DateTime? dUpdateDate { get; set; }
        public bool IsDelete { get; set; }
        public List<ManageUnitDepartmentClass> lstData { get; set; } = new List<ManageUnitDepartmentClass>();
        public List<cDropDown> lstGroupSection { get; set; } = new List<cDropDown>();
        public int PagePrevious { get; internal set; }
        public int PageNext { get; internal set; }
        public int PageNumber { get; internal set; }
        public int PageSize { get; internal set; }
        public int PagerCount { get; internal set; }
        public int TotalCount { get; internal set; }
    }
}

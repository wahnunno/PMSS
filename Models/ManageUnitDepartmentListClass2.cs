using System.ComponentModel.DataAnnotations;
using static PMSS.Extensions.Common.AllClass;

namespace Demo1.Models
{
    public class ManageUnitDepartmentListClass2
    {
        [Required]
        public string SectionCode { get; set; }
        [Required]
        public string SectionName { get; set; }
        [Required]
        public string SectionGroup { get; set; }

        public List<ManageUnitDepartmentClass> lstData { get; set; } = new List<ManageUnitDepartmentClass>();
        public List<cDropDown> lstGroupSection { get; set; } = new List<cDropDown>();
        public int PagePrevious { get; internal set; } = 0;
        public int PageNext { get; internal set; } = 0;
        public int PageNumber { get; internal set; } = 0;
        public int PageSize { get; internal set; } = 0;
        public int PagerCount { get; internal set; } = 0;
        public int TotalCount { get; internal set; } = 0;
    }
}

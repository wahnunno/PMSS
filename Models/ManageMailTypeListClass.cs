namespace Demo1.Models
{
    public class ManageMailTypeListClass
    {
        public decimal ID { get; set; }
        public string Type_Name { get; set; } = null!;
        public decimal Type_Pay { get; set; }
        public string Type_Mail1 { get; set; } = null!;
        public string Type_InOut { get; set; } = null!;
        public List<ManageUnitDepartmentClass> lstData { get; set; } = new List<ManageUnitDepartmentClass>();
        //public List<cDropDown> lstGroupSection { get; set; } = new List<cDropDown>();
        public int PagePrevious { get; internal set; }
        public int PageNext { get; internal set; }
        public int PageNumber { get; internal set; }
        public int PageSize { get; internal set; }
        public int PagerCount { get; internal set; }
        public int TotalCount { get; internal set; }
    }
}

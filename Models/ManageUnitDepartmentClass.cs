namespace Demo1.Models
{
    public class ManageUnitDepartmentClass
    {
        public int nNo { get; set; }
        public string SectionCode { get; set; } = null!;
        public string SectionName { get; set; } = null!;
        public string SectionGroup { get; set; }
        public string SectionGroupName { get; set; }
        public string? sCreate { get; set; }
        public DateTime? dCreateDate { get; set; }
        public string? sUpdate { get; set; }
        public DateTime? dUpdateDate { get; set; }
        public bool IsDelete { get; set; }
    }
}

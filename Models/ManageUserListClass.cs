namespace Demo1.Models
{
    public class ManageUserListClass
    {
        public int nID { get; set; }
        public string OAUserID { get; set; }
        public string Name { get; set; }
        public DateTime dStartDate { get; set; }
        public DateTime dEndDate { get; set; }
        public string sStartDate { get; set; }
        public string sEndDate { get; set; }
        public bool IsEdit { get; set; }
        public List<ManageUserClass> lstData { get; set; } = new List<ManageUserClass>();
        public int PagePrevious { get; internal set; }
        public int PageNext { get; internal set; }
        public int PageNumber { get; internal set; }
        public int PageSize { get; internal set; }
        public int PagerCount { get; internal set; }
        public int TotalCount { get; internal set; }
    }
}

namespace Demo1.Models
{
    public class ManageUserListClass
    {
        public string? sOAUserID { get; set; }
        public string? sName { get; set; }
        public DateTime? dStartDate { get; set; }
        public string sStartDate { get; set; }
        public DateTime? dEndDate { get; set; }
        public string sEndDate { get; set; }
        public bool IsDelete { get; set; }
        public List<ManageUserClass>? lstData { get; set; }
    }
}

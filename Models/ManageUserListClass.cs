namespace Demo1.Models
{
    public class ManageUserListClass
    {
        public int nID { get; set; }
        public string sOAUserID { get; set; } = null!;
        public string sRole { get; set; } = null!;
        public string sName { get; set; } = "";
        public string? sDep { get; set; }
        public string? sEmail { get; set; }
        public string? sTel { get; set; }
        public DateTime dStartDate { get; set; }
        public string sStartDate { get; set; } = null!;
        public DateTime dEndDate { get; set; }
        public string sEndDate { get; set; } = null!;
        public string? sCreate { get; set; }
        public DateTime? dCreateDate { get; set; }
        public string? sUpdate { get; set; }
        public DateTime? dUpdateDate { get; set; }
        public bool IsDelete { get; set; }
        public List<ManageUserClass> lstData { get; set; } = new List<ManageUserClass>();
    }
}

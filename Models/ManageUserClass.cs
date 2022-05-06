namespace Demo1.Models
{
    public class ManageUserClass
    {
        public int nNo { get; set; }
        public string sOAUserID { get; set; } = null!;
        public string sRole { get; set; } = null!;
        public string? sName { get; set; }
        public string? sDep { get; set; }
        public string? sEmail { get; set; }
        public string? sTel { get; set; }
        public DateTime? dStartDate { get; set; }
        public string sStartDate { get; set; }
        public DateTime? dEndDate { get; set; }
        public string sEndDate { get; set; }
        public bool IsDelete { get; set; }
    }
}

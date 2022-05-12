﻿namespace Demo1.Models
{
    public class ManageUserClass
    {
        public int nNo { get; set; }
        public int nID { get; set; }
        public string sOAUserID { get; set; } = null!;
        public string? sRole { get; set; }
        public string sName { get; set; } = null!;
        public string? sDep { get; set; }
        public string? sEmail { get; set; }
        public string? sTel { get; set; }
        public DateTime dStartDate { get; set; }
        public DateTime dEndDate { get; set; }
        public string? sCreate { get; set; }
        public DateTime? dCreateDate { get; set; }
        public string? sUpdate { get; set; }
        public DateTime? dUpdateDate { get; set; }
        public bool IsDelete { get; set; }
        public string sStartDate { get; set; } = null!;
        public string sEndDate { get; set; } = null!;
    }
}

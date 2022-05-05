using System;
using System.Collections.Generic;

namespace Demo1.Models.PSOrderContext
{
    public partial class UserMaintain
    {
        public int nNo { get; set; }
        public string sOAUserID { get; set; } = null!;
        public string sRole { get; set; } = null!;
        public string? sName { get; set; }
        public string? sDep { get; set; }
        public string? sEmail { get; set; }
        public string? sTel { get; set; }
        public DateTime? dStartDate { get; set; }
        public DateTime? dEndDate { get; set; }
        public string? sCreate { get; set; }
        public DateTime? dCreateDate { get; set; }
        public string? sUpdate { get; set; }
        public DateTime? dUpdateDate { get; set; }
        public bool IsDelete { get; set; }
    }
}

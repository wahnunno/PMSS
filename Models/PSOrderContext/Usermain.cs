using System;
using System.Collections.Generic;

namespace Demo1.Models.PSOrderContext
{
    public partial class Usermain
    {
        public decimal No { get; set; }
        public string OAUserID { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Dep { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Tel { get; set; } = null!;
        public DateTime dStartDate { get; set; }
        public DateTime dEndDate { get; set; }
        public string? sCreate { get; set; }
        public DateTime? dCreateDate { get; set; }
        public string? sUpdate { get; set; }
        public DateTime? dUpdateDate { get; set; }
        public bool? IsDelete { get; set; }
    }
}

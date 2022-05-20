using System;
using System.Collections.Generic;

namespace Demo1.Models.PSOrderContext
{
    public partial class MT_TypeInOut
    {
        public int nID { get; set; }
        public string? sTypeInOutName { get; set; }
        public string cActive { get; set; } = null!;
        public string? sCreate { get; set; }
        public DateTime? dCreateDate { get; set; }
        public string? sUpdate { get; set; }
        public DateTime? dUpdateDate { get; set; }
        public bool IsDelete { get; set; }
    }
}

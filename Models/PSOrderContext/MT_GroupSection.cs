using System;
using System.Collections.Generic;

namespace Demo1.Models.PSOrderContext
{
    public partial class MT_GroupSection
    {
        public int nGroupSectionID { get; set; }
        public string? sGroupSectionName { get; set; }
        public string cActive { get; set; } = null!;
    }
}

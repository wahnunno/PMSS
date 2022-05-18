using System;
using System.Collections.Generic;

namespace Demo1.Models.PSOrderContext
{
    public partial class Section
    {
        public string SectionCode { get; set; } = null!;
        public string SectionName { get; set; } = null!;
        /// <summary>
        /// MT_GroupSection =&gt; กลุ่มงาน : 1=สาขา,4=ส่วน/ฝ่าย/สำนัก,5=ต่างประเทศ,6=กองทุน
        /// </summary>
        public string SectionGroup { get; set; } = null!;
        public string? sCreate { get; set; }
        public DateTime? dCreateDate { get; set; }
        public string? sUpdate { get; set; }
        public DateTime? dUpdateDate { get; set; }
        public bool IsDelete { get; set; }
    }
}

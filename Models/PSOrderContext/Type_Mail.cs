using System;
using System.Collections.Generic;

namespace Demo1.Models.PSOrderContext
{
    public partial class Type_Mail
    {
        public decimal ID { get; set; }
        public string Type_Name { get; set; } = null!;
        public decimal Type_Pay { get; set; }
        /// <summary>
        /// MT_TypeMail =&gt; 0 : จดหมายธรรมดา,1 : จดหมายลงทะเบียน
        /// </summary>
        public string Type_Mail1 { get; set; } = null!;
        public int? nTypeInOutID { get; set; }
        /// <summary>
        /// MT_TypeInOut =&gt; 1 : ไปรษณียภัณฑ์ในประเทศ,2 : ไปรษณียภัณฑ์ต่างประเทศ
        /// </summary>
        public string Type_InOut { get; set; } = null!;
        public string? sCreate { get; set; }
        public DateTime? dCreateDate { get; set; }
        public string? sUpdate { get; set; }
        public DateTime? dUpdateDate { get; set; }
        public bool? IsDelete { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Demo1.Models.PSOrderContext
{
    public partial class Normal
    {
        public decimal ID { get; set; }
        public string Sender { get; set; } = null!;
        public string? DivName { get; set; }
        public string Ref { get; set; } = null!;
        public string? Res { get; set; }
        public decimal? Num { get; set; }
        public string? EndT { get; set; }
        public decimal Pay { get; set; }
        public string RC { get; set; } = null!;
        public string? UserID { get; set; }
        public string? Lot { get; set; }
        public string? TypeLetter { get; set; }
        public string? TypeOfLetter { get; set; }
        public string? Datetime { get; set; }
        public DateTime? dDate { get; set; }
        public string? Period { get; set; }
        public string? Product { get; set; }
        public int? rload { get; set; }
        public string? sCreate { get; set; }
        public DateTime? dCreateDate { get; set; }
        public string? sUpdate { get; set; }
        public DateTime? dUpdateDate { get; set; }
        public bool? IsDelete { get; set; }
    }
}

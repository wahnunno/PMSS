using System;
using System.Collections.Generic;

namespace Demo1.Models.PSOrderContext
{
    public partial class KeyDoc
    {
        public int ID { get; set; }
        public string? DivCode { get; set; }
        public decimal? Num { get; set; }
        public decimal? Pay { get; set; }
        public string? Datetime { get; set; }
        public string? Period { get; set; }
    }
}

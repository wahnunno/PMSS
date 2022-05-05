using System;
using System.Collections.Generic;

namespace Demo1.Models.PSOrderContext
{
    public partial class Round
    {
        public int ID { get; set; }
        public int? Round1 { get; set; }
        public string? DayTrip { get; set; }
        public string? Period { get; set; }
    }
}

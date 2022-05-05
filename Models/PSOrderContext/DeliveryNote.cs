using System;
using System.Collections.Generic;

namespace Demo1.Models.PSOrderContext
{
    public partial class DeliveryNote
    {
        public decimal ID { get; set; }
        public string Re_Date { get; set; } = null!;
        public string Re_Div { get; set; } = null!;
        public string Re_DivName { get; set; } = null!;
        public decimal? Re_DeliNote { get; set; }
        public string? Re_Recipient { get; set; }
        public string Lot { get; set; } = null!;
        public string UserID { get; set; } = null!;
        public string Re_time { get; set; } = null!;
    }
}

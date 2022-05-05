using System;
using System.Collections.Generic;

namespace Demo1.Models.PSOrderContext
{
    public partial class Return_Table
    {
        public decimal ID { get; set; }
        public string Re_Date { get; set; } = null!;
        public string Re_Div { get; set; } = null!;
        public string Re_DivName { get; set; } = null!;
        public string? Re_DeliNote { get; set; }
        public string? Re_Barcode { get; set; }
        public string? Re_Ref { get; set; }
        public string Re_Group { get; set; } = null!;
        public string Re_Type { get; set; } = null!;
        public decimal Re_Count { get; set; }
        public string? Re_Sender { get; set; }
        public string? Re_Reci { get; set; }
        public string? Re_Province { get; set; }
        public string? Re_Country { get; set; }
        public string? Re_Cause { get; set; }
        public string UserID { get; set; } = null!;
        public string Re_time { get; set; } = null!;
        public string Lot { get; set; } = null!;
    }
}

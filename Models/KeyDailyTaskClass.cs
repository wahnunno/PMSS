using System.ComponentModel.DataAnnotations;

namespace Demo1.Models
{
    public class KeyDailyTaskClass
    {
        public int nNo { get; set; }
        public string sDivCode { get; set; }
        public string sDetail { get; set; }
        public int nQuantity { get; set; }
        public decimal nAmount { get; set; }
    }
}

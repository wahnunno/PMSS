using System.ComponentModel.DataAnnotations;

namespace Demo1.Models
{
    public class ManageUserClass
    {
        public int nNo { get; set; }
        public int nID { get; set; }
        public string OAUserID { get; set; } 
        public string Name { get; set; } 
        public DateTime dStartDate { get; set; }
        public DateTime dEndDate { get; set; }
        public string sStartDate { get; set; } 
        public string sEndDate { get; set; } 
    }
}

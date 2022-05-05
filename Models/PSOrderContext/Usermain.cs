using System;
using System.Collections.Generic;

namespace Demo1.Models.PSOrderContext
{
    public partial class Usermain
    {
        public decimal No { get; set; }
        public string OAUserID { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Dep { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Tel { get; set; } = null!;
    }
}

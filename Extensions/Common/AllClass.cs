using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Extensions.Common.STResultAPI;

namespace PMSS.Extensions.Common
{
    public class AllClass
    {
        public class cResult_Autocomplete : ResultAPI
        {
            public List<cAutocomplete> lstAutocomplete { get; set; }
        }

        public class cAutocomplete : ResultAPI
        {
            public string value { get; set; }
            public string label { get; set; }
        }

        public class ResultAD : ResultAPI
        {
            public string sUsername { get; set; }
            public string sPWD { get; set; }
        }

        public class GlobalData
        {
            public  List<string> MonthSmallName { get; set; } = new List<string> { "ม.ค.", "ก.พ.", "มี.ค.", "เม.ย.", "พ.ค.", "มิ.ย.", "ก.ค.", "ส.ค.", "ก.ย.", "ต.ค.", "พ.ย.", "ธ.ค." };
            public  List<string> MonthsFullName { get; set; } = new List<string> { "มกราคม", "กุมภาพันธ์", "มีนาคม", "เมษายน", "พฤษภาคม", "มิถุนายน", "กรกฎาคม", "สิงหาคม", "กันยายน", "ตุลาคม", "พฤศจิกายน", "ธันวาคม" };
        }
    }
}

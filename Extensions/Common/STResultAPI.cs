using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Extensions.Common.STResultAPI
{
    public class ResultAPI
    {
        public string Status { get; set; }
        public string Message { get; set; }
    }

    public class ResultStatus
    {
        public const string Duplicate = "Duplicate Data";
        public const string Error = "Error";
        public const string Success = "Success";
        public const string Failed = "Failed";
    }

    public class ResultStatusText
    {
        public const string Duplicate = "ข้อมูลได้ถูกบันทึกแล้ว";
        public const string NotFound = "ไม่พบข้อมูล";
        public const string Success = "บันทึกข้อมูลสำเร็จ";
        public const string Unsuccess = "บันทึกข้อมูลไม่สำเร็จ";
    }

    public class ResultDropDown
    {
        public string value { get; set; }
        public string label { get; set; }
    }
}

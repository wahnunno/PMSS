using Demo1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System;
using System.IO;
using Demo1.Models.PSOrderContext;
using Extensions.Common.STExtension;
using Extensions.Common.STResultAPI;

namespace Demo1.Controllers
{
    public class PayrollDataController : Controller
    {
        private readonly PSOrderContext DB;
        public PayrollDataController(PSOrderContext db)
        {
            DB = db;
        }

        public IActionResult PayrollData(PayrollDataListClass Obj, int? pageNumber)
        {
            PayrollDataListClass Model = new PayrollDataListClass();
            List<PayrollDataClass> lstData = new List<PayrollDataClass>();
            lstData = GetData(Obj);
            DownloadTextFile(lstData);
            Model.lstData = lstData;

            Model.PageNumber = (pageNumber == null ? 1 : Convert.ToInt32(pageNumber));
            Model.PageSize = 10;

            Model.PagePrevious = Model.PageNumber - 1;
            Model.PageNext = Model.PageNumber + 1;

            if (lstData != null)
            {
                Model.lstData = lstData.OrderBy(x => x.nNo)
                                    .Skip(Model.PageSize * (Model.PageNumber - 1))
                                    .Take(Model.PageSize).ToList();
                Model.TotalCount = lstData.Count;
                Model.PagerCount = ((Model.TotalCount / Model.PageSize) -
                                    (Model.TotalCount % Model.PageSize == 0 ? 1 : 0)) + 1;
            }

            return View(Model);
        }

        public List<PayrollDataClass> GetData(PayrollDataListClass Obj)
        {
            List<PayrollDataClass> lstData = new List<PayrollDataClass>();

            DateTime? dDateFilter = ("01/" + Obj.nMonth + "/" + Obj.nYear).ToDateFromString();
            if (dDateFilter.HasValue)
            {
                var lstNormal = DB.Normals.Where(w => w.dDate.Value.Month == dDateFilter.Value.Month && w.dDate.Value.Year == dDateFilter.Value.Year).ToList();
                if (lstNormal.Count > 0)
                {
                    var lstGroup = lstNormal.GroupBy(g => g.Sender).ToList();
                    int i = 1;
                    string sSEQ = "";

                    foreach (var Group in lstGroup)
                    {
                        string str = i + "";
                        for (int j = 0; j <= (5 - str.Length); j++)
                        {
                            sSEQ += "0";
                        }
                        sSEQ += str;

                        string sACC = "";
                        string sDIV = "";
                        decimal nAMT = 0;
                        string sAMT = "";
                        foreach (var Item in Group)
                        {
                            sDIV = Item.Sender.Trim();
                            var lstSection = DB.Sections.FirstOrDefault(f => f.SectionCode.Trim() == Item.Sender.Trim());
                            if (lstSection != null)
                            {
                                switch (lstSection.SectionGroup.Trim())
                                {
                                    case "1":
                                    case "4":
                                        sACC = "828213";
                                        break;
                                    case "5":
                                        sACC = "381145";
                                        break;
                                    case "6":
                                        sACC = "275013";
                                        sDIV = "002600";
                                        break;
                                }
                            }
                            nAMT += Item.Pay;
                        }
                        string[] ArrStr = (nAMT + "").Split('.');
                        if (ArrStr.Length > 0)
                        {
                            sAMT += ArrStr[0] + ArrStr[1];
                        }
                        string StrAMT = sAMT.PadLeft(13, '0');
                        sAMT = StrAMT;

                        lstData.Add(new PayrollDataClass
                        {
                            nNo = i++,
                            ACC = sACC,
                            REG = "8002",
                            SEQ = sSEQ,
                            DIV = sDIV,
                            AMT = sAMT,
                            SIGN = " ",
                            VENDOR = "0000005068",
                            DETAIL = "                                  ",
                            DEPT = "POST"
                        });
                        sACC = "";
                        sSEQ = "";
                        sDIV = "";
                        nAMT = 0;
                    }
                }
            }
            return lstData;
        }

        public void DownloadTextFile(List<PayrollDataClass> lstData)
        {
            try
            {
                string path = @"C:\Users\2521116455\Downloads\POST_" + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".txt";
                if (!System.IO.File.Exists(path))
                {
                    if (lstData.Count > 0)
                    {
                        using (StreamWriter sw = System.IO.File.CreateText(path))
                        {
                            foreach (var Item in lstData)
                            {
                                sw.WriteLine(Item.ACC + Item.REG + Item.SEQ + Item.DIV + Item.AMT + Item.SIGN + Item.VENDOR + Item.DETAIL + Item.DEPT);
                            }
                            TempData["Success"] = "Action Completed', 'Data is already saved.";
                            Redirect("PayrollData");
                        }
                    }
                }

                if (lstData.Count > 0)
                {
                    using (StreamReader sr = System.IO.File.OpenText(path))
                    {
                        string s;
                        while ((s = sr.ReadLine()) != null)
                        {
                            Console.WriteLine(s);
                            TempData["Success"] = "Action Completed', 'Data is already saved.";
                            Redirect("PayrollData");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
        }
    }
}

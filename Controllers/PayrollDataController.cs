using Demo1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System;
using System.IO;
using Demo1.Models.PSOrderContext;
using Extensions.Common.STExtension;

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
            List<PayrollDataClass> lstData = GetData(Obj);
            Model.lstData= lstData;

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

                        lstData.Add(new PayrollDataClass
                        {
                            nNo = i++,
                            ACC = sACC,
                            REG = "8002",
                            SEQ = sSEQ,
                            DIV = sDIV,
                            AMT = nAMT.ToString("#.##"),
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
                    //foreach (var Item in lstData)
                    //{
                    //    string StrPost = Item.ACC + Item.REG + Item.SEQ + Item.DIV + Item.AMT + Item.SIGN + Item.VENDOR + Item.DETAIL + Item.DEPT;
                    //}
                }
            }
            return lstData;
        }
        public string DownloadTextFile(PayrollDataListClass Obj)
        {
            //// Get the current directory.
            //string path = Directory.GetCurrentDirectory();
            //string target = @"C:\temp";
            //Console.WriteLine("The current directory is {0}", path);
            //if (!Directory.Exists(target))
            //{
            //    Directory.CreateDirectory(target);
            //}

            //// Change the current directory.
            //Environment.CurrentDirectory = (target);
            //if (path.Equals(Directory.GetCurrentDirectory()))
            //{
            //    Console.WriteLine("You are in the temp directory.");
            //}
            //else
            //{
            //    Console.WriteLine("You are not in the temp directory.");
            //}

            string path = @"C:\Users\2521116455\Downloads\MyTest.txt";
            if (!System.IO.File.Exists(path))
            {
                using (StreamWriter sw = System.IO.File.CreateText(path))
                {
                    sw.WriteLine("Hello");
                    sw.WriteLine("And");
                    sw.WriteLine("Welcome");
                }
            }

            using (StreamReader sr = System.IO.File.OpenText(path))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    Console.WriteLine(s);
                }
            }

            return "";
        }
    }
}

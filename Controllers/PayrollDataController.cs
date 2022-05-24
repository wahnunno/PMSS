using Demo1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System;
using System.IO;

namespace Demo1.Controllers
{
    public class PayrollDataController : Controller
    {
        public IActionResult PayrollData(PayrollDataClass Obj)
        {
            PayrollDataClass Model = new PayrollDataClass();
            string Str = GetData(Obj);
            string Str1 = DownloadTextFile(Obj);
            return View(Model);
        }

        public string GetData(PayrollDataClass Obj)
        {
            return "";
        }
        public string DownloadTextFile(PayrollDataClass Obj)
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

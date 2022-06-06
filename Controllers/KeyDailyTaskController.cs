using Demo1.Models;
using Demo1.Models.PSOrderContext;
using Extensions.Common.STExtension;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using static PMSS.Extensions.Common.AllClass;

namespace Demo1.Controllers
{
    public class KeyDailyTaskController : Controller
    {
        private readonly PSOrderContext DB;
        public KeyDailyTaskController(PSOrderContext db)
        {
            DB = db;
        }

        public IActionResult KeyDailyTaskForm(KeyDailyTaskListClass Obj, int? pageNumber)
        {
            KeyDailyTaskListClass Model = new KeyDailyTaskListClass();
            List<cDropDown> lstDiv = new List<cDropDown>();
            List<cDropDown> lstGroupMail = new List<cDropDown>();
            List<cDropDownTypeMail> lstTypeMail = new List<cDropDownTypeMail>();
            List<KeyDailyTask_Normal_Class> lstNormal = new List<KeyDailyTask_Normal_Class>();
            List<KeyDailyTask_Register_Class> lstRegister = new List<KeyDailyTask_Register_Class>();

            lstDiv = GetDropDownDivision();
            lstGroupMail = GetDropDownGroupMail();
            lstTypeMail = GetDropDownTypeMail();
            //lstNormal = GetData_Normal(Obj);
            //lstRegister = GetData_Register(Obj);

            Model.lstDiv = lstDiv;
            Model.lstGroupMail = lstGroupMail;
            Model.lstTypeMail = lstTypeMail;
            Model.lstNormal = lstNormal;
            Model.lstRegister = lstRegister;

            Model.PageNumber = (pageNumber == null ? 1 : Convert.ToInt32(pageNumber));
            Model.PageSize = 10;
            Model.PagePrevious = Model.PageNumber - 1;
            Model.PageNext = Model.PageNumber + 1;

            if (lstNormal != null)
            {
                Model.lstNormal = lstNormal.OrderBy(x => x.nNo)
                                    .Skip(Model.PageSize * (Model.PageNumber - 1))
                                    .Take(Model.PageSize).ToList();
                Model.TotalCount = lstNormal.Count;
                Model.PagerCount = ((Model.TotalCount / Model.PageSize) -
                                    (Model.TotalCount % Model.PageSize == 0 ? 1 : 0)) + 1;
            }

            if (lstRegister != null)
            {
                Model.lstRegister = lstRegister.OrderBy(x => x.nNo)
                                    .Skip(Model.PageSize * (Model.PageNumber - 1))
                                    .Take(Model.PageSize).ToList();
                Model.TotalCount = lstRegister.Count;
                Model.PagerCount = ((Model.TotalCount / Model.PageSize) -
                                    (Model.TotalCount % Model.PageSize == 0 ? 1 : 0)) + 1;
            }

            return View(Model);
        }

        public List<cDropDown> GetDropDownDivision()
        {
            List<cDropDown> lstData = new List<cDropDown>();
            lstData = DB.Sections.Select(s =>
                   new cDropDown
                   {
                       value = s.SectionCode + "",
                       label = s.SectionCode + " : " + s.SectionName,
                   }).ToList();
            return lstData;
        }

        public List<cDropDownTypeMail> GetDropDownTypeMail()
        {
            List<cDropDownTypeMail> lstData = new List<cDropDownTypeMail>();
            lstData = DB.Type_Mails.Where(w => !w.IsDelete.Value).Select(s =>
                   new cDropDownTypeMail
                   {
                       value = s.ID + "",
                       label = s.Type_Name + "",
                       sTypeMailID = s.Type_Mail1 + ""
                   }).ToList();
            return lstData;
        }

        public List<cDropDown> GetDropDownGroupMail()
        {
            List<cDropDown> lstData = new List<cDropDown>();
            lstData = DB.MT_TypeInOuts.Where(w => w.cActive == "Y" && !w.IsDelete).Select(s =>
                  new cDropDown
                  {
                      value = s.nID + "",
                      label = s.sTypeInOutName + ""
                  }).ToList();
            return lstData;
        }

        public List<KeyDailyTask_Normal_Class> GetData_Normal(cData Obj)
        {
            List<KeyDailyTask_Normal_Class> lstData = new List<KeyDailyTask_Normal_Class>();
            DateTime? dDateFilter = Obj.sDate.ToDateFromString();
            if (dDateFilter.HasValue)
            {
                var lst = DB.Normals.Where(w => w.dDate == dDateFilter.Value && w.Period == Obj.sTime && w.nTypeInOutID == Obj.sGroupMail.ToInt() && w.nTypeMailID == Obj.sTypeMail.ToInt() && w.IsDelete.Value == false).ToList();
                if (lst.Count > 0)
                {
                    int i = 1;
                    foreach (var Item in lst)
                    {
                        lstData.Add(new KeyDailyTask_Normal_Class
                        {
                            nNo = i++,
                            nID = (Item.ID + "").ToInt(),
                            sDivCode = Item.Sender + " : " + Item.DivName,
                            sDetail = Item.Ref,
                            nQuantity = (Item.Num + "").ToInt(),
                            nAmount = Item.Pay
                        });
                    }
                }
            }

            return lstData;
        }

        public List<KeyDailyTask_Register_Class> GetData_Register(KeyDailyTaskListClass Obj)
        {
            List<KeyDailyTask_Register_Class> lstData = new List<KeyDailyTask_Register_Class>();
            DateTime? dDateFilter = Obj.sDate.ToDateFromString();
            if (dDateFilter.HasValue)
            {
                var lst = DB.Normals.Where(w => w.dDate == dDateFilter.Value && w.nTypeInOutID == Obj.sGroupMail.ToInt() && w.nTypeMailID == Obj.sTypeMail.ToInt() && w.IsDelete.Value == false).ToList();
                if (lst.Count > 0)
                {
                    int i = 1;
                    foreach (var Item in lst)
                    {
                        lstData.Add(new KeyDailyTask_Register_Class
                        {
                            nNo = i++,
                            nID = (Item.ID + "").ToInt(),
                            sReceiverName = Item.Res + "",
                            sDestination = Item.EndT + "",
                            sRCNumber = Item.RC,
                            nAmount = Item.Pay,
                            sDivCode = Item.Sender + " : " + Item.DivName,
                            sRef = Item.Ref,
                        });
                    }
                }
            }

            return lstData;
        }

        [HttpPost]
        public IActionResult KeyDailyTaskForm(KeyDailyTaskListClass Obj)
        {
            try
            {
                string sTypeLetter = "";
                var lstTypeInOuts = DB.MT_TypeInOuts.FirstOrDefault(f => f.nID == Obj.sGroupMail.ToInt() && f.IsDelete == false);
                if (lstTypeInOuts != null)
                {
                    sTypeLetter = lstTypeInOuts.sTypeInOutName.TrimEnd() + "";
                }

                int nTypeMail_ID = 0;
                string sTypeOfLetter = "";
                var lstTypeMail = DB.Type_Mails.FirstOrDefault(f => f.ID == Obj.sTypeMail.ToInt() && f.IsDelete == false);
                if (lstTypeMail != null)
                {
                    nTypeMail_ID = (lstTypeMail.Type_Mail1 + "").ToInt();
                    var lstMS = DB.MT_TypeMails.FirstOrDefault(f => f.nID == lstTypeMail.Type_Mail1.ToInt() && !f.IsDelete);
                    if (lstMS != null)
                    {
                        sTypeOfLetter = lstMS.sTypeMailName.TrimEnd() + "";
                    }
                }

                string sSender = "";
                string sDivName = "";
                string sRef = "";
                decimal nAmount = 0;
                string sRCNumber = "";
                string[] ArrStr;
                if (Obj.IsLot)
                {
                    ArrStr = Obj.sSender1.Split(" : ");
                    if (ArrStr.Length > 0)
                    {
                        sSender = ArrStr[0];
                    }
                    else
                    {
                        sSender = Obj.sSender1;
                    }
                    var lstDiv = DB.Sections.FirstOrDefault(f => f.SectionCode == sSender);
                    if (lstDiv != null)
                    {
                        sDivName = lstDiv.SectionName.TrimEnd();
                    }
                    sRef = Obj.sRef1 + "";
                    nAmount = Obj.nAmount1;
                }
                else
                {
                    switch (nTypeMail_ID)
                    {
                        case 0:
                            ArrStr = Obj.sSender1.Split(" : ");
                            if (ArrStr.Length > 0)
                            {
                                sSender = ArrStr[0];
                            }
                            else
                            {
                                sSender = Obj.sSender1;
                            }
                            var lstDiv = DB.Sections.FirstOrDefault(f => f.SectionCode == sSender);
                            if (lstDiv != null)
                            {
                                sDivName = lstDiv.SectionName.TrimEnd();
                            }
                            sRef = Obj.sRef1 + "";
                            nAmount = Obj.nAmount1;
                            break;
                        case 1:
                            ArrStr = Obj.sSender2.Split(" : ");
                            if (ArrStr.Length > 0)
                            {
                                sSender = ArrStr[0];
                            }
                            else
                            {
                                sSender = Obj.sSender2;
                            }
                            var lstDiv2 = DB.Sections.FirstOrDefault(f => f.SectionCode == sSender);
                            if (lstDiv2 != null)
                            {
                                sDivName = lstDiv2.SectionName.TrimEnd();
                            }
                            sRef = Obj.sRef2 + "";
                            nAmount = Obj.nAmount2;
                            sRCNumber = Obj.sRCNumber1 + Obj.sRCNumber2 + Obj.sRCNumber3 + "";
                            break;
                    }
                }

                var UPT = DB.Normals.FirstOrDefault(f => f.ID == Obj.nID && f.IsDelete == false);
                if (UPT != null)
                {
                    UPT.Sender = sSender.TrimEnd();
                    UPT.DivName = sDivName.TrimEnd();
                    UPT.Ref = sRef.TrimEnd();
                    UPT.Res = Obj.sReceiver != null ? Obj.sReceiver.TrimEnd() : "";
                    UPT.Num = Obj.nQuantity;
                    UPT.EndT = Obj.sPostal != null ? Obj.sPostal.TrimEnd() : "";
                    UPT.Pay = nAmount;
                    UPT.RC = sRCNumber.TrimEnd();
                    UPT.UserID = null;
                    UPT.Lot = Obj.IsLot ? "1" : "0";
                    UPT.nTypeInOutID = Obj.sGroupMail.ToInt();
                    UPT.TypeLetter = sTypeLetter;
                    UPT.nTypeMailID = Obj.sTypeMail.ToInt();
                    UPT.TypeOfLetter = sTypeOfLetter;
                    UPT.Datetime = Obj.sDate != null ? Obj.sDate.TrimEnd() : "";
                    UPT.dDate = Obj.sDate.ToDateFromString();
                    UPT.Period = Obj.sTime != null ? Obj.sTime.TrimEnd() : "";
                    UPT.Product = null;
                    UPT.rload = null;
                    UPT.sUpdate = null;
                    UPT.dUpdateDate = DateTime.Now;
                    UPT.IsDelete = false;
                    DB.SaveChanges();

                    TempData["Success"] = "Action Completed', 'Data is already saved.";
                    Redirect("KeyDailyTaskForm");
                }
                else
                {
                    Normal CRT = new Normal();
                    CRT.Sender = sSender.TrimEnd();
                    CRT.DivName = sDivName.TrimEnd();
                    CRT.Ref = sRef.TrimEnd();
                    CRT.Res = Obj.sReceiver != null ? Obj.sReceiver.TrimEnd() : "";
                    CRT.Num = Obj.nQuantity;
                    CRT.EndT = Obj.sPostal != null ? Obj.sPostal.TrimEnd() : "";
                    CRT.Pay = nAmount;
                    CRT.RC = sRCNumber.TrimEnd();
                    CRT.UserID = null;
                    CRT.Lot = Obj.IsLot ? "1" : "0";
                    CRT.nTypeInOutID = Obj.sGroupMail.ToInt();
                    CRT.TypeLetter = sTypeLetter;
                    CRT.nTypeMailID = Obj.sTypeMail.ToInt();
                    CRT.TypeOfLetter = sTypeOfLetter;
                    CRT.Datetime = Obj.sDate != null ? Obj.sDate.TrimEnd() : "";
                    CRT.dDate = Obj.sDate.ToDateFromString();
                    CRT.Period = Obj.sTime != null ? Obj.sTime.TrimEnd() : "";
                    CRT.Product = null;
                    CRT.rload = null;
                    CRT.sCreate = null;
                    CRT.dCreateDate = DateTime.Now;
                    CRT.sUpdate = null;
                    CRT.dUpdateDate = DateTime.Now;
                    CRT.IsDelete = false;
                    DB.Normals.Add(CRT);
                    DB.SaveChanges();

                    TempData["Success"] = "Action Completed', 'Data is already saved.";
                    Redirect("KeyDailyTaskForm");
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return View(Obj);
        }

        public class cData
        {
            public string sDate { get; set; }
            public string sTime { get; set; }
            public string sGroupMail { get; set; }
            public string sTypeMail { get; set; }
        }
    }
}

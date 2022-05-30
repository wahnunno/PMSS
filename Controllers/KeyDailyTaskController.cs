﻿using Demo1.Models;
using Demo1.Models.PSOrderContext;
using Extensions.Common.STExtension;
using Microsoft.AspNetCore.Mvc;
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
            List<cDropDown> lstGroupMail = new List<cDropDown>();
            List<cDropDownTypeMail> lstTypeMail = new List<cDropDownTypeMail>();
            List<KeyDailyTask_Normal_Class> lstNormal = new List<KeyDailyTask_Normal_Class>();
            List<KeyDailyTask_Register_Class> lstRegister = new List<KeyDailyTask_Register_Class>();
            lstGroupMail = GetDropDownGroupMail();
            lstTypeMail = GetDropDownTypeMail();
            lstNormal = GetData_Normal(Obj);
            lstRegister = GetData_Register(Obj);
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

        public List<KeyDailyTask_Normal_Class> GetData_Normal(KeyDailyTaskListClass Obj)
        {
            List<KeyDailyTask_Normal_Class> lstData = new List<KeyDailyTask_Normal_Class>();
            DateTime? dDateFilter = Obj.sDate.ToDateFromString();
            if (dDateFilter.HasValue)
            {
                var lst = DB.Normals.Where(w => w.dDate == dDateFilter.Value && w.nTypeInOutID == Obj.sGroupMail.ToInt() && w.nTypeMailID == Obj.sTypeMail.ToInt() && w.IsDelete.Value == false).ToList();
                if (lst.Count > 0)
                {
                    int i = 1;
                    foreach (var Item in lst)
                    {
                        lstData.Add(new KeyDailyTask_Normal_Class
                        {
                            nNo = i++,
                            sDivCode = Item.Sender,
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
                            sReceiverName = Item.Res,
                            sDestination = Item.EndT,
                            sRCNumber = Item.RC,
                            nAmount = Item.Pay,
                            sDivCode = Item.Sender,
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
                var UPT = DB.Normals.FirstOrDefault(f => f.ID == Obj.nID && f.IsDelete == false);
                if (UPT != null)
                {
                    UPT.Sender = Obj.sSender;
                    //UPT.DivName = Obj.nID;
                    UPT.Ref = Obj.sRef;
                    UPT.Res = Obj.sReceiver;
                    UPT.Num = Obj.nQuantity;
                    UPT.EndT = Obj.nPostal + "";
                    UPT.Pay = Obj.nAmount;
                    UPT.RC = Obj.sRCNumber;
                    UPT.UserID = null;
                    //UPT.Lot = Obj.;
                    UPT.nTypeInOutID = Obj.sGroupMail.ToInt();
                    //UPT.TypeLetter = Obj.nID;
                    UPT.nTypeMailID = Obj.sTypeMail.ToInt();
                    //UPT.TypeOfLetter = Obj.nID;
                    UPT.Datetime = Obj.sDate;
                    UPT.dDate = Obj.sDate.ToDateFromString();
                    UPT.Period = Obj.sTime;
                    UPT.Product = null;
                    UPT.rload = null;
                    UPT.sUpdate = null;
                    UPT.dUpdateDate = DateTime.Now;
                    UPT.IsDelete = false;
                    DB.SaveChanges();
                }
                else
                {
                    Normal CRT = new Normal();
                    CRT.Sender = Obj.sSender;
                    //CRT.DivName = Obj.nID;
                    CRT.Ref = Obj.sRef;
                    CRT.Res = Obj.sReceiver;
                    CRT.Num = Obj.nQuantity;
                    CRT.EndT = Obj.nPostal + "";
                    CRT.Pay = Obj.nAmount;
                    CRT.RC = Obj.sRCNumber;
                    CRT.UserID = null;
                    //CRT.Lot = Obj.;
                    CRT.nTypeInOutID = Obj.sGroupMail.ToInt();
                    //CRT.TypeLetter = Obj.nID;
                    CRT.nTypeMailID = Obj.sTypeMail.ToInt();
                    //CRT.TypeOfLetter = Obj.nID;
                    CRT.Datetime = Obj.sDate;
                    CRT.dDate = Obj.sDate.ToDateFromString();
                    CRT.Period = Obj.sTime;
                    CRT.Product = null;
                    CRT.rload = null;
                    CRT.sCreate = null;
                    CRT.dCreateDate = DateTime.Now;
                    CRT.sUpdate = null;
                    CRT.dUpdateDate = DateTime.Now;
                    CRT.IsDelete = false;
                    DB.Normals.Add(CRT);
                    DB.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }

            return KeyDailyTaskForm(Obj);
        }
    }
}
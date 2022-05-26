using Demo1.Models;
using Demo1.Models.PSOrderContext;
using Extensions.Common.STExtension;
using Extensions.Common.STResultAPI;
using Microsoft.AspNetCore.Mvc;
using static PMSS.Extensions.Common.AllClass;

namespace Demo1.Controllers
{
    public class ManageMailTypeController : Controller
    {
        private readonly PSOrderContext DB;
        public ManageMailTypeController(PSOrderContext db)
        {
            DB = db;
        }

        public IActionResult ManageMailTypeForm(int? pageNumber)
        {
            ManageMailTypeListClass Model = new ManageMailTypeListClass();
            List<ManageMailTypeClass> lstData = new List<ManageMailTypeClass>();
            List<cDropDown> lstTypeMail = new List<cDropDown>();
            List<cDropDown> lstGroupMail = new List<cDropDown>();
            lstData = GetData();
            lstTypeMail = GetDropDownTypeMail();
            lstGroupMail = GetDropDownGroupMail();
            Model.lstData = lstData;
            Model.lstTypeMail = lstTypeMail;
            Model.lstGroupMail = lstGroupMail;
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

        public List<ManageMailTypeClass> GetData()
        {
            List<ManageMailTypeClass> lstData = new List<ManageMailTypeClass>();
            var lstTypeMail = DB.Type_Mails.Where(w => !w.IsDelete.Value).OrderByDescending(o => o.dUpdateDate).ToList();
            if (lstTypeMail.Count > 0)
            {
                int i = 1;
                foreach (var Item in lstTypeMail)
                {
                    string sType_Mail = "";
                    var lstType_Mail = DB.MT_TypeMails.FirstOrDefault(f => f.nID == Item.Type_Mail1.ToInt());
                    if (lstType_Mail != null)
                    {
                        sType_Mail = lstType_Mail.sTypeMailName + "";
                    }

                    string sType_InOut = "";
                    var lstType_InOut = DB.MT_TypeInOuts.FirstOrDefault(f => f.nID == Item.nTypeInOutID.ToInt());
                    if (lstType_InOut != null)
                    {
                        sType_InOut = lstType_InOut.sTypeInOutName + "";
                    }

                    lstData.Add(new ManageMailTypeClass
                    {
                        nNo = i++,
                        ID = Item.ID,
                        Type_Name = Item.Type_Name.TrimEnd(),
                        Type_Pay = Item.Type_Pay,
                        nTypeMailID = Item.Type_Mail1.ToInt(),
                        Type_Mail = sType_Mail,
                        nTypeInOutID = Item.nTypeInOutID,
                        Type_InOut = sType_InOut
                    });
                }
            }

            return lstData;
        }

        public List<cDropDown> GetDropDownTypeMail()
        {
            List<cDropDown> lstData = new List<cDropDown>();
            lstData = DB.MT_TypeMails.Where(w => w.cActive == "Y" && !w.IsDelete).Select(s =>
                    new cDropDown
                    {
                        value = s.nID + "",
                        label = s.sTypeMailName + ""
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ManageMailTypeForm(ManageMailTypeListClass Obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var UPT = DB.Type_Mails.FirstOrDefault(f => f.ID == Obj.ID);
                    if (UPT != null)
                    {
                        if (Obj.IsEdit)
                        {
                            UPT.Type_Name = Obj.Type_Name.TrimEnd();
                            UPT.Type_Pay = Obj.Type_Pay;
                            UPT.Type_Mail1 = Obj.Type_Mail;
                            UPT.Type_InOut = Obj.Type_InOut;
                            UPT.sUpdate = null;
                            UPT.dUpdateDate = DateTime.Now;
                            UPT.IsDelete = false;
                            DB.SaveChanges();

                            TempData["Success"] = "Action Completed', 'Data is already saved.";
                            Redirect("ManageMailTypeForm");
                        }
                        else
                        {
                            var lstDup = DB.Type_Mails.Where(w => w.Type_Name.TrimEnd() == Obj.Type_Name.TrimEnd() && !w.IsDelete.Value).ToList();
                            if (lstDup.Count > 0)
                            {
                                TempData["Error"] = "Mail is Duplicate.";
                                Redirect("ManageMailTypeForm");
                            }
                            else
                            {
                                UPT.Type_Name = Obj.Type_Name.TrimEnd();
                                UPT.Type_Pay = Obj.Type_Pay;
                                UPT.Type_Mail1 = Obj.Type_Mail;
                                UPT.Type_InOut = Obj.Type_InOut;
                                UPT.sUpdate = null;
                                UPT.dUpdateDate = DateTime.Now;
                                UPT.IsDelete = false;
                                DB.SaveChanges();

                                TempData["Success"] = "Action Completed', 'Data is already saved.";
                                Redirect("ManageMailTypeForm");
                            }
                        }
                    }
                    else
                    {
                        Type_Mail CRT = new Type_Mail();
                        CRT.Type_Name = Obj.Type_Name.TrimEnd();
                        CRT.Type_Pay = Obj.Type_Pay;
                        CRT.Type_Mail1 = Obj.Type_Mail;
                        CRT.Type_InOut = Obj.Type_InOut;
                        CRT.sCreate = null;
                        CRT.dCreateDate = DateTime.Now;
                        CRT.sUpdate = null;
                        CRT.dUpdateDate = DateTime.Now;
                        CRT.IsDelete = false;
                        DB.Type_Mails.Add(CRT);
                        DB.SaveChanges();

                        TempData["Success"] = "Action Completed', 'Data is already saved.";
                        Redirect("ManageMailTypeForm");
                    }
                }
                catch (Exception ex)
                {
                    TempData["Error"] = ex.Message;
                }
            }
            else
            {
                TempData["Error"] = "Please require field data.";
            }

            return ManageMailTypeForm(0);
        }

        [HttpPost]
        public ResultAPI Delete(string sID)
        {
            ResultAPI Result = new ResultAPI();

            try
            {
                var DEL = DB.Type_Mails.FirstOrDefault(f => f.ID == sID.ToDecimal());
                if (DEL != null)
                {
                    DEL.dUpdateDate = DateTime.Now;
                    DEL.sUpdate = null;
                    DEL.IsDelete = true;
                    DB.SaveChanges();
                }
                Result.Status = ResultStatus.Success;
                Redirect("ManageMailTypeForm");
            }
            catch (Exception ex)
            {
                Result.Message = ex.Message;
                Result.Status = ResultStatus.Failed;
            }

            return Result;
        }
    }
}

using Demo1.Models;
using Demo1.Models.PSOrderContext;
using Extensions.Common.STResultAPI;
using Microsoft.AspNetCore.Mvc;
using Extensions.Common.STExtension;
using static PMSS.Extensions.Common.AllClass;

namespace Demo1.Controllers
{
    public class ManageUnitDepartmentController : Controller
    {
        private readonly PSOrderContext DB;
        public ManageUnitDepartmentController(PSOrderContext db)
        {
            DB = db;
        }

        public IActionResult ManageUnitDepartmentForm(int? pageNumber)
        {
            ManageUnitDepartmentListClass Model = new ManageUnitDepartmentListClass();
            List<ManageUnitDepartmentClass> lstData = new List<ManageUnitDepartmentClass>();
            List<cDropDown> lstGroupSection = new List<cDropDown>();
            lstData = GetData();
            lstGroupSection = GetDropDownGroupSection();
            Model.SectionCode = null;
            Model.SectionName = null;
            Model.SectionGroup = null;
            Model.lstData = lstData;
            Model.lstGroupSection = lstGroupSection;
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

        public List<ManageUnitDepartmentClass> GetData()
        {
            List<ManageUnitDepartmentClass> lstData = new List<ManageUnitDepartmentClass>();
            var lstUser = DB.Sections.Where(w => !w.IsDelete.Value).OrderByDescending(o => o.dUpdateDate).ToList();
            if (lstUser.Count > 0)
            {
                int i = 1;
                foreach (var Item in lstUser)
                {
                    string sSectionGroupName = "";
                    var lstGroup = DB.MT_GroupSections.FirstOrDefault(f => f.nGroupSectionID == Item.SectionGroup.ToInt());
                    if (lstGroup != null)
                    {
                        sSectionGroupName = lstGroup.sGroupSectionName + "";
                    }
                    lstData.Add(new ManageUnitDepartmentClass
                    {
                        nNo = i++,
                        SectionCode = Item.SectionCode,
                        SectionName = Item.SectionName,
                        SectionGroup = Item.SectionGroup,
                        SectionGroupName = Item.SectionGroup + " : " + sSectionGroupName
                    });
                }
            }

            return lstData;
        }

        public List<cDropDown> GetDropDownGroupSection()
        {
            List<cDropDown> lstData = new List<cDropDown>();
            lstData = DB.MT_GroupSections.Where(w => w.cActive == "Y").Select(s =>
                  new cDropDown
                  {
                      value = s.nGroupSectionID + "",
                      label = s.nGroupSectionID + " : " + s.sGroupSectionName
                  }).ToList();
            return lstData;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ManageUnitDepartmentForm(ManageUnitDepartmentListClass Obj)
        {
            //if (Obj == null)
            //{
            //    ModelState.AddModelError("SectionCode", "SectionCode");
            //}
            string sSectionCode = "";
            if (ModelState.IsValid)
            {
                try
                {
                    if (Obj.SectionCode.Length < 5)
                    {
                        string Str = "0";
                        for (int i = Obj.SectionCode.Length; i < 5; i++)
                        {
                            sSectionCode += Str;
                        }
                        sSectionCode += Obj.SectionCode;
                    }
                    else if (Obj.SectionCode.Length == 5)
                    {
                        sSectionCode = Obj.SectionCode;
                    }

                    var UPT = DB.Sections.FirstOrDefault(f => f.SectionCode == sSectionCode);
                    if (UPT != null)
                    {
                        if (Obj.IsEdit)
                        {
                            UPT.SectionName = Obj.SectionName;
                            UPT.SectionGroup = Obj.SectionGroup;
                            UPT.dUpdateDate = DateTime.Now;
                            UPT.sUpdate = null;
                            UPT.IsDelete = false;
                            DB.SaveChanges();

                            TempData["Success"] = "Action Completed', 'Data is already saved.";
                            Redirect("ManageUnitDepartmentForm");
                        }
                        else
                        {
                            var lstDup = DB.Sections.Where(w => w.SectionCode == sSectionCode && !w.IsDelete.Value).ToList();
                            if (lstDup.Count > 0)
                            {
                                TempData["Error"] = "Division Code is Duplicate.";
                                Redirect("ManageUnitDepartmentForm");
                            }
                            else
                            {
                                UPT.SectionName = Obj.SectionName;
                                UPT.SectionGroup = Obj.SectionGroup;
                                UPT.dUpdateDate = DateTime.Now;
                                UPT.sUpdate = null;
                                UPT.IsDelete = false;
                                DB.SaveChanges();

                                TempData["Success"] = "Action Completed', 'Data is already saved.";
                                Redirect("ManageUnitDepartmentForm");
                            }
                        }
                    }
                    else
                    {
                        Section CRT = new Section();
                        CRT.SectionCode = sSectionCode;
                        CRT.SectionName = Obj.SectionName;
                        CRT.SectionGroup = Obj.SectionGroup;
                        CRT.dCreateDate = DateTime.Now;
                        CRT.sCreate = null;
                        CRT.dUpdateDate = DateTime.Now;
                        CRT.sUpdate = null;
                        CRT.IsDelete = false;
                        DB.Sections.Add(CRT);
                        DB.SaveChanges();

                        TempData["Success"] = "Action Completed', 'Data is already saved.";
                        Redirect("ManageUnitDepartmentForm");
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
            //Obj.SectionCode = "";
            //Obj.SectionName = "";
            //Obj.SectionGroup = "";
            //Obj.lstData = GetData();
            //Obj.lstGroupSection = GetDropDownGroupSection();

            return ManageUnitDepartmentForm(0);
            //return View(Obj);
        }

        [HttpPost]
        public IActionResult Delete(string sID)
        {
            try
            {
                var DEL = DB.Sections.FirstOrDefault(f => f.SectionCode == sID);
                if (DEL != null)
                {
                    DEL.dUpdateDate = DateTime.Now;
                    DEL.sUpdate = null;
                    DEL.IsDelete = true;
                    DB.SaveChanges();
                }
                TempData["Success"] = "Action Completed', 'Data is already saved.";
                Redirect("ManageUnitDepartmentForm");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return ManageUnitDepartmentForm(0);
        }

        //[HttpPost]
        //public ResultAPI SaveToDB(ManageUnitDepartmentClass Obj)
        //{
        //    ResultAPI Result = new ResultAPI();

        //    try
        //    {
        //        var UPT = DB.Sections.FirstOrDefault(f => f.SectionCode == Obj.SectionCode);
        //        if (UPT != null)
        //        {
        //            UPT.SectionName = Obj.SectionName;
        //            UPT.SectionGroup = Obj.SectionGroup;
        //            UPT.dUpdateDate = DateTime.Now;
        //            UPT.sUpdate = null;
        //            UPT.IsDelete = false;
        //            DB.SaveChanges();
        //        }
        //        else
        //        {
        //            var lstDup = DB.Sections.Where(w => w.SectionCode == Obj.SectionCode && !w.IsDelete).ToList();
        //            if (lstDup.Count > 0)
        //            {
        //                Result.Message = "Division Code is Duplicate.";
        //                Result.Status = ResultStatus.Failed;
        //            }
        //            else
        //            {
        //                Section CRT = new Section();
        //                CRT.SectionCode = Obj.SectionCode;
        //                CRT.SectionName = Obj.SectionName;
        //                CRT.SectionGroup = Obj.SectionGroup;
        //                CRT.dCreateDate = DateTime.Now;
        //                CRT.sCreate = null;
        //                CRT.dUpdateDate = DateTime.Now;
        //                CRT.sUpdate = null;
        //                CRT.IsDelete = false;
        //                DB.Sections.Add(CRT);
        //                DB.SaveChanges();
        //            }
        //        }
        //        Result.Status = ResultStatus.Success;
        //    }
        //    catch (Exception ex)
        //    {
        //        Result.Message = ex.Message;
        //        Result.Status = ResultStatus.Failed;
        //    }

        //    return Result;
        //}

        //public IActionResult ManageUnitDepartmentForm2(int? pageNumber)
        //{
        //    ManageUnitDepartmentListClass2 Model = new ManageUnitDepartmentListClass2();
        //    List<ManageUnitDepartmentClass> lstData = new List<ManageUnitDepartmentClass>();
        //    List<cDropDown> lstGroupSection = new List<cDropDown>();
        //    lstData = GetData();
        //    lstGroupSection = GetDropDownGroupSection();
        //    Model.lstData = lstData;
        //    Model.lstGroupSection = lstGroupSection;
        //    Model.PageNumber = (pageNumber == null ? 1 : Convert.ToInt32(pageNumber));
        //    Model.PageSize = 10;

        //    Model.PagePrevious = Model.PageNumber - 1;
        //    Model.PageNext = Model.PageNumber + 1;

        //    if (lstData != null)
        //    {
        //        Model.lstData = lstData.OrderBy(x => x.nNo)
        //                            .Skip(Model.PageSize * (Model.PageNumber - 1))
        //                            .Take(Model.PageSize).ToList();
        //        Model.TotalCount = lstData.Count;
        //        Model.PagerCount = ((Model.TotalCount / Model.PageSize) -
        //                            (Model.TotalCount % Model.PageSize == 0 ? 1 : 0)) + 1;
        //    }

        //    return View(Model);
        //}

        //[HttpPost]
        //public IActionResult ManageUnitDepartmentForm2(ManageUnitDepartmentListClass2 Obj)
        //{
        //    if (Obj == null)
        //    {
        //        ModelState.AddModelError("SectionCode", "SectionCode");
        //    }
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            var UPT = DB.Sections.FirstOrDefault(f => f.SectionCode == Obj.SectionCode);
        //            if (UPT != null)
        //            {
        //                UPT.SectionName = Obj.SectionName;
        //                UPT.SectionGroup = Obj.SectionGroup;
        //                UPT.dUpdateDate = DateTime.Now;
        //                UPT.sUpdate = null;
        //                UPT.IsDelete = false;
        //                DB.SaveChanges();

        //                TempData["Success"] = "Action Completed', 'Data is already saved.";
        //                return Redirect("ManageUnitDepartmentForm2");
        //            }
        //            else
        //            {
        //                var lstDup = DB.Sections.Where(w => w.SectionCode == Obj.SectionCode && !w.IsDelete).ToList();
        //                if (lstDup.Count > 0)
        //                {
        //                    //Result.Message = "Division Code is Duplicate.";
        //                    //Result.Status = ResultStatus.Failed;
        //                }
        //                else
        //                {
        //                    Section CRT = new Section();
        //                    CRT.SectionCode = Obj.SectionCode;
        //                    CRT.SectionName = Obj.SectionName;
        //                    CRT.SectionGroup = Obj.SectionGroup;
        //                    CRT.dCreateDate = DateTime.Now;
        //                    CRT.sCreate = null;
        //                    CRT.dUpdateDate = DateTime.Now;
        //                    CRT.sUpdate = null;
        //                    CRT.IsDelete = false;
        //                    DB.Sections.Add(CRT);
        //                    DB.SaveChanges();

        //                    return Redirect("ManageUnitDepartmentForm2");
        //                }

        //            }
        //            //Result.Status = ResultStatus.Success;
        //        }
        //        catch (Exception ex)
        //        {
        //            //Result.Message = ex.Message;
        //            //Result.Status = ResultStatus.Failed;

        //            TempData["Error"] = "Error, Checking Alert " + ex.Message;
        //        }
        //    }
        //    Obj.lstGroupSection = GetDropDownGroupSection();

        //    return View(Obj);
        //}
    }
}

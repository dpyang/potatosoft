using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Xml;
using TglFirst.Core;
using LookFund.Dao.FundInfo;
using LookFund.Models.ViewModel;
using LookFund.Models.Membership;
using LookFund.Dao.Membership;
using LookFund.Controllers.Filter;
using LookFund.Models;

namespace LookFund.Controllers
{
    public class AccountController : AbstractPageController
    {
        [Authorization]
        public ActionResult AdminMain()
        {
            if (string.Equals("22", CurrentUser.EmpKind))
            {
                var dao = SpringContext.Context.GetObject("dao.BasicInfo") as IBasicInfo;
                var str = dao.GetGanlibu(CurrentUser.Uid.ToString());
                if (!string.IsNullOrEmpty(str))
                {
                    var empdao = SpringContext.Context.GetObject("dao.Employe") as IEmploye;
                    ViewData["manageinfo"] = empdao.Query("EmpKind", str.Substring(0, 4));
                }
            }
            return View(CurrentUser);
        }


        [Authorization]
        public ActionResult LeftMemu()
        {
            return PartialView("LeftMenu", CurrentUser);
        }

        #region 数据导入
        [Authorization]
        public ActionResult BaseImport()
        {
            return PartialView("BaseImport");
        }

        [Authorization]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult BaseImport(HttpPostedFileBase upfile, string ddlAddress)
        {
            if (upfile != null)
            {
                try
                {
                    string FileType = upfile.FileName.Substring(upfile.FileName.LastIndexOf(".") + 1);
                     string _tempFileName= upfile.FileName.Replace("/","\\");
                     string[] _fs = _tempFileName.Split(new string []{"\\"},StringSplitOptions.RemoveEmptyEntries );
                     string[] fs = _fs[_fs.Length-1].Split('.')[0].Split('_');
                     if (fs.Length != 2)
                     {
                         return Content("文件上传格式不正确");
                     }

                    string fileName = AppDomain.CurrentDomain.BaseDirectory + "Uploads\\" + Guid.NewGuid().ToString() + "." + FileType;
                    upfile.SaveAs(fileName);
                    if (System.IO.File.Exists(fileName))
                    {
                        var EmpDao = SpringContext.Context.GetObject("dao.BasicInfo") as IBasicInfo;
                        var msg = EmpDao.uploadTemp(fileName, ddlAddress, CurrentUser.LoginName, _fs[_fs.Length - 1]);
                        return Content(msg);
                    }
                    return Content("文件上传失败");
                }
                catch (Exception ex)
                {
                    return Content(ex.Message);
                }
            }
            return Content("请选择上传文件！");
        }

        [Authorization]
        public ActionResult BaseImportSave(string ddlAddress)
        {
            try
            {
                var EmpDao = SpringContext.Context.GetObject("dao.BasicInfo") as IBasicInfo;
                var msg = EmpDao.ImportBaseSave(ddlAddress, CurrentUser.LoginName);
                return Content(msg);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        [Authorization]
        public ActionResult HistoryDateImport()
        {
            return PartialView("HistoryDateImport");
        }

        [Authorization]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult HistoryDateImport(HttpPostedFileBase upfile, string ddlAddress)
        {
            if (upfile != null)
            {
                try
                {

                    string _tempFileName = upfile.FileName.Replace("/", "\\");
                    string[] _fs = _tempFileName.Split(new string[] {"\\"}, StringSplitOptions.RemoveEmptyEntries);
                    string[] fs = _fs[_fs.Length - 1].Split('.')[0].Split('_');
                    if (fs.Length != 2)
                    {
                        return Content("文件上传格式不正确");
                    }
                    string FileType = upfile.FileName.Substring(upfile.FileName.LastIndexOf(".") + 1);
                    string fileName = AppDomain.CurrentDomain.BaseDirectory + "Uploads\\" + Guid.NewGuid().ToString() +
                                      "." + FileType;
                    upfile.SaveAs(fileName);
                    if (System.IO.File.Exists(fileName))
                    {
                        var EmpDao = SpringContext.Context.GetObject("dao.BasicInfo") as IBasicInfo;
                        var msg = EmpDao.uploadTemp(fileName, ddlAddress, CurrentUser.LoginName, _fs[_fs.Length - 1]);
                        return Content(msg);
                    }
                    return Content("文件上传失败");
                }
                catch (Exception ex)
                {
                    return Content(ex.Message);
                }
            }
            return Content("请选择上传文件！");
        }

        [Authorization]
        public ActionResult HistoryImportSave(string ddlAddress)
        {
            try
            {
                var EmpDao = SpringContext.Context.GetObject("dao.BasicInfo") as IBasicInfo;
                var msg = EmpDao.ImprotHistorySave(ddlAddress, CurrentUser.LoginName);
                return Content(msg);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        [Authorization]
        public ActionResult CenterImport()
        {
            return PartialView("CenterImport");
        }

        [Authorization]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CenterImport(HttpPostedFileBase upfile, string ddlAddress)
        {
            if (upfile != null)
            {
                try
                {
                    string FileType = upfile.FileName.Substring(upfile.FileName.LastIndexOf(".") + 1);
                    string fileName = AppDomain.CurrentDomain.BaseDirectory + "Uploads\\" + DateTime.Now.ToString("yyyyMMddHHmmss") + "." + FileType;
                    upfile.SaveAs(fileName);
                    if (System.IO.File.Exists(fileName))
                    {
                        var EmpDao = SpringContext.Context.GetObject("dao.BasicInfo") as IBasicInfo;
                        EmpDao.ImportCenter(fileName, ddlAddress);
                        return Content("导入成功");
                    }
                    return Content("文件上传失败");
                }
                catch (Exception ex)
                {
                    return Content(ex.Message);
                }
            }
            return Content("请选择上传文件！");
        }

        #endregion

        #region 密码管理
        [Authorization]
        public ActionResult PwdEdit()
        {
            return PartialView("PwdEdit");
        }
        [Authorization]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult PwdEdit(PwdEditViewModel model)
        {
            if (ModelState.IsValid && model.UpdatePwd(ModelState, CurrentUser.Uid))
            {
                return Content("Saved");
            }
            else
            {
                return PartialView(model);
            }
        }
        [Authorization]
        public ActionResult PwdInit()
        {
            return PartialView("PwdInit");
        }
        [Authorization]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult PwdInit(string LoginName)
        {
            var dao = SpringContext.Context.GetObject("dao.Employe") as IEmploye;
            if (dao.PwdInit(LoginName,CurrentUser.EmpKind))
            {
                return Content("Saved");
            }
            return Content("False");
        }

        #endregion

        #region 意见中心
        [Authorization]
        public ActionResult EmpComments()
        {
            var dao = SpringContext.Context.GetObject("dao.Comments") as IComments;
            ViewData["Comments"] = dao.Query("ComPerson", CurrentUser);
            return PartialView("EmpComments");
        }

        [Authorization]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EmpComments(CommentsViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.ComPerson = CurrentUser;
                model.Save();
            }
            var dao = SpringContext.Context.GetObject("dao.Comments") as IComments;
            ViewData["Comments"] = dao.Query("ComPerson", CurrentUser);
            return PartialView("EmpComments");
        }

        [Authorization]
        public ActionResult CommentsManage()
        {
            return PartialView("CommentsManage");
        }

        [Authorization]
        public ActionResult CommentsResult()
        {
            var dao = SpringContext.Context.GetObject("dao.Comments") as IComments;
            string PageHtml = string.Empty;
            ViewData["InfoList"] = dao.GetList(CurrentUser.EmpKind, 0, 10, out PageHtml);
            ViewData["pageHtml"] = PageHtml;
            return PartialView("CommentsResult");
        }

        [Authorization]
        public ActionResult CommentsManagePage(string page,string strSearch)
        {
            var dao = SpringContext.Context.GetObject("dao.Comments") as IComments;
            string PageHtml = string.Empty;
            ViewData["InfoList"] = dao.GetList(CurrentUser.EmpKind, Int32.Parse(page), 10, out PageHtml, strSearch);
            ViewData["pageHtml"] = PageHtml;
            return PartialView("CommentsResult");
        }

        [Authorization]
        public ActionResult ReplyEdit()
        {
            string uid = Request.QueryString["id"];
            var dao = SpringContext.Context.GetObject("dao.Comments") as IComments;
            return PartialView("ReplyEdit", dao.Get(new Guid(uid)));
        }

        [Authorization]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CommentsManage(MComments model,string page)
        {
            var dao = SpringContext.Context.GetObject("dao.Comments") as IComments;
            var getModel = dao.Get(model.Uid);
            getModel.Reply = model.Reply;
            dao.Update(getModel);
            string PageHtml = string.Empty;
            ViewData["InfoList"] = dao.GetList(CurrentUser.EmpKind, Int32.Parse(page) - 1, 10, out PageHtml);
            ViewData["pageHtml"] = PageHtml;
            return PartialView("CommentsResult");
        }

        [Authorization]
        public ActionResult CommentsSearch(string strSearch)
        {
            var dao = SpringContext.Context.GetObject("dao.Comments") as IComments;
            string PageHtml = string.Empty;
            ViewData["InfoList"] = dao.GetList(CurrentUser.EmpKind, 0, 10, out PageHtml, strSearch);
            ViewData["pageHtml"] = PageHtml;
            return PartialView("CommentsResult");
        }

        [Authorization]
        public ActionResult CommentsStat()
        {
            var dao = SpringContext.Context.GetObject("dao.Comments") as IComments;
            return PartialView("CommentsStat", dao.CommentsStat(CurrentUser.EmpKind));
        }


        #endregion

        #region 数据验证页
        [Authorization]
        public ActionResult VerifyFund()
        {
            var dao = SpringContext.Context.GetObject("dao.CurrentMonth") as ICurrentMonth;
            string htmlPage = string.Empty;
            var list = dao.DateMonth(0, 20, out htmlPage, CurrentUser.EmpKind);
            ViewData["htmlPage"] = htmlPage;
            return PartialView("VerifyFund", list);
        }
        [Authorization]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult VerifyFund(string page)
        {
            var dao = SpringContext.Context.GetObject("dao.CurrentMonth") as ICurrentMonth;
            string htmlPage = string.Empty;
            var list = dao.DateMonth(Int32.Parse(page), 20, out htmlPage, CurrentUser.EmpKind);
            ViewData["htmlPage"] = htmlPage;
            return PartialView("VerifyFund", list);
        }
        [Authorization]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult VerifySucced(string strGuid, string page)
        {
            var dao = SpringContext.Context.GetObject("dao.CurrentMonth") as ICurrentMonth;
            dao.VerifySucced(strGuid);
            string htmlPage = string.Empty;
            var list = dao.DateMonth(Int32.Parse(page) - 1, 20, out htmlPage, CurrentUser.EmpKind);
            ViewData["htmlPage"] = htmlPage;
            return PartialView("VerifyFund", list);
        }
        [Authorization]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult PageSucced(string arrGuid, string page)
        {
            var dao = SpringContext.Context.GetObject("dao.CurrentMonth") as ICurrentMonth;
            dao.PageSucced(arrGuid.Split(','));
            string htmlPage = string.Empty;
            var list = dao.DateMonth(Int32.Parse(page) - 1, 20, out htmlPage, CurrentUser.EmpKind);
            ViewData["htmlPage"] = htmlPage;
            return PartialView("VerifyFund", list);
        }
        [Authorization]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SuccedAll()
        {
            var dao = SpringContext.Context.GetObject("dao.CurrentMonth") as ICurrentMonth;
            dao.SuccedAll(CurrentUser.EmpKind);
            string htmlPage = string.Empty;
            var list = dao.DateMonth(0, 20, out htmlPage, CurrentUser.EmpKind);
            ViewData["htmlPage"] = htmlPage;
            return PartialView("VerifyFund", list);
        }

        #endregion

        #region 数据查询
        [Authorization]
        public ActionResult FundInfoData()
        {
            var dao = SpringContext.Context.GetObject("dao.BasicInfo") as IBasicInfo;
            var curr = SpringContext.Context.GetObject("dao.CurrentMonth") as ICurrentMonth;
            string strGuid = dao.BaseGuid(CurrentUser.LoginName);
            if (!string.IsNullOrEmpty(strGuid))
            {
                ViewData["FundDate"] = curr.DateMonth(strGuid);
            }
            return PartialView("FundInfoData", dao.BaseInfo(CurrentUser.LoginName));
        }
        [Authorization]
        public ActionResult FundLookTime()
        {
            FundLookTimeViewModel model = new FundLookTimeViewModel();
            return PartialView("FundLookTime");
        }
        [Authorization]
        public ActionResult HistoryFundLook(FundLookTimeViewModel model)
        {
            var dao = SpringContext.Context.GetObject("dao.BasicInfo") as IBasicInfo;
            var hist = SpringContext.Context.GetObject("dao.HistoryMonth") as IHistoryMonth;
            string strGuid = dao.BaseGuid(CurrentUser.LoginName);
            if (!string.IsNullOrEmpty(strGuid))
            {
                ViewData["FundDate"] = hist.FundHistoryDate(model, strGuid);
            }
            return PartialView("HistoryDataEmp", dao.BaseInfo(CurrentUser.LoginName));
        }
        [Authorization]
        public ActionResult CenterLook()
        {
            return PartialView("CenterLook");
        }
        [Authorization]
        public ActionResult EmpCenterLook(EmpLookViewModel model)
        { 
            var dao = SpringContext.Context.GetObject("dao.BasicInfo") as IBasicInfo;
            string htmlPage = string.Empty;
            var dt = dao.CenterLookInfo(model, CurrentUser.EmpKind, 0, 20, out htmlPage);
            ViewData["htmlPage"] = htmlPage;           
            return PartialView("CenterLookShow",dt );
        }
        [Authorization]
        public ActionResult EmpCenterLookPage(EmpLookViewModel model,string page)
        {
            var dao = SpringContext.Context.GetObject("dao.BasicInfo") as IBasicInfo;
            string htmlPage = string.Empty;
            var dt = dao.CenterLookInfo(model, CurrentUser.EmpKind, Int32.Parse(page), 20, out htmlPage);
            ViewData["htmlPage"] = htmlPage;
            return PartialView("CenterLookShow", dt);
        }
        [Authorization]
        public ActionResult EmpCenterLookShow()
        {
            var dao = SpringContext.Context.GetObject("dao.BasicInfo") as IBasicInfo;
            return PartialView("EmpCenterLookShow", dao.CenterLookInfo(CurrentUser.LoginName));
        }
        #endregion

        #region 操作员管理
        [Authorization]
        public ActionResult OperatorManage()
        {
            var empDao = SpringContext.Context.GetObject("dao.Employe") as IEmploye;
            string PageHtml = string.Empty;
            var list = empDao.GetList(0, 20, out PageHtml, CurrentUser.EmpKind);
            ViewData["pageHtml"] = PageHtml;
            ViewData["currentUser"] = CurrentUser.LoginName;
            return PartialView("OperatorManage", list);
        }
        [Authorization]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult OperatorPage(string page)
        {
            var empDao = SpringContext.Context.GetObject("dao.Employe") as IEmploye;
            string PageHtml = string.Empty;
            var list = empDao.GetList(Int32.Parse(page), 20, out PageHtml, CurrentUser.EmpKind);
            ViewData["pageHtml"] = PageHtml;
            ViewData["currentUser"] = CurrentUser.LoginName;
            return PartialView("OperatorManage", list);
        }
        [Authorization]
        public ActionResult OperatorAdd()
        {
            return PartialView("OperatorAdd");
        }
        [Authorization]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult OperatorAdd(OperatorEditViewModel model)
        {
            if (ModelState.IsValid && model.AddSave(ModelState))
            {
                return Content("Saved");
            }
            return PartialView(model);
        }
        [Authorization]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult OperatorUpdateInit(string uid)
        {
            var dao = SpringContext.Context.GetObject("dao.Employe") as IEmploye;
            MEmploye model = dao.Get(new Guid(uid));
            OperatorEditViewModel viewmodel = new OperatorEditViewModel()
            {
                Uid = model.Uid.ToString(),
                LoginName = model.LoginName,
                ShowName = model.ShowName,
                LoginPwk = model.LoginPwk,
                ConfirmPwd = model.LoginPwk,
                EmpKind = model.EmpKind,
                EmpPhone=model.EmpPhone
            };
            return PartialView("OperatorUpdate", viewmodel);
        }
        [Authorization]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult OperatorUpdate(OperatorEditViewModel model)
        {
            if (ModelState.IsValid && model.UpdateSave(ModelState))
            {
                return Content("Saved");
            }
            return PartialView(model);
        }
        [Authorization]
        public ActionResult OperatorDel(string uid, string page)
        {
            var dao = SpringContext.Context.GetObject("dao.Employe") as IEmploye;
            dao.OperatorDel(uid);
            string PageHtml = string.Empty;
            var list = dao.GetList(Int32.Parse(page), 20, out PageHtml, CurrentUser.EmpKind);
            ViewData["pageHtml"] = PageHtml;
            ViewData["currentUser"] = CurrentUser.LoginName;
            return PartialView("OperatorManage", list);
        }
        [Authorization]
        public ActionResult OperatorFun(string uid)
        {
            var dao = SpringContext.Context.GetObject("dao.Employe") as IEmploye;
            var model = dao.Get(new Guid(uid));
            var menu = SpringContext.Context.GetObject("dao.FunctionMenu") as IFunctionMenu;
            ViewData["FunTree"] = menu.TableTree(model);
            return PartialView("OperatorFun");
        }
        [Authorization]
        public ActionResult OperatorFunSave(string arrCode, string empUId)
        {
            var dao = SpringContext.Context.GetObject("dao.Employe") as IEmploye;
            dao.OperatorFunSave(arrCode, empUId);
            string PageHtml = string.Empty;
            var list = dao.GetList(0, 20, out PageHtml, CurrentUser.EmpKind);
            ViewData["pageHtml"] = PageHtml;
            ViewData["currentUser"] = CurrentUser.LoginName;
            return PartialView("OperatorManage", list);
        }
        #endregion

        #region 查看员工公积金
        [Authorization]
        public ActionResult EmpFundLook()
        {
            return PartialView("EmpFundLook");
        }

        [Authorization]
        public ActionResult FirstSelectListUnit()
        {
             var dao = SpringContext.Context.GetObject("dao.BasicInfo") as IBasicInfo;
             ViewData["selectList"] = dao.SelectListUnit(CurrentUser.EmpKind);
            return PartialView("SelectListUnit");
        }

        [Authorization]
        public ActionResult SelectListUnit(string company)
        {
            if (company.Equals("0"))
                company = CurrentUser.EmpKind;
            var dao = SpringContext.Context.GetObject("dao.BasicInfo") as IBasicInfo;
            ViewData["selectList"] = dao.SelectListUnit(company);
            return PartialView("SelectListUnit");
        }

        [Authorization]
        public ActionResult SelectEorrListUnit()
        {
            if (!"22".Equals(CurrentUser.EmpKind))
            {
                string company = CurrentUser.EmpKind + "01";
                var dao = SpringContext.Context.GetObject("dao.BasicInfo") as IBasicInfo;
                ViewData["selectList"] = dao.SelectEorrListUnit(company);
            }
            return PartialView("SelectListUnit");
        }
        [Authorization]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EmpFundLook(EmpLookViewModel model)
        {
            var dao = SpringContext.Context.GetObject("dao.BasicInfo") as IBasicInfo;
            string htmlPage = string.Empty;
            var dt = dao.BaseInfo(model,0, 20, out htmlPage);
            ViewData["htmlPage"] = htmlPage;
            return PartialView("LookResultShow", dt);
        }

        [Authorization]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EmpFundLookPage(EmpLookViewModel model,string page)
        {
            var dao = SpringContext.Context.GetObject("dao.BasicInfo") as IBasicInfo;
            string htmlPage = string.Empty;
            var dt = dao.BaseInfo(model, Int32.Parse(page), 20, out htmlPage);
            ViewData["htmlPage"] = htmlPage;
            return PartialView("LookResultShow", dt);
        }

        [Authorization]
        public ActionResult EmpFundLookInfo(string strGuid, string strCompany)
        {
            var dao = SpringContext.Context.GetObject("dao.BasicInfo") as IBasicInfo;
            var hist = SpringContext.Context.GetObject("dao.HistoryMonth") as IHistoryMonth;
            ViewData["FundDate"] = hist.FundDate(strCompany, strGuid);
            return PartialView("HistoryData", dao.BaseInfoGuid(strGuid));
        }

        #endregion

        #region  上报状态
        [Authorization]
        public ActionResult ReportState()
        {
            return PartialView("ReportState");
        }
        public ActionResult ReportStateResult(ReportStatViewModel model)
        {
            var dao = SpringContext.Context.GetObject("dao.CurrentMonth") as ICurrentMonth;
            string htmlPage = string.Empty;
            var dt = dao.ReportState(model, CurrentUser.EmpKind, 0, 1, out htmlPage);
            ViewData["htmlPage"] = htmlPage;
            return PartialView("ReportStateResult", dt);
        }

        public ActionResult ReportStatLook(ReportStatViewModel model)
        {
            var dao = SpringContext.Context.GetObject("dao.CurrentMonth") as ICurrentMonth;
            string htmlPage = string.Empty;
            var dt = dao.ReportState(model, CurrentUser.EmpKind, 0, 1, out htmlPage);
            ViewData["htmlPage"] = htmlPage;
            return PartialView("ReportStateResult", dt);
        }

        public ActionResult ReportStatepage(ReportStatViewModel model,string page)
        {
            var dao = SpringContext.Context.GetObject("dao.CurrentMonth") as ICurrentMonth;
            string htmlPage = string.Empty;
            var dt = dao.ReportState(model, CurrentUser.EmpKind, Int32.Parse(page), 1, out htmlPage);
            ViewData["htmlPage"] = htmlPage;
            return PartialView("ReportStateResult", dt);
        }
       
        #endregion

        [Authorization]
        public ActionResult ExportError()
        {
            return PartialView("ExportError");
        }

        [Authorization]
        public void ExportErrorDate(ErrorViewModel model)
        {
            var hist = SpringContext.Context.GetObject("dao.HistoryMonth") as IHistoryMonth;
            string filePath = "Error" + CurrentUser.LoginName + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
            ExcelHelper.ExportByWeb(hist.ExporError(model), "sheet1",filePath );
        }

        public ActionResult CheckLoginHead()
        {
            if (CurrentUser == null)
            {
                return PartialView("CheckLoginHead", "false");
            }
            return PartialView("CheckLoginHead", CurrentUser.ShowName);
        }

        public ActionResult CancelLogin()
        {
            HttpContext.Session.Remove("UserKey");
            return Content("Saved");
        }

        public ActionResult DeleteError(string teamname,string errorteam,string errortime,string erroritem)
        {
            var hist = SpringContext.Context.GetObject("dao.HistoryMonth") as IHistoryMonth;
            hist.DeleteError(new ErrorViewModel{errorType = erroritem,strDate = errortime,strUnit = teamname,unitName = errorteam});
            return Content("OK");
        }
    }
}

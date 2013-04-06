using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LookFund.Models.Membership;

namespace LookFund.Controllers
{
    public abstract class AbstractPageController : Controller
    {

        public static string SessionUserKey = "UserKey";
        public MEmploye CurrentUser
        {
            get
            {
                return HttpContext.Session["UserKey"] as MEmploye;
            }
            set
            {
                HttpContext.Session["UserKey"] = value;
            }
        }

        //public AbstractPageController()
        //{
        //    var model = HttpContext.Session["UserKey"] as MEmploye;
        //    //if (model == null)
        //    //{
        //    //    ViewData["IsLogin"] = "False";
        //    //}
        //    //else
        //    //{
        //    //    ViewData["IsLogin"] = CurrentUser.ShowName;
        //    //}
        //}
        //public ActionResult GetUserName()
        //{
        //    if (CurrentUser == null)
        //    {
        //        return Content("False");
        //    }
        //    return Content(CurrentUser.ShowName);
        //}
    }
}

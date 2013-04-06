using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TglFirst.Core;
using LookFund.Dao.Membership;
using LookFund.Models.Membership;
using LookFund.Models.ViewModel;
using LookFund.Controllers.Filter;
using NHibernate;
using LookFund.Controllers.ValidCode;

namespace LookFund.Controllers
{
    public class HomeController : AbstractPageController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid && model.Login(ModelState))
            {
                return RedirectToAction("AdminMain", "Account");
            }
            else
            {
                ModelState.SetModelValue("ValidCode", new ValueProviderResult("", "", null));
            }
            return View();
        }

        [HttpPost]
        public ActionResult LoginIndex(LoginViewModel model)
        {
            if (ModelState.IsValid && model.Login(ModelState))
            {
                return RedirectToAction("AdminMain", "Account");
            }
            return View("Index");
        }

        public ActionResult GetValidateCode()
        {
            ValidateCode vCode = new ValidateCode();
            string code = vCode.CreateValidateCode(5);
            Session["ValidateCode"] = code;
            byte[] bytes = vCode.CreateValidateGraphic(code);
            return File(bytes, @"image/jpeg");
        }
    }
}

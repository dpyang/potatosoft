using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using TglFirst.Core;
using LookFund.Dao.Membership;
using LookFund.Dao;

namespace LookFund.Models.ViewModel
{
    public class PwdEditViewModel
    {
        [Required(ErrorMessage = "旧密码不能为空。")]
        public string OldPwd
        {
            get;
            set;
        }
        [Required(ErrorMessage = "新密码不能为空。")]
        public string NewPwd
        {
            get;
            set;
        }
         [Required(ErrorMessage = "密码密码不能为空。")]
        public string ConfirmPwd
        {
            get;
            set;
        }

         public bool UpdatePwd(ModelStateDictionary state, Guid guid)
         {
             if (!string.Equals(NewPwd, ConfirmPwd))
             {
                 state.AddModelError("ConfirmPwd", "两次输入的密码不一至");
             }
             if (state.IsValid)
             {
                 try
                 {
                     var EmpDao = SpringContext.Context.GetObject("dao.Employe") as IEmploye;
                     var model = EmpDao.Get(guid);
                     if (string.Equals(DES.DESEncrypt(OldPwd),model.LoginPwk))
                     {
                         model.LoginPwk = DES.DESEncrypt(NewPwd);
                         EmpDao.Update(model);
                         return true;
                     }
                     else
                     {
                         state.AddModelError("OldPwd", "原密码错误请重新输入");
                     }
                 }
                 catch
                 {
                     return false;
                 }
             }
             return state.IsValid;
         }
    }
}
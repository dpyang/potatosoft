using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using TglFirst.Core;
using LookFund.Dao.Membership;
using LookFund.Dao;
using LookFund.Dao.FundInfo;

namespace LookFund.Models.ViewModel
{
    public class LoginViewModel
    {
        /// <summary>
        /// 设置获取用户的登录名
        /// </summary>
        /// <value>The username.</value>
        [Required(ErrorMessage = "身份证号码不能为空。")]
        public string LoginName
        {
            get;
            set;
        }

        /// <summary>
        /// 设置获取用户的登录密码
        /// </summary>
        /// <value>The password.</value>
       [Required(ErrorMessage = "登录密码不能为空。")]
        public string LoginPwk
        {
            get;
            set;
        }

        /// <summary>
        /// 验证码
        /// </summary>
        [Required(ErrorMessage = "验证码不能为空。")]
        public string ValidCode
        {
            get;
            set;
        }

        public string strAddress
        {
            get;
            set;
        }

        public bool Login(ModelStateDictionary state)
        {
            var strCode="123456";
            if(HttpContext.Current.Session["ValidateCode"] != null)
            strCode=  HttpContext.Current.Session["ValidateCode"].ToString();
            if (!ValidCode.Equals(strCode))
            {
                state.AddModelError("ValidCode", "验证码错误。");
                return false;
            }
            if (string.Equals(strAddress, "0"))
            {
                state.AddModelError("strAddress", "请选择所属单位。");
                return false;
            }
            var EmpDao = SpringContext.Context.GetObject("dao.Employe") as IEmploye;
            var list= EmpDao.Query("LoginName", LoginName);
            if (list.Count == 1)
            { 
                var model=list.First();
                if (string.Equals("22", model.EmpKind))
                {
                    var dao = SpringContext.Context.GetObject("dao.BasicInfo") as IBasicInfo;
                    var str = dao.GetGanlibu(model.LoginName.ToString());
                    if (string.Equals(str, strAddress))
                    {
                        if (!model.LoginPwk.Equals(DES.DESEncrypt(LoginPwk)))
                        {
                            state.AddModelError("LoginPwk", "密码错误。");
                        }
                    }
                    else
                    {
                        state.AddModelError("strAddress", "你无权登录所选择的单位");
                    }
                }
                else
                {
                    if (strAddress.Contains(model.EmpKind))
                    {
                        if (!model.LoginPwk.Equals(DES.DESEncrypt(LoginPwk)))
                        {
                            state.AddModelError("LoginPwk", "密码错误。");
                        }
                    }
                    else
                    {
                        state.AddModelError("strAddress", "你无权登录所选择的单位");
                    }
                }
            }
            else
            {
                state.AddModelError("LoginName", "身份证号不存在");
            }
            if (state.IsValid)
            {
                HttpContext.Current.Session.Add("UserKey", list.First());
            }
            return state.IsValid;
        }
    }
}
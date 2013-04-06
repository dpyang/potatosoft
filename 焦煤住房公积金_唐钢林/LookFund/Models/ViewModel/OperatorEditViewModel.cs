using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using TglFirst.Core;
using LookFund.Dao.Membership;
using LookFund.Models.Membership;
using LookFund.Dao;

namespace LookFund.Models.ViewModel
{
    public class OperatorEditViewModel
    {
        public string Uid
        {
            get;
            set;
        }
        /// <summary>
        /// 设置获取用户的登录名
        /// </summary>
        /// <value>The username.</value>
        [Required(ErrorMessage = "身份证号不能为空。")]
        public string LoginName
        {
            get;
            set;
        }

    

        /// <summary>
        /// 系统显示的用户名
        /// </summary>
        /// <value>The name of the show.</value>
       [Required(ErrorMessage = "名称不能为空。")]
        public string ShowName
        {
            set;
            get;
        }

        /// <summary>
        /// 所属矿物局
        /// </summary>
        public string EmpKind
        {
            get;
            set;
        }
        /// <summary>
        /// 设置获取用户的登录密码
        /// </summary>
        /// <value>The password.</value>
        public string LoginPwk
        {
            get;
            set;
        }
        /// <summary>
        /// 确认密码
        /// </summary>
        public string ConfirmPwd
        {
            get;
            set;
        }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string EmpPhone
        {
            get;
            set;
        }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string EmpEmail
        {
            get;
            set;
        }

        public bool AddSave(ModelStateDictionary state)
        {
            if (string.IsNullOrEmpty(LoginPwk.Trim()))
            {
                state.AddModelError("LoginPwk", "登录密码不能为空！");
            }
            if (string.IsNullOrEmpty(ConfirmPwd.Trim()))
            {
                state.AddModelError("ConfirmPwd", "确认密码不能为空！");
            }
            if (!string.Equals(LoginPwk, ConfirmPwd))
            {
                state.AddModelError("ConfirmPwd", "两次输入的密码不一至");
            }
            if (state.IsValid)
            {
              
                var dao = SpringContext.Context.GetObject("dao.Employe") as IEmploye;
                var list = dao.Query("LoginName", LoginName);
                if (list.Count == 0)
                {
                    var menu = SpringContext.Context.GetObject("dao.FunctionMenu") as IFunctionMenu;
                    MEmploye model = new MEmploye();
                    model.LoginName = LoginName;
                    model.ShowName = ShowName;
                    model.LoginPwk = DES.DESEncrypt(LoginPwk);
                    model.FunctionList = menu.AdminMenuList();
                    model.EmpKind = EmpKind;
                    model.EmpPhone = EmpPhone;
                    model.EmpEmail = EmpEmail;
                    dao.Save(model);
                }
                else
                {
                    state.AddModelError("LoginName", "身份证号重复!");
                }
            }
            return state.IsValid;
        }

        public bool UpdateSave(ModelStateDictionary state)
        {
            var dao = SpringContext.Context.GetObject("dao.Employe") as IEmploye;
            var list= dao.Query("LoginName", LoginName);
            if (list.Count > 0)
            {
                if (!string.Equals(list.First().Uid.ToString(), Uid))
                {
                    state.AddModelError("LoginName", "登录名/身份证号重复");
                }
            }
            if (state.IsValid)
            {                
                var model = dao.Get(new Guid(Uid));
                model.LoginName = LoginName;
                model.ShowName = ShowName;
                model.EmpKind = EmpKind;
                model.EmpPhone = EmpPhone;
                model.EmpEmail = EmpEmail;
                dao.Update(model);
            }
            return state.IsValid;
        }
    }
}
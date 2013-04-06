using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TglFirst.Core.Model.Example;
using System.Web.Mvc;

namespace LookFund.Models.Membership
{
    public class MEmploye : User
    {
        private string _kind = "22";

        /// <summary>
        /// 管理员级别
        /// </summary>
        public virtual string EmpKind
        {
            get
            {
                return _kind;
            }
            set
            {
                _kind = value;
            }
        }

        /// <summary>
        /// 电话
        /// </summary>
        public virtual string EmpPhone
        {
            get;
            set;
        }
        /// <summary>
        /// 邮箱
        /// </summary>
        public virtual string EmpEmail
        {
            get;
            set;
        }

        /// <summary>
        /// 功能列表
        /// </summary>
        public virtual ICollection<FunctionMenu> FunctionList
        {
            get;
            set;
        }

        /// <summary>
        /// 问题列表
        /// </summary>
        public virtual ICollection<MComments> CommentsList
        {
            get;
            set;
        }

        public static SelectList SelectListEnum(Type enumType)
        {
            IDictionary<int, string> dic = EnumToIEnumerable(enumType);
            return new SelectList(dic, "Key", "Value");
        }

        public static SelectList SelectListEnum(Type enumType, bool please)
        {
            if (please)
            {
                IDictionary<int, string> dic = EnumToIEnumerable(enumType);
                dic.Add(0, "＝请选择＝");
                return new SelectList(dic, "Key", "Value", 0);
            }
            return SelectListEnum(enumType);
        }

        public static SelectList SelectListEnum(Type enumType, bool please, bool buser)
        {
            if (please)
            {
                IDictionary<int, string> dic = new Dictionary<int, string>();
                dic.Add(0, "＝请选择＝");
                if (buser)
                {
                    dic = EnumToIEnumerable(enumType);
                }
                else
                {
                    foreach (int item in Enum.GetValues(enumType))
                    {
                        dic.Add(item, Enum.GetName(enumType, item));
                    }
                }
                return new SelectList(dic, "Key", "Value", 0);
            }
            return SelectListEnum(enumType);
        }

        private static IDictionary<int, string> EnumToIEnumerable(Type enumType)
        {
            IDictionary<int, string> dic = new Dictionary<int, string>();
            var model = HttpContext.Current.Session["UserKey"] as MEmploye;
            if (model != null)
            {
                foreach (int item in Enum.GetValues(enumType))
                {
                    if (item.ToString().Contains(model.EmpKind))
                    {
                        dic.Add(item, Enum.GetName(enumType, item));
                    }
                }
            }
            return dic;
        }
    }

    /// <summary>
    /// 操作员级别
    /// </summary>
    public enum EmpKind
    {
        集团管理员 = 11,
        焦煤本部 = 1101,
        西山煤电管理部 = 1102,
        汾西矿务管理部= 1103,
        霍州煤电管理部 = 1104
    }

    public enum Company
    {
        焦煤本部 = 110101,
        西山煤电管理部 = 110201,
        汾西矿务管理部 = 110301,
        霍州煤电管理部 = 110401
    }
}
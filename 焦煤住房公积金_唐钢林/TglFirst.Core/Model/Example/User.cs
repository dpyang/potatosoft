using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace TglFirst.Core.Model.Example
{
    public abstract class User : AbstractModel
    {
        /// <summary>
        /// 设置获取用户的登录名
        /// </summary>
        /// <value>The username.</value>
        public virtual string LoginName
        {
            get;
            set;
        }

        /// <summary>
        /// 设置获取用户的登录密码
        /// </summary>
        /// <value>The password.</value>
        public virtual string LoginPwk
        {
            get;
            set;
        }

        /// <summary>
        /// 系统显示的用户名
        /// </summary>
        /// <value>The name of the show.</value>
        public virtual string ShowName
        {
            set;
            get;
        }
    }
}

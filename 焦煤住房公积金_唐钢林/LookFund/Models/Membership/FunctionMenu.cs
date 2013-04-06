using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TglFirst.Core.Model;

namespace LookFund.Models.Membership
{
    public class FunctionMenu : AbstractModel
    { 
        
        /// <summary>
        /// 层次编码
        /// </summary>
        public virtual string LevelCode
        {
            get;
            set;
        }
        /// <summary>
        /// 功能名称
        /// </summary>
        public virtual string FunctionName
        {
            get;
            set;
        }     
        /// <summary>
        /// 下级编码最大数
        /// </summary>
        public virtual int NextNodeNumber
        {
            get;
            set;
        }
        /// <summary>
        /// 打开的路径
        /// </summary>
        public virtual string ActionName
        {
            get;
            set;
        }

    }
}
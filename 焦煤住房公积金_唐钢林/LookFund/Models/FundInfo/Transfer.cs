using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TglFirst.Core.Model;

namespace LookFund.Models.FundInfo
{
    public abstract class Transfer : AbstractModel
    {

        /// <summary>
        /// 调出单位
        /// </summary>
        public virtual string OutUnit
        {
            get;
            set;
        }
        /// <summary>
        /// 调出部门
        /// </summary>
        public virtual string OutDept
        {
            get;
            set;
        }
       

        /// <summary>
        /// 调入单位
        /// </summary>
        public virtual string JoinUnit
        {
            get;
            set;
        }
        /// <summary>
        /// 调入部门
        /// </summary>
        public virtual string JoinDept
        {
            get;
            set;
        }
    }
}
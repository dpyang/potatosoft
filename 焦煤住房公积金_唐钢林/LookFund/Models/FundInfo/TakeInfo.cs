using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TglFirst.Core.Model;

namespace LookFund.Models.FundInfo
{
    public class TakeInfo : AbstractModel
    {

        /// <summary>
        /// 支取类型
        /// </summary>
        public virtual string TakeType
        {
            get;
            set;
        }

        /// <summary>
        /// 支取时间
        /// </summary>
        public virtual DateTime TakeDate
        {
            get;
            set;
        }

        /// <summary>
        /// 支取金额
        /// </summary>
        public virtual int TakeMoney
        {
            get;
            set;
        }

        /// <summary>
        /// 审核人
        /// </summary>
        public virtual string AuditPerson
        {
            get;
            set;
        }

        public virtual BasicInfo BelongBasicInfo
        {
            get;
            set;
        }
    }
}
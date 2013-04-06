using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TglFirst.Core.Model;

namespace LookFund.Models.FundInfo
{
    /// <summary>
    /// 公积金
    /// </summary>
    public abstract class Reserve : AbstractModel
    {
        /// <summary>
        /// 年月
        /// </summary>
        public virtual DateTime YearMonth
        {
            get;
            set;
        }

        /// <summary>
        /// 缴费基数
        /// </summary>
        public virtual decimal BaseNumber
        {
            get;
            set;
        }

        /// <summary>
        /// 交费单位
        /// </summary>
        public virtual string EmpUnit
        {
            get;
            set;
        }
        /// <summary>
        /// 外部转入
        /// </summary>
        public virtual decimal ExternalJoin
        {
            get;
            set;
        }
        /// <summary>
        /// 个人缴费
        /// </summary>
        public virtual decimal SelfCross
        {
            get;
            set;
        }

        /// <summary>
        /// 企业缴费
        /// </summary>
        public virtual decimal UnitCorss
        {
            get;
            set;
        }

        /// <summary>
        /// 个人补缴
        /// </summary>
        public virtual decimal SelfPlusCross
        {
            get;
            set;
        }

        /// <summary>
        /// 企业补缴
        /// </summary>
        public virtual decimal UnitPlusCross
        {
            get;
            set;
        }

        /// <summary>
        /// 转入金额
        /// </summary>
        public virtual decimal MoveJoin
        {
            get;
            set;
        }

        /// <summary>
        /// 转出金额
        /// </summary>
        public virtual decimal MoveOut
        {
            get;
            set;
        }
    }
}
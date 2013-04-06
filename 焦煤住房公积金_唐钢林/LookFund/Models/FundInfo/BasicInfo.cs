using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TglFirst.Core.Model;
using LookFund.Models.FundInfo;

namespace LookFund.Models.FundInfo
{
    public class BasicInfo : AbstractModel
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public virtual string EmpName
        {
            get;
            set;
        }

        /// <summary>
        /// 身份证号
        /// </summary>
        public virtual string EmpIdCard
        {
            get;
            set;
        }

        /// <summary>
        /// 性别
        /// </summary>
        public virtual string EmpSex
        {
            get;
            set;
        }

        /// <summary>
        /// 状态
        /// </summary>
        public virtual string EmpState
        {
            get;
            set;
        }

        /// <summary>
        /// 职工帐号
        /// </summary>
        public virtual string EmpAccount
        {
            get;
            set;
        }

        /// <summary>
        /// 所属单位
        /// </summary>
        public virtual string EmpUnit
        {
            get;
            set;
        }

        /// <summary>
        /// 所属部门
        /// </summary>
        public virtual string EmpDept
        {
            get;
            set;
        }

        /// <summary>
        /// 期初余额
        /// </summary>
        public virtual decimal StartMoney
        {
            get;
            set;
        }

        /// <summary>
        /// 支取信息
        /// </summary>
        public virtual ICollection<TakeInfo> EmpTakeInfo
        {
            get;
            set;
        }

        /// <summary>
        /// 变更信息
        /// </summary>
        public virtual ICollection<Transfer> TransferInfo
        {
            get;
            set;
        }

        /// <summary>
        /// 公积金信息
        /// </summary>
        public virtual ICollection<Reserve> ReserveInfo
        {
            get;
            set;
        }        
    }
}
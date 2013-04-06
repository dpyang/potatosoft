using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TglFirst.Core.Model;

namespace LookFund.Models.FundInfo
{
    public class TransferJoin : Transfer
    {
        /// <summary>
        /// 调入日期
        /// </summary>
        public virtual DateTime JoinDate
        {
            get;
            set;
        }
    }
}
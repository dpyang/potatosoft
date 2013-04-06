using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TglFirst.Core.Model;

namespace LookFund.Models.FundInfo
{
    public class CurrentMonth : Reserve
    {
        /// <summary>
        /// 上月结存
        /// </summary>
        public virtual decimal LastMonthSurplus
        {
            get;
            set;
        }

       
    }
}
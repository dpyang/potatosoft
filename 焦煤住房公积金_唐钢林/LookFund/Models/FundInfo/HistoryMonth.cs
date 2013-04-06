using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TglFirst.Core.Model;

namespace LookFund.Models.FundInfo
{
    public class HistoryMonth : Reserve
    {
        /// <summary>
        /// 本月结存
        /// </summary>
        public virtual decimal MnethSurplus
        {
            get;
            set;
        }
    }
}
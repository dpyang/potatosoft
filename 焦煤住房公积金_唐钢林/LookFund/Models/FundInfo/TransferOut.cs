using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TglFirst.Core.Model;

namespace LookFund.Models.FundInfo
{
    public class TransferOut : Transfer
    {
        /// <summary>
        /// 调出类型
        /// </summary>
        public virtual string OutType
        {
            get;
            set;
        }

        /// <summary>
        /// 调出时间
        /// </summary>
        public virtual DateTime OutDate
        {
            get;
            set;
        }
    }
}
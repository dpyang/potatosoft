using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TglFirst.Core.Model;

namespace LookFund.Models.FundInfo
{
    public abstract class  AbstractType
    {
        /// <summary>
        /// 编码
        /// </summary>
        public string TypeCode
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string TypeName
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
    }
}
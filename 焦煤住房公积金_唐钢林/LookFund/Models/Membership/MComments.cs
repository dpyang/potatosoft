using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TglFirst.Core.Model;

namespace LookFund.Models.Membership
{
    public class MComments : AbstractModel
    {
        /// <summary>
        /// 意见内容
        /// </summary>
        public virtual string Conent
        {
            get;
            set;
        }

        /// <summary>
        /// 意见提出时间
        /// </summary>
        public virtual DateTime CommDate
        {
            get;
            set;
        }

        /// <summary>
        /// 回复
        /// </summary>
        public virtual string Reply
        {
            get;
            set;
        }

        /// <summary>
        /// 发表意见人员
        /// </summary>
        public virtual MEmploye ComPerson
        {
            get;
            set;
        }
    }
}
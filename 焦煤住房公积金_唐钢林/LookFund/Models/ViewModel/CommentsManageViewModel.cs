using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LookFund.Dao.Membership;
using TglFirst.Core;
using LookFund.Models.Membership;

namespace LookFund.Models.ViewModel
{
    public class CommentsManageViewModel
    {
        public Guid Uid
        {
            get;
            set;
        }
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
        /// 发布意见人
        /// </summary>
        public virtual string EmpName
        {
            get;
            set;
        }
        /// <summary>
        /// 发布意见人Uid
        /// </summary>
        public virtual Guid EmpUid
        {
            get;
            set;
        }

        //public void Save()
        //{
            
        //}
    }
}
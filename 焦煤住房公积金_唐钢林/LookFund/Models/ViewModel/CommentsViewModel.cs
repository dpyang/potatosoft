using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LookFund.Models.Membership;
using System.ComponentModel.DataAnnotations;
using TglFirst.Core;
using LookFund.Dao.Membership;

namespace LookFund.Models.ViewModel
{
    public class CommentsViewModel
    {
        private DateTime _commDate = DateTime.Now;
        /// <summary>
        /// 意见内容
        /// </summary>
        [Required(ErrorMessage = "意见内容不能为空。")]
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
            get
            {
                return _commDate;
            }
            set
            {
                _commDate = value;
            }
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


        public void Save()
        {
            var dao = SpringContext.Context.GetObject("dao.Comments") as IComments;
            MComments model = new MComments();
            model.CommDate = CommDate;
            model.ComPerson = ComPerson;
            model.Conent = Conent;
            dao.Save(model);
        }
    }
}
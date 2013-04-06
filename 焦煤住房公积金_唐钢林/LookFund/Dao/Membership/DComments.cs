using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LookFund.Models.Membership;
using TglFirst.Core.Repository;
using NHibernate.Transform;
using LookFund.Models.ViewModel;
using System.Data;
using System.Data.SqlClient;

namespace LookFund.Dao.Membership
{
    public interface  IComments : IRepository<MComments>
    {
        /// <summary>
        /// 获取子矿物局意见列表
        /// </summary>
        /// <param name="KuangWuju">所属矿物局</param>
        IList<CommentsManageViewModel> GetList(string KuangWuju);

        IList<CommentsManageViewModel> GetList(string KuangWuju, int page, int size, out string HtmlPage);
        IList<CommentsManageViewModel> GetList(string KuangWuju, int page, int size, out string HtmlPage, string strSearch);
        /// <summary>
        /// 问题统计
        /// </summary>
        /// <returns></returns>
        DataTable CommentsStat(string empKind);
    }
    public class DComments : RepositoryBase<MComments>, IComments
    {
        public IList<CommentsManageViewModel> GetList(string KuangWuju)
        {
            string sql = Session.GetNamedQuery("CommentBranch").QueryString
                .Replace("#BranchValue", KuangWuju);

            return Session.CreateSQLQuery(sql)
                .SetResultTransformer(Transformers.AliasToBean(typeof(CommentsManageViewModel)))
                .List<CommentsManageViewModel>();

        }


        public IList<CommentsManageViewModel> GetList(string KuangWuju, int page, int size, out string HtmlPage)
        {
            string SqlCount = Session.GetNamedQuery("CommentBranchCount").QueryString
                .Replace("#BranchValue", KuangWuju);
            int rows = Convert.ToInt32(Session.CreateSQLQuery(SqlCount).List()[0]);

            int yu = rows % size;
            int count = yu == 0 ? rows / size : rows / size + 1;

            page = page >= count ? count - 1 : page;
            int minpage = page - 5 > 0 ? page - 5 : 0;
            int maxpage = page + 6 < count ? page + 6 : count;
            HtmlPage = "<span style=\"font-size:25px; padding-right:20px;\">共" + rows + "行</span><span style='font-size:20px;'>共" + count + "页</span>&nbsp;&nbsp;&nbsp;&nbsp;";
            for (int i = minpage; i < maxpage; i++)
            {
                if (i == page)
                    HtmlPage += "<span style=\"font-size:25px; padding-right:5px;\"><b id='currentPage'>" + (i + 1) + "</b></span>";
                else
                    HtmlPage += "<a href=\"javascript:NextPageComments(" + i + ")\"><span style=\"font-size:20px; padding-right:5px;\">" + (i + 1) + "</span></a>";
            }
            string sql = Session.GetNamedQuery("CommentBranch").QueryString
                .Replace("#BranchValue", KuangWuju);

            return Session.CreateSQLQuery(sql)
                 .SetFirstResult(page * size)
                .SetMaxResults(size)
                .SetResultTransformer(Transformers.AliasToBean(typeof(CommentsManageViewModel)))
                .List<CommentsManageViewModel>();
        }

        public IList<CommentsManageViewModel> GetList(string KuangWuju, int page, int size, out string HtmlPage,string strSearch)
        {
            string strWhere = string.Empty;
            if (!string.IsNullOrEmpty(strSearch.Trim()))
                strWhere = " and (b.A11 like '%" + strSearch + "%' or b.A12 like '%" + strSearch + "%' or a.A13 like '%" + strSearch + "%')";
            string SqlCount = Session.GetNamedQuery("CommentBranchCount").QueryString
                .Replace("#BranchValue", KuangWuju) + strWhere;
            int rows = Convert.ToInt32(Session.CreateSQLQuery(SqlCount).List()[0]);

            int yu = rows % size;
            int count = yu == 0 ? rows / size : rows / size + 1;

            page = page >= count ? count - 1 : page;
            int minpage = page - 5 > 0 ? page - 5 : 0;
            int maxpage = page + 6 < count ? page + 6 : count;
            HtmlPage = "<span style=\"font-size:25px; padding-right:20px;\">共" + rows + "行</span><span style='font-size:20px;'>共" + count + "页</span>&nbsp;&nbsp;&nbsp;&nbsp;";
            for (int i = minpage; i < maxpage; i++)
            {
                if (i == page)
                    HtmlPage += "<span style=\"font-size:25px; padding-right:5px;\"><b id='currentPage'>" + (i + 1) + "</b></span>";
                else
                    HtmlPage += "<a href=\"javascript:NextPageComments(" + i + ")\"><span style=\"font-size:20px; padding-right:5px;\">" + (i + 1) + "</span></a>";
            }

            string sql = Session.GetNamedQuery("CommentBranch").QueryString
                .Replace("#BranchValue", KuangWuju) + strWhere;

            return Session.CreateSQLQuery(sql)
                 .SetFirstResult(page * size)
                .SetMaxResults(size)
                .SetResultTransformer(Transformers.AliasToBean(typeof(CommentsManageViewModel)))
                .List<CommentsManageViewModel>();
        }

        public DataTable CommentsStat(string empKind)
        {
            string sql = Session.GetNamedQuery("commentsStat").QueryString
                .Replace("#empKInd", empKind);
            var cmd = Session.Connection.CreateCommand();
            cmd.CommandText = sql;
            SqlDataAdapter da = new SqlDataAdapter(cmd as SqlCommand);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }
}
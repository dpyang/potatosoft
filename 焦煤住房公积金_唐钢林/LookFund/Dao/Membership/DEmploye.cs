using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TglFirst.Core.Repository;
using LookFund.Models.Membership;
using NHibernate.Criterion;
using TglFirst.Core;
using LookFund.Dao.FundInfo;

namespace LookFund.Dao.Membership
{
    public interface IEmploye : IRepository<MEmploye>
    {
        IList<MEmploye> GetList(int page, int size, out string HtmlPage,string empKInd);
        void OperatorDel(string uid);
        void OperatorFunSave(string arrCode, string empUId);
        bool PwdInit(string loginName,string empKind);
    }

    public class DEmploye : RepositoryBase<MEmploye>, IEmploye
    {
        public IList<MEmploye> GetList(int page, int size, out string HtmlPage, string empKInd)
        {
            int count = Session.CreateCriteria(typeof(MEmploye))
                .Add(Expression.Like("EmpKind", empKInd+"%"))
                .List().Count;
            int countPage = count % size == 0 ? count / size : count / size + 1;
            page = page >= countPage ? countPage - 1 : page;
            HtmlPage = string.Empty;
            for (int i = 0; i < countPage; i++)
            {
                if (i == page)
                    HtmlPage += "<span style=\"font-size:30px; padding-right:5px;\"><b id='currentPage'>" + (i + 1) + "</b></span>";
                else
                    HtmlPage += "<a href=\"javascript:OperatorNextPage(" + i + ")\"><span style=\"font-size:30px; padding-right:5px;\">" + (i + 1) + "</span></a>";
            }
            return Session.CreateCriteria(typeof(MEmploye))
                .Add(Expression.Like("EmpKind", empKInd + "%"))
                .SetFirstResult(page * size)
                .SetMaxResults(size)
                .List<MEmploye>();
        }

        public void OperatorDel(string uid)
        {
            var model = Get(new Guid(uid));
            Delete(model);
        }

        public void OperatorFunSave(string arrCode, string empUId)
        {
            string[] arr = arrCode.Split(',');
            var menu = SpringContext.Context.GetObject("dao.FunctionMenu") as IFunctionMenu;
            var model = Get(new Guid(empUId));
            model.FunctionList = menu.GetMenuList(arr);
            Update(model);
        }

        public bool PwdInit(string loginName,string empKind)
        {
            var list = Query("LoginName", loginName);
            if (list.Count == 1)
            {
                var EmpDao = SpringContext.Context.GetObject("dao.BasicInfo") as IBasicInfo;
                Guid guid = list.First().Uid;
                if (!EmpDao.GetEmpKind(guid.ToString()).Contains(empKind))
                    return false;                 
                var model = Get(guid);
                model.LoginPwk = DES.DESEncrypt("123456");
                Update(model);
                return true;
            }
            return false;
        }

    }
}
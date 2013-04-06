using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spring.Data.NHibernate.Generic.Support;
using Spring.Transaction.Interceptor;
using NHibernate.Criterion;

namespace TglFirst.Core.Repository
{
    public class RepositoryBase<T> : HibernateDaoSupport, IRepository<T>
    {

        #region IRepository<T> 成员

        public T Load(object uid)
        {
            return HibernateTemplate.Load<T>(uid);
        }

        public T Get(object uid)
        {
            return HibernateTemplate.Get<T>(uid);
        }

        public IList<T> GetAll()
        {
            return HibernateTemplate.LoadAll<T>();
        }

        public void Save(T model)
        {
            HibernateTemplate.Save(model);
        }

        public void SaveOrUpdate(T model)
        {
            HibernateTemplate.SaveOrUpdate(model);
        }

        public void Update(T model)
        {
            HibernateTemplate.Update(model);
        }

        public void Delete(T model)
        {
            HibernateTemplate.Delete(model);
        }

        public IList<T> Query(string propertyName, object value)
        {
            return Session.CreateCriteria(typeof(T))
                .Add(Restrictions.Eq(propertyName, value))
                .List<T>();
        }

        #endregion
    }
}

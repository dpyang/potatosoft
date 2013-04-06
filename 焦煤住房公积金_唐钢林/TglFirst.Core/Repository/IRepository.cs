using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TglFirst.Core.Repository
{
    public interface IRepository<T>
    {
        T Load(object uid); 
        T Get(object uid);
        IList<T> GetAll();
        void Save(T model);
        void SaveOrUpdate(T model); 
        void Update(T model);
        void Delete(T model);
        IList<T> Query(string propertyName, object value);
    }
}

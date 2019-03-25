using System;
using System.Collections.Generic;
using System.Text;

namespace BizDbAccess.GenericInterfaces
{
    public interface IEntityDbAccess<T> where T : class
    {
        IEnumerable<T> GetAll();
        void Add(T entity);
        void Delete(T entity);
        T Update(T entity, T toUpd);
    }
}

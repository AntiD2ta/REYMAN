using System;
using System.Collections.Generic;
using System.Text;

namespace BizDbAccess.GenericInterfaces
{
    /// <summary>
    /// Represents a repository for the entity, responsible for access to the database. 
    /// </summary>
    /// <typeparam name="T">Entity's type</typeparam>
    public interface IEntityDbAccess<T> where T : class
    {
        IEnumerable<T> GetAll();
        void Add(T entity);
        void Delete(T entity);
        T Update(T entity, T toUpd);
    }
}

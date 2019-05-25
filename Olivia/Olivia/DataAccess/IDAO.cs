using System;
using System.Collections.ObjectModel;

namespace Olivia.DataAccess
{
    public interface IDAO<T>
    {
        T Insert(T obj);

        bool remove(T obj);

        T FindById(int id);

        bool Update(T obj);

        Collection<T> ListAll();
    }
}

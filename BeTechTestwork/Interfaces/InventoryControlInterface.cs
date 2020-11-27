using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeTechTestwork.Interfaces
{
    interface InventoryControlInterface<T>
    {
        IEnumerable<T> GetList();
        void Create(T item);
        void Update(T item);
        void Delete(string id);
    }
}

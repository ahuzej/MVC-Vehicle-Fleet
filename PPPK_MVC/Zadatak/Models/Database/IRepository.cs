using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak.Models.Database
{
    public interface IRepository<T>
    {
        T GetById(int? id);
        IEnumerable<T> List();
        int Add(T entity);
        int Delete(int? id);
    }
}

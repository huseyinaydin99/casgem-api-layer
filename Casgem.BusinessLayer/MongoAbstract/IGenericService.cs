using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Casgem.BusinessLayer.MongoAbstract
{
    public interface IGenericService<T> where T : class, new()
    {
        public void TDelete(T t);
        public void TInsert(T t);
        public void TUpdate(T t);
        public T TGetById(int id);
        public List<T> TGetList();
    }
}
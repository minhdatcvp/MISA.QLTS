using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MISA.QLTS.BACKEND.DATALAYER.Interface
{
    public interface IBaseData<T>
    {
        IEnumerable<T> GetAll(string sqlCommand=null, object parameters = null, CommandType commandType = CommandType.Text);
        T GetById(Guid id);
        int Insert(T entity);
        int Update(T entity);
        int Delete(Guid id);
    }
}

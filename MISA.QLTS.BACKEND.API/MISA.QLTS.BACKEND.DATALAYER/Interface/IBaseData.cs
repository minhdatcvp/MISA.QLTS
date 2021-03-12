using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MISA.QLTS.BACKEND.DATALAYER.Interface
{
    public interface IBaseData<T>
    {
        IEnumerable<T> GetAll(string sqlCommand=null, object parameters = null, CommandType commandType = CommandType.Text);
    }
}

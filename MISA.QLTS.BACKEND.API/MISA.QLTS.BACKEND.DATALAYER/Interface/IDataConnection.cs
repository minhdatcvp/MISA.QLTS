using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MISA.QLTS.BACKEND.DATALAYER.Interface
{
    /// <summary>
    /// Interface tham chiếu tới class kết nối database
    /// </summary>
    /// <typeparam name="T">(Entity : Asset or Decrement)</typeparam>
    public interface IDataConnection<T>
    {
        IEnumerable<T> Query(string sqlCommand, DynamicParameters parameters = null, CommandType commandType = CommandType.Text);

        int Excute(string sqlCommand, DynamicParameters parameters = null, CommandType commandType = CommandType.Text);

        T QueryFirst(string sqlCommand, DynamicParameters parameters = null, CommandType commandType = CommandType.Text);
    }
}

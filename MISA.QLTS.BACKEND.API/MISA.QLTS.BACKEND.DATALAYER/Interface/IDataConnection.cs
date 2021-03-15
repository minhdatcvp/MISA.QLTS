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
        /// <summary>
        /// Sử dụng dapper thực hiện truy vấn lấy
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        IEnumerable<T> Query(string sqlCommand, DynamicParameters parameters = null, CommandType commandType = CommandType.Text);
        /// <summary>
        /// Sử dụng dapper trả về số dòng
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        int Excute(string sqlCommand, DynamicParameters parameters = null, CommandType commandType = CommandType.Text);
        /// <summary>
        /// Lấy giá trị đầu tiên trả về
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <param name="parameters"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        T QueryFirst(string sqlCommand, DynamicParameters parameters = null, CommandType commandType = CommandType.Text);
    }
}

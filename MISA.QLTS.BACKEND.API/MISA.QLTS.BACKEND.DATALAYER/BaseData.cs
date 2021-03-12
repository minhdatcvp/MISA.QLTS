using MISA.QLTS.BACKEND.DATALAYER.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MISA.QLTS.BACKEND.DATALAYER
{
    /// <summary>
    /// Datalayer dùng chung cho các entity
    /// Có các phương thức thêm sửa xóa
    /// </summary>
    /// <typeparam name="T">(Entity : Asset or Decrement)</typeparam>
    public class BaseData<T> : IBaseData<T>
    {
        // Khởi tạo tham chiếu interface
        private readonly IDataConnection<T> _dataConnection;

        #region Contructor
        /// <summary>
        /// Hàm khởi tạo BaseConnect khai báo dbContext
        /// </summary>
        /// <param name="dbContext"></param>
        public BaseData(IDataConnection<T> dataConnection)
        {
            _dataConnection = dataConnection;
        }
        #endregion

        #region Method
        /// <summary>
        /// Lấy dữ liệu theo tham số truyền vào nếu không truyền vào mặc định là lấy tất cả
        /// </summary>
        /// <typeparam name="T">Đối tượng cần lấy dữ liệu</typeparam>
        /// <param name="sqlCommand">Tên Store lấy dữ liệu</param>
        /// <param name="parameters">Dữ liệu truyền vào( lấy theo id,code,...)</param>
        /// <param name="commandType">Kiểu lấy dữ liệu</param>
        /// <returns>Trả về thông tin cần lấy</returns>
        public virtual IEnumerable<T> GetAll(string sqlCommand, object parameters = null, CommandType commandType = CommandType.Text)
        {
            var procName = "Proc_SelectFixed" +typeof(T).Name +"Datas";
            var listEntity = _dataConnection.Query(procName, commandType: CommandType.StoredProcedure);
            return listEntity;
        }
        #endregion



    }
}

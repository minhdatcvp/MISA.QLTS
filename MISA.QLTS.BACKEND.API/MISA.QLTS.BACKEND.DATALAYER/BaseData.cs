using Dapper;
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
        //public string buildStore(int typeStore)
        //{
        //    var type = new TypeStore();
        //    var sqlCommand = string.Empty;
        //    var className = typeof(T).Name;
        //    switch (typeStore)
        //    {
        //        case 1:
        //            sqlCommand = "Proc_SelectFixedAssetDatas";
        //            break;
        //        case 2:
        //            sqlCommand = "Proc_SelectFixedAssetDatas";
        //            break;
        //        case 3:
        //            sqlCommand = "Proc_SelectFixedAssetDatas";
        //            break;
        //        case 4:
        //            sqlCommand = "Proc_SelectFixedAssetDatas";
        //            break;
        //        case 5:
        //            sqlCommand = "Proc_SelectFixedAssetDatas";
        //            break;
        //        default:
        //            break;
        //    }
        //    return sqlCommand;
        //}


        /// <summary>
        /// Lấy dữ liệu theo tham số truyền vào nếu không truyền vào mặc định là lấy tất cả
        /// </summary>
        /// <typeparam name="T">Đối tượng cần lấy dữ liệu</typeparam>
        /// <param name="sqlCommand">Tên Store lấy dữ liệu</param>
        /// <param name="parameters">Dữ liệu truyền vào( lấy theo id,code,...)</param>
        /// <param name="commandType">Kiểu lấy dữ liệu</param>
        /// <returns>Trả về thông tin cần lấy</returns>
        public IEnumerable<T> GetAll(string sqlCommand = null, object parameters = null, CommandType commandType = CommandType.Text)
        {
            var procName = "Proc_Select" + typeof(T).Name + "Datas";
            var listEntity = _dataConnection.Query(procName, commandType: CommandType.StoredProcedure);
            return listEntity;
        }
        /// <summary>
        /// Thực thi proceduce lấy theo id
        /// Đặt tên proc theo kiểu Proc_Get...ById
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Danh sách thực thể</returns>
        public T GetById(Guid id)
        {
            var procName = "Proc_Select" + typeof(T).Name + "ById";
            var parameters = new DynamicParameters(id);
            var result = _dataConnection.QueryFirst(procName, parameters, CommandType.StoredProcedure);
            return result;
        }
        /// <summary>
        /// Thực thi proceduce insert
        /// Đặt tên proc theo kiểu Proc_Insert...
        /// </summary>
        /// <param name="entity">Thực thể cần thêm</param>
        /// <returns>Số bản ghi thay đổi</returns>
        public int Insert(T entity)
        {
            var procName = "Proc_Insert" + typeof(T).Name;
            var parameters = new DynamicParameters(entity);
            var result = _dataConnection.Excute(procName, parameters, CommandType.StoredProcedure);
            return result;
        }
        /// <summary>
        /// Thực thi proceduce update
        /// Đặt tên proc theo kiểu Proc_Update...
        /// </summary>
        /// <param name="entity">Thực thể cần sửa</param>
        /// <returns>Số bản ghi thay đổi</returns>
        public int Update(T entity)
        {
            var procName = "Proc_Update" + typeof(T).Name;
            var parameters = new DynamicParameters(entity);
            var result = _dataConnection.Excute(procName, parameters, CommandType.StoredProcedure);
            return result;
        }

        /// <summary>
        /// Thực thi proceduce delete
        /// Đặt tên proc theo kiểu Proc_Delete...
        /// </summary>
        /// <param name="entity">Thực thể cần xóa</param>
        /// <returns>Số bản ghi thay đổi</returns>
        public int Delete(Guid id)
        {
            var procName = "Proc_Delete" + typeof(T).Name;
            var parameters = new DynamicParameters(id);
            var result = _dataConnection.Excute(procName, parameters, CommandType.StoredProcedure);
            return result;
        }
        #endregion
    }
}

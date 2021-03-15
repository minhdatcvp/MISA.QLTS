using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MISA.QLTS.BACKEND.DATALAYER.Interface
{
    /// <summary>
    /// Interface tham chiếu tới baseData
    /// </summary>
    /// <typeparam name="T">entity</typeparam>
    /// CreatedBy: NMDAT(14/03/2021)
    public interface IBaseData<T>
    {
        /// <summary>
        /// Lấy toàn bộ danh sách
        /// </summary>
        /// <param name="sqlCommand">truy vấn</param>
        /// <param name="parameters">param truyền vào</param>
        /// <param name="commandType">Kiểu truy vấn</param>
        /// <returns></returns>
        IEnumerable<T> GetAll(string sqlCommand=null, object parameters = null, CommandType commandType = CommandType.Text);
        /// <summary>
        /// Thêm dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
          
        /// <summary>
        /// Lấy theo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetById(Guid id);
        int Insert(T entity);
        /// <summary>
        /// Sửa dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Update(T entity);
        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Delete(T entity);
    }
}

using MISA.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.QLTS.BACKEND.SERVICE.Interface
{
    /// <summary>
    /// interface tham chiếu tới service cha
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseService<T>
    {
        /// <summary>
        /// Phương thức Get
        /// </summary>
        /// <returns></returns>
        ServiceResult Get();
        /// <summary>
        /// Phương thức thêm
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        ServiceResult Insert(T entity);
        /// <summary>
        /// Phương thức Sửa
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        ServiceResult Update(T entity);
        /// <summary>
        /// Phương thức xóa
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        ServiceResult Delete(T entity);
        /// <summary>
        /// Lấy theo Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ServiceResult GetById(Guid id);
    }
}

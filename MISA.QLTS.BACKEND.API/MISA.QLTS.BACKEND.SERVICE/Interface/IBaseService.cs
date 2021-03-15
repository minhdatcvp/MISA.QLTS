using MISA.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.QLTS.BACKEND.SERVICE.Interface
{
    public interface IBaseService<T>
    {
        /// <summary>
        /// Phương thức Get
        /// </summary>
        /// <returns></returns>
        ServiceResult Get();
        /// <summary>
        /// Get dữ liệu theo Id
        /// </summary>
        /// <returns></returns>
        ServiceResult GetById(Guid id);
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
        ServiceResult Delete(Guid id);
    }
}

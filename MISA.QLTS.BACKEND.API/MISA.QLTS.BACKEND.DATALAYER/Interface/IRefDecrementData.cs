using MISA.QLTS.BACKEND.COMMON.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.QLTS.BACKEND.DATALAYER.Interface
{
    /// <summary>
    /// Interface tham chiếu tới Danh sách chứng từ
    /// </summary>
    public interface IRefDecrementData : IBaseData<RefDecrement>
    {
        /// <summary>
        /// Check trùng mã
        /// </summary>
        /// <param name="ref_no"></param>
        /// <returns></returns>
        RefDecrement CheckRefDecrementCodeExits(string ref_no);
    }
}

using Dapper;
using MISA.QLTS.BACKEND.COMMON.Entity;
using MISA.QLTS.BACKEND.DATALAYER.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MISA.QLTS.BACKEND.DATALAYER.Entity
{
    /// <summary>
    /// Tầng Data Chứng từ
    /// </summary>
    /// CreatedBy: NMDAT(14/03/2021)
    public class RefDecrementData: BaseData<RefDecrement>, IRefDecrementData
    {
        // Khởi tạo tham chiếu interface
        private readonly IDataConnection<RefDecrement> _dataConnection;

        #region Contructor
        public RefDecrementData(IDataConnection<RefDecrement> dataConnection) : base(dataConnection)
        {
            _dataConnection = dataConnection;
        }
        #endregion

        #region Method
        /// <summary>
        /// Kiểm tra mã tài sản đã tồn tại hay chưa
        /// </summary>
        /// <param name="customerCode">mã tài sản</param>
        /// <returns>true là tồn tại - false là chưa tồn tại</returns>
        public RefDecrement CheckRefDecrementCodeExits(string ref_no)
        {
            // sql truy vấn mã tài sản
            //var sql = $"SELECT * FROM ref_decrement rd WHERE rd.ref_no = '{ref_no}'";
            var procName = "Proc_SelectRefDecrementByCode";
            var parameters = new DynamicParameters();
            parameters.Add("$RefNo", ref_no);
            // dapper thực hiện truy vấn nếu null là không tồn tại - != null là tồn tại
            var customerCodeExits = _dataConnection.Query(procName, parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
            //if (customerCodeExits != null)
            //    return true;
            //else
            //    return false;
            return customerCodeExits;
        }
        #endregion
    }
}

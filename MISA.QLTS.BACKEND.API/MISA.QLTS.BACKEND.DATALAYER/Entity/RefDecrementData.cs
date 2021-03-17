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
        /// Xóa nhiều bản ghi cùng lúc
        /// </summary>
        /// <param name="param">Mảng các id</param>
        /// <returns></returns>
        public int Deletes(string param)
        {
            // lấy tên bảng
            var className = typeof(RefDecrement).Name;
            var paramId = param.Split(",");
            var id = string.Empty;
            foreach (var item in paramId)
            {
                id += "," + "'" + $"{item}" + "'";
            }
            id = id.Remove(0, 1);
            // câu lệnh truy vấn
            var sqlCommand = $"DELETE FROM ref_decrement WHERE ref_decrement_id IN ({id})";
            var result = _dataConnection.Excute(sqlCommand);
            return result;
        }
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

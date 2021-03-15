using MISA.QLTS.BACKEND.COMMON.Entity;
using MISA.QLTS.BACKEND.DATALAYER.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MISA.QLTS.BACKEND.DATALAYER.Entity
{
    public class AssetData : BaseData<FixedAsset>,IAssetData
    {
        // Khởi tạo tham chiếu interface
        private readonly IDataConnection<FixedAsset> _dataConnection;

        #region Contructor
        public AssetData(IDataConnection<FixedAsset> dataConnection) : base(dataConnection)
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
        public FixedAsset CheckRefDecrementCodeExits(string fixed_asset_code)
        {
            // sql truy vấn mã tài sản
            var sql = $"SELECT * FROM fixed_asset WHERE fixed_asset_code = '{fixed_asset_code}'";

            // dapper thực hiện truy vấn nếu null là không tồn tại - != null là tồn tại
            var fixedAssetCodeExits = _dataConnection.Query(sql).FirstOrDefault();
            //if (customerCodeExits != null)
            //    return true;
            //else
            //    return false;
            return fixedAssetCodeExits;
        }
        #endregion
    }
}

using MISA.QLTS.BACKEND.COMMON.Entity;
using MISA.QLTS.BACKEND.DATALAYER.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MISA.QLTS.BACKEND.DATALAYER.Entity
{
    public class AssetData : BaseData<Asset>,IAssetData
    {
        // Khởi tạo tham chiếu interface
        private readonly IDataConnection<Asset> _dataConnection;

        #region Contructor
        public AssetData(IDataConnection<Asset> dataConnection) : base(dataConnection)
        {
            _dataConnection = dataConnection;
        }
        #endregion

        #region Method
        
        #endregion
    }
}

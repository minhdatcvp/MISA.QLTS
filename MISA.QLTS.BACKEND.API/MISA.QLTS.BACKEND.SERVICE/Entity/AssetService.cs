using MISA.QLTS.BACKEND.COMMON.Entity;
using MISA.QLTS.BACKEND.DATALAYER.Interface;
using MISA.QLTS.BACKEND.SERVICE.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.QLTS.BACKEND.SERVICE.Entity
{
    public class AssetService : BaseService<FixedAsset>, IAssetService
    {
        //Khởi tạo tham chiếu tới DbConnectionAsset
        private readonly IAssetData _dbConnectionAsset;
        #region Contructor
        public AssetService(IBaseData<FixedAsset> baseData, IAssetData dbConnectionAsset) : base(baseData)
        {
            _dbConnectionAsset = dbConnectionAsset;
        }
        #endregion


    }
}

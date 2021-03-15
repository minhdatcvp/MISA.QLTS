using MISA.QLTS.BACKEND.COMMON.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.QLTS.BACKEND.DATALAYER.Interface
{
    /// <summary>
    /// Interface tham chiếu tới AssetData
    /// </summary>
    /// CreatedBy: NMDAT(14/03/2021)
    public interface IAssetData : IBaseData<FixedAsset>
    {
        /// <summary>
        /// Check trùng mã
        /// </summary>
        /// <param name="fixed_asset_code"></param>
        /// <returns></returns>
        FixedAsset CheckRefDecrementCodeExits(string fixed_asset_code);
    }
}

using MISA.QLTS.BACKEND.COMMON.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.QLTS.BACKEND.DATALAYER.Interface
{
    public interface IAssetData : IBaseData<FixedAsset>
    {
        FixedAsset CheckRefDecrementCodeExits(string fixed_asset_code);
    }
}

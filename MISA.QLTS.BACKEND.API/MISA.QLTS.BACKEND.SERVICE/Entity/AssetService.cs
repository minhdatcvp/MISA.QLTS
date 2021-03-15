using MISA.Common.Model;
using MISA.QLTS.BACKEND.COMMON.Entity;
using MISA.QLTS.BACKEND.DATALAYER.Interface;
using MISA.QLTS.BACKEND.SERVICE.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.QLTS.BACKEND.SERVICE.Entity
{
    /// <summary>
    /// Service xử lý nghiệp vụ Tài sản
    /// </summary>
    /// CreatedBy: NMDAT(14/03/2021)
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
        #region Method
        /// <summary>
        /// Override Validate insert từ BaseService
        /// </summary>
        /// <param name="entity">fixedAsset</param>
        /// <param name="errorMsg">messenger trả về</param>
        /// <returns>true : dữ liệu hợp lệ - false : dữ liệu không hợp lệ</returns>
        protected override bool Validate(FixedAsset entity, ErrorMsg errorMsg)
        {
            // Khởi tạo biến isValid lưu trạng thái validate
            var isValid = true;

            //Validate dữ liệu(xử lý về nghiệp vụ):
            //1. Validate bắt buộc nhập:
            if (entity.fixed_asset_code == null || entity.fixed_asset_code == string.Empty)
            {
                errorMsg.UserMsg.Add(MISA.QLTS.BACKEND.COMMON.Properties.Resources.ErrorService_EmptyRefDecrementCode);
                isValid = false;
            }
            if (entity.fixed_asset_name == null)
            {
                errorMsg.UserMsg.Add(MISA.QLTS.BACKEND.COMMON.Properties.Resources.ErrorService_EmptyPostedDate);
                isValid = false;
            }

            //2. Validate dữ liệu không được phép (trùng):
            // kiểm tra trong database đã tồn tại đã mã kh hay chưa
            var isExits = _dbConnectionAsset.CheckRefDecrementCodeExits(entity.fixed_asset_code);
            if (isExits != null)
            {
                errorMsg.UserMsg.Add(MISA.QLTS.BACKEND.COMMON.Properties.Resources.ErrorService_DuplicateRefDecrementCode);
                isValid = false;
            }
            return isValid;
        }

        /// <summary>
        /// Override Validate update từ BaseService
        /// </summary>
        /// <param name="entity">fixedAsset</param>
        /// <param name="errorMsg">messenger trả về</param>
        /// <returns>true : dữ liệu hợp lệ - false : dữ liệu không hợp lệ</returns>
        protected override bool ValidateUpdate(FixedAsset entity, ErrorMsg errorMsg)
        {
            // Khởi tạo biến isValid lưu trạng thái validate
            var isValid = true;

            //Validate dữ liệu(xử lý về nghiệp vụ):
            //1. Validate bắt buộc nhập:
            //1. Validate bắt buộc nhập:
            if (entity.fixed_asset_code == null || entity.fixed_asset_code == string.Empty)
            {
                errorMsg.UserMsg.Add(MISA.QLTS.BACKEND.COMMON.Properties.Resources.ErrorService_EmptyRefDecrementCode);
                isValid = false;
            }
            if (entity.fixed_asset_name == null)
            {
                errorMsg.UserMsg.Add(MISA.QLTS.BACKEND.COMMON.Properties.Resources.ErrorService_EmptyPostedDate);
                isValid = false;
            }
            //2. Validate dữ liệu không được phép (trùng): 
            // kiểm tra trong database đã tồn tại đã mã kh hay chưa
            var isExits = _dbConnectionAsset.CheckRefDecrementCodeExits(entity.fixed_asset_code);
            if (isExits != null)
            {
                if(isExits.fixed_asset_code == entity.fixed_asset_code && isExits.fixed_asset_id != entity.fixed_asset_id)
                {
                    errorMsg.UserMsg.Add(MISA.QLTS.BACKEND.COMMON.Properties.Resources.ErrorService_DuplicateRefDecrementCode);
                    isValid = false;
                }
                
            }
            return isValid;
        }
        #endregion
    }
}

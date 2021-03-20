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
    /// Service Xử lý nghiệp vụ Chứng từ
    /// </summary>
    /// CreatedBy: NMDAT(14/03/2021)
    public class RefDecrementService : BaseService<RefDecrement>, IRefDecrementService
    {
        //Khởi tạo tham chiếu tới DbConnectionRefDecrement
        private readonly IRefDecrementData _dbConnectionRefDecrement;
        #region Contructor
        public RefDecrementService(IBaseData<RefDecrement> baseData, IRefDecrementData dbConnectionRefDecrement) : base(baseData)
        {
            _dbConnectionRefDecrement = dbConnectionRefDecrement;
        }
        #endregion

        #region Method
        /// <summary>
        /// Override Validate insert từ BaseService
        /// </summary>
        /// <param name="entity">refDecrement</param>
        /// <param name="errorMsg">messenger trả về</param>
        /// <returns>true : dữ liệu hợp lệ - false : dữ liệu không hợp lệ</returns>
        protected override bool Validate(RefDecrement entity, ErrorMsg errorMsg)
        {
            // Khởi tạo biến isValid lưu trạng thái validate
            var isValid = true;

            //Validate dữ liệu(xử lý về nghiệp vụ):
            //1. Validate bắt buộc nhập:
            if (entity.RefNo == null || entity.RefNo == string.Empty)
            {
                errorMsg.UserMsg.Add(MISA.QLTS.BACKEND.COMMON.Properties.Resources.ErrorService_EmptyRefDecrementCode);
                isValid = false;
            }
            if (entity.PostedDate == null)
            {
                errorMsg.UserMsg.Add(MISA.QLTS.BACKEND.COMMON.Properties.Resources.ErrorService_EmptyPostedDate);
                isValid = false;
            }

            //2. Validate dữ liệu không được phép (trùng):
            // kiểm tra trong database đã tồn tại đã mã kh hay chưa
            var isExits = _dbConnectionRefDecrement.CheckRefDecrementCodeExits(entity.RefNo);
            if (isExits!= null)
            {
                errorMsg.UserMsg.Add(MISA.QLTS.BACKEND.COMMON.Properties.Resources.ErrorService_DuplicateRefDecrementCode);
                isValid = false;
            }
            return isValid;
        }

        /// <summary>
        /// Override Validate update từ BaseService
        /// </summary>
        /// <param name="entity">Refdecrement</param>
        /// <param name="errorMsg">messenger trả về</param>
        /// <returns>true : dữ liệu hợp lệ - false : dữ liệu không hợp lệ</returns>
        protected override bool ValidateUpdate(RefDecrement entity, ErrorMsg errorMsg)
        {
            // Khởi tạo biến isValid lưu trạng thái validate
            var isValid = true;

            //Validate dữ liệu(xử lý về nghiệp vụ):
            //1. Validate bắt buộc nhập:
            //1. Validate bắt buộc nhập:
            if (entity.RefNo == null || entity.RefNo == string.Empty)
            {
                errorMsg.UserMsg.Add(MISA.QLTS.BACKEND.COMMON.Properties.Resources.ErrorService_EmptyRefDecrementCode);
                isValid = false;
            }
            if (entity.PostedDate == null)
            {
                errorMsg.UserMsg.Add(MISA.QLTS.BACKEND.COMMON.Properties.Resources.ErrorService_EmptyPostedDate);
                isValid = false;
            }
            //2. Validate dữ liệu không được phép (trùng):
            // kiểm tra trong database đã tồn tại đã mã kh hay chưa
            var isExits = _dbConnectionRefDecrement.CheckRefDecrementCodeExits(entity.RefNo);
            if (isExits != null)
            {
                if (isExits.RefNo == entity.RefNo && isExits.RefDecrementId != entity.RefDecrementId)
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

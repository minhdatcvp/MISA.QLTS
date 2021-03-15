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
        ///// <summary>
        ///// Xử lý nghiệp vụ xóa
        ///// </summary>
        ///// <param name="entity">thực thể cần xóa</param>
        ///// <returns></returns>
        public virtual ServiceResult Deletes(string param)
        {
            // Khởi tạo reponsive database
            var serviceResult = new ServiceResult();

            // Gọi đến method queyry của dbContex để thực hiện truy vấn vào database
            serviceResult.Data = _dbConnectionRefDecrement.Deletes(param);
            if (serviceResult.Data != null)
            {
                serviceResult.MisaCode = MISACode.Success;
                serviceResult.Success = true;
            }
            else
            {
                ErrorMsg errorMsg = new ErrorMsg();
                errorMsg.DevMsg = MISA.QLTS.BACKEND.COMMON.Properties.Resources.Msg_Dev;
                errorMsg.MoreInfo = MISA.QLTS.BACKEND.COMMON.Properties.Resources.MoreInfo;
                errorMsg.UserMsg.Add(MISA.QLTS.BACKEND.COMMON.Properties.Resources.Error_UserMsg);
                serviceResult.Data = errorMsg;
                serviceResult.MisaCode = MISACode.NotValid;
                serviceResult.Success = false;
            }
            return serviceResult;
        }
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
            if (entity.ref_no == null || entity.ref_no == string.Empty)
            {
                errorMsg.UserMsg.Add(MISA.QLTS.BACKEND.COMMON.Properties.Resources.ErrorService_EmptyRefDecrementCode);
                isValid = false;
            }
            if (entity.posted_date == null)
            {
                errorMsg.UserMsg.Add(MISA.QLTS.BACKEND.COMMON.Properties.Resources.ErrorService_EmptyPostedDate);
                isValid = false;
            }

            //2. Validate dữ liệu không được phép (trùng):
            // kiểm tra trong database đã tồn tại đã mã kh hay chưa
            var isExits = _dbConnectionRefDecrement.CheckRefDecrementCodeExits(entity.ref_no);
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
            if (entity.ref_no == null || entity.ref_no == string.Empty)
            {
                errorMsg.UserMsg.Add(MISA.QLTS.BACKEND.COMMON.Properties.Resources.ErrorService_EmptyRefDecrementCode);
                isValid = false;
            }
            if (entity.posted_date == null)
            {
                errorMsg.UserMsg.Add(MISA.QLTS.BACKEND.COMMON.Properties.Resources.ErrorService_EmptyPostedDate);
                isValid = false;
            }
            //2. Validate dữ liệu không được phép (trùng):
            // kiểm tra trong database đã tồn tại đã mã kh hay chưa
            var isExits = _dbConnectionRefDecrement.CheckRefDecrementCodeExits(entity.ref_no);
            if (isExits != null)
            {
                if (isExits.ref_no == entity.ref_no && isExits.ref_decrement_id != entity.ref_decrement_id)
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

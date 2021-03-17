using MISA.Common.Model;
using MISA.QLTS.BACKEND.DATALAYER.Interface;
using MISA.QLTS.BACKEND.SERVICE.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.QLTS.BACKEND.SERVICE
{
    public class BaseService<T> : IBaseService<T>
    {
        private readonly IBaseData<T> _baseData;
        #region Contructor
        public BaseService(IBaseData<T> baseData)
        {
            _baseData = baseData;
        }
        #endregion

        #region Method
        /// <summary>
        /// Lấy danh sách dư liệu
        /// </summary>
        /// <returns>Danh sách dữ liệu</returns>
        public virtual ServiceResult Get()
        {
            // Khởi tạo reponsive database
            var serviceResult = new ServiceResult();

            // Gọi đến method queyry của dbContex để thực hiện truy vấn vào database
            serviceResult.Data = _baseData.GetAll();
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
        /// Lấy dữ liệu theo Id
        /// </summary>
        /// <returns>Dữ liệu theo Id</returns>
        public virtual ServiceResult GetById(Guid id)
        {
            // Khởi tạo reponsive database
            var serviceResult = new ServiceResult();

            // Gọi đến method queyry của dbContex để thực hiện truy vấn vào database
            serviceResult.Data = _baseData.GetById(id);
            if (serviceResult.Data != null)
            {
                serviceResult.MisaCode = MISACode.Success;
                serviceResult.Success = true;
            }
            // Trường hợp không lấy được dữ liệu thông báo lỗi cho các bên
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
        /// Xử lý nghiệp vụ thêm mới tài sản
        /// </summary>
        /// <param name="">đối tượng thêm mới</param>
        /// <returns>ServiceResult</returns>
        public virtual ServiceResult Insert(T entity)
        {
            // Khởi tạo đối tượng trả về
            var serviceResult = new ServiceResult();
            // Khởi tạo thông báo lỗi 
            var errorMsg = new ErrorMsg();

            // Thuực hiện validate dữ liệu
            var isValid = Validate(entity, errorMsg);

            //Validate dữ liệu oke thì thực hiện hàm thêm
            if (isValid)
            {
                var res = _baseData.Insert(entity);
                if (res > 0)
                {
                    serviceResult.Success = true;
                    serviceResult.Data = res;
                    serviceResult.MisaCode = MISACode.Success;
                    return serviceResult;
                }
                else
                {
                    serviceResult.Success = true;
                    serviceResult.Data = res;
                    serviceResult.MisaCode = MISACode.IsValid;
                    return serviceResult;
                }
            }
            else
            {
                serviceResult.Success = false;
                serviceResult.Data = errorMsg;
                serviceResult.MisaCode = MISACode.NotValid;
            }
            return serviceResult;

        }

        /// <summary>
        /// Thực hiện validate dữ liệu
        /// </summary>
        /// <param name="entity">Đối tượng validate</param>
        /// <param name="errorMsg">Messsnger gửi về</param>
        /// <returns>true : Dữ liệu hợp lệ ; false : Dữ liệu không hợp lệ</returns>
        protected virtual bool Validate(T entity, ErrorMsg errorMsg)
        {
            return true;
        }

        /// <summary>
        /// Xử lý nghiệp vụ Sửa thông tin tài sản
        /// </summary>
        /// <param name="">đối tượng Sửa</param>
        /// <returns>ServiceResult</returns>
        public virtual ServiceResult Update(T entity)
        {
            // Khởi tạo đối tượng trả về
            var serviceResult = new ServiceResult();
            // Khởi tạo thông báo lỗi 
            var errorMsg = new ErrorMsg();
            // Thuực hiện validate dữ liệu
            var isValid = ValidateUpdate(entity, errorMsg);
            // Validate dữ liệu oke thì thực hiện hàm thêm
            if (isValid)
            {
                var res = _baseData.Update(entity);
                if (res > 0)
                {
                    serviceResult.Success = true;
                    serviceResult.Data = res;
                    serviceResult.MisaCode = MISACode.Success;
                    return serviceResult;
                }
                else
                {
                    serviceResult.Success = true;
                    serviceResult.Data = res;
                    serviceResult.MisaCode = MISACode.IsValid;
                    return serviceResult;
                }
            }
            else
            {
                serviceResult.Success = false;
                serviceResult.Data = errorMsg;
                serviceResult.MisaCode = MISACode.NotValid;
            }
            return serviceResult;
    }

        /// <summary>
        /// Thực hiện validate dữ liệu update
        /// </summary>
        /// <param name="entity">Đối tượng validate</param>
        /// <param name="errorMsg">Messsnger gửi về</param>
        /// <returns>true : Dữ liệu hợp lệ ; false : Dữ liệu không hợp lệ</returns>
        protected virtual bool ValidateUpdate(T entity, ErrorMsg errorMsg)
        {
            return true;
        }

        /// <summary>
        /// Xử lý nghiệp vụ xóa
        /// </summary>
        /// <param name="entity">thực thể cần xóa</param>
        /// <returns></returns>
        public virtual ServiceResult Delete(Guid id)
        {
            // Khởi tạo reponsive database
            var serviceResult = new ServiceResult();

            // Gọi đến method queyry của dbContex để thực hiện truy vấn vào database
            serviceResult.Data = _baseData.Delete(id);
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
        #endregion
    }
}

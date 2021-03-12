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
            //else
            //{
            //    ErrorMsg errorMsg = new ErrorMsg();
            //    errorMsg.DevMsg = MISA.QLTS.BACKEND.COMMON.Properties.Resources.Msg_Dev;
            //    errorMsg.MoreInfo = MISA.QLTS.BACKEND.COMMON.Properties.Resources.MoreInfo;
            //    errorMsg.UserMsg.Add(MISA.QLTS.BACKEND.COMMON.Properties.Resources.Error_UserMsg);
            //    serviceResult.Data = errorMsg;
            //    serviceResult.MisaCode = MISACode.NotValid;
            //    serviceResult.Success = false;
            //}
            return serviceResult;

        }
        #endregion
    }
}

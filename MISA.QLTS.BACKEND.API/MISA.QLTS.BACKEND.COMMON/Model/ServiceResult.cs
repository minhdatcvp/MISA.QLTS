﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Common.Model
{
    /// <summary>
    /// class kết quả validate
    /// </summary>
    /// CreatedBy : NMDAT (19/02/2021)
    public class ServiceResult
    {
        #region Contructor
        /// <summary>
        ///  Khởi tạo ban đầu là true
        /// </summary>
        public ServiceResult()
        {
            Success = true;
        }
        #endregion
        /// <summary>
        /// Trạng thái của service (true: thành công - false : thất bại)
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// Dữ liệu trả về
        /// </summary>
        public object Data { get; set; }
        /// <summary>
        /// Code tự định nghĩa
        /// </summary>
        public MISACode MisaCode  { get; set; }
    }
}

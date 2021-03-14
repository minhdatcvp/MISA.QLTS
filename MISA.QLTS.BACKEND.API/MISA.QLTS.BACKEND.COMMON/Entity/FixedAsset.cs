using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.QLTS.BACKEND.COMMON.Entity
{
    /// <summary>
    /// Thông tin tài sản
    /// </summary>
    /// CreatedBy: NMDAT (12/03/2021)
    public class FixedAsset
    {
        #region Contructor
        /// <summary>
        /// ID tài sản
        /// </summary>
        public Guid fixed_asset_id { get; set; }
        /// <summary>
        /// Mã tài sản
        /// </summary>
        public String fixed_asset_code { get; set; }
        /// <summary>
        /// Tên tài sản
        /// </summary>
        public String fixed_asset_name { get; set; }
        /// <summary>
        /// ID của đơn vị
        /// </summary>
        public Guid? organization_id { get; set; }
        /// <summary>
        /// Mã đơn vị
        /// </summary>
        public String organization_code { get; set; }
        /// <summary>
        /// Tên của đơn vị
        /// </summary>
        public String organization_name { get; set; }
        /// <summary>
        /// ID phòng ban
        /// </summary>
        public Guid? department_id { get; set; }
        /// <summary>
        /// Mã phòng ban
        /// </summary>
        public String department_code { get; set; }
        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public String department_name { get; set; }
        /// <summary>
        /// ID loại tài sản
        /// </summary>
        public Guid? fixed_asset_category_id { get; set; }
        /// <summary>
        /// Mã loại tài sản
        /// </summary>
        public String fixed_asset_category_code { get; set; }
        /// <summary>
        /// Tên loại tài sản
        /// </summary>
        public String fixed_asset_category_name { get; set; }
        /// <summary>
        /// Ngày ghi tăng
        /// </summary>
        public DateTime? increment_date { get; set; }
        /// <summary>
        /// Nguyên giá
        /// </summary>
        public Double? cost { get; set; }
        /// <summary>
        /// Số lượng
        /// </summary>
        public int? quantity { get; set; }
        /// <summary>
        /// Tỷ lệ hao mòn
        /// </summary>
        public float? depreciation_rate { get; set; }
        /// <summary>
        /// Gía trị hao mòn năm
        /// </summary>
        public Double? depreciation_year_price { get; set; }
        /// <summary>
        /// Năm bắt đầu theo dõi tài sản trên phần mềm
        /// </summary>
        public int? tracked_year { get; set; }
        /// <summary>
        /// Số năm sử dụng
        /// </summary>
        public int? life_time { get; set; }
        /// <summary>
        /// Năm sử dụng
        /// </summary>
        public int? production_year { get; set; }
        /// <summary>
        /// Sử dụng
        /// </summary>
        public int? active { get; set; }
        #endregion

        #region Orther
        /// <summary>
        /// Người khởi tạo
        /// </summary>
        public String created_by { get; set; }
        /// <summary>
        /// Ngày khởi tạo
        /// </summary>
        public DateTime created_date { get; set; }
        /// <summary>
        /// Người chỉnh sửa
        /// </summary>
        public String modified_by { get; set; }
        /// <summary>
        /// Ngày chỉnh sửa
        /// </summary>
        public DateTime modified_date { get; set; }
        #endregion
    }
}

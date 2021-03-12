using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.QLTS.BACKEND.COMMON.Entity
{
    /// <summary>
    /// Thông tin chứng từ ghi giảm
    /// </summary>
    /// CreatedBy: NMDAT(12/03/2021)
    public class Decrement
    {
        #region Contructor
        /// <summary>
        /// ID chứng từ
        /// </summary>
        public Guid ref_decrement_id { get; set; }
        /// <summary>
        /// ID của đơn vị
        /// </summary>
        public String organization_id { get; set; }
        /// <summary>
        /// Số chứng từ
        /// </summary>
        public String ref_no { get; set; }
        /// <summary>
        /// Loại chứng từ
        /// </summary>
        public int ref_type { get; set; }
        /// <summary>
        /// Ngày chứng từ
        /// </summary>
        public DateTime ref_date { get; set; }
        /// <summary>
        /// 'Chi tiết danh sách tài sản của chứng từ
        /// </summary>
        public String ref_detail { get; set; }
        /// <summary>
        /// Diễn giải
        /// </summary>
        public String journal_memo { get; set; }
        /// <summary>
        /// Ngày ghi giảm
        /// </summary>
        public DateTime posted_date { get; set; }
        /// <summary>
        /// Năm làm việc ~ năm chứng từ
        /// </summary>
        public int tracked_year { get; set; }
        /// <summary>
        /// Tổng giá trị còn lại
        /// </summary>
        public Double cost_total { get; set; }
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

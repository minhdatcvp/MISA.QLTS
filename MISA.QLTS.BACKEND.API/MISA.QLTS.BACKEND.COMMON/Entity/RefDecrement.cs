using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.QLTS.BACKEND.COMMON.Entity
{
    /// <summary>
    /// Thông tin chứng từ ghi giảm
    /// </summary>
    /// CreatedBy: NMDAT(12/03/2021)
    public class RefDecrement
    {
        #region Contructor
        /// <summary>
        /// ID chứng từ
        /// </summary>
        public Guid RefDecrementId { get; set; }
        /// <summary>
        /// ID của đơn vị
        /// </summary>
        public Guid? OrganizationId { get; set; }
        /// <summary>
        /// Số chứng từ
        /// </summary>
        public String RefNo { get; set; }
        /// <summary>
        /// Loại chứng từ
        /// </summary>
        public int RefType { get; set; }
        /// <summary>
        /// Ngày chứng từ
        /// </summary>
        public DateTime RefDate { get; set; }
        /// <summary>
        /// Chi tiết danh sách tài sản của chứng từ
        /// </summary>
        public String RefDetail { get; set; }
        /// <summary>
        /// Diễn giải
        /// </summary>
        public String JournalMemo { get; set; }
        /// <summary>
        /// Ngày ghi giảm
        /// </summary>
        public DateTime PostedDate { get; set; }
        /// <summary>
        /// Năm làm việc ~ năm chứng từ
        /// </summary>
        public int TrackedYear { get; set; }
        /// <summary>
        /// Tổng giá trị còn lại
        /// </summary>
        public Double CostTotal { get; set; }
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

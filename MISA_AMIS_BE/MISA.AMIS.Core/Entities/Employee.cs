using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.AMIS.Core.Enum;

namespace MISA.AMIS.Core.Entities
{
    public class Employee: BaseEntity
    {
        #region Property

        /// <summary>
        /// Id nhân viên
        /// </summary>
        public Guid EmployeeID { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        [Required]
        [Duplicated]
        [PropNameDisplay("Mã nhân viên")]
        public string? EmployeeCode { get; set; }

        /// <summary>
        /// Tên nhân viên
        /// </summary>
        [Required]
        [PropNameDisplay("Tên nhân viên")]
        public string? FullName { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        public MISAEnum.Gender? Gender { get; set; }

        /// <summary>
        /// Id đơn vị
        /// </summary>
        [Required]
        [PropNameDisplay("Đơn vị")]
        public int? OrganizationID { get; set; }

        /// <summary>
        /// Tên đơn vị
        /// </summary>
        public string? OrganizationName { get; set; }

        /// <summary>
        /// Mã đơn vị
        /// </summary>
        public string? OrganizationCode { get; set; }

        /// <summary>
        /// Id chức danh
        /// </summary>
        public int? PositionID { get; set; }

        /// <summary>
        /// Tên chức danh
        /// </summary>
        public string? PositionName { get; set; }

        /// <summary>
        /// Số chứng minh nhân dân
        /// </summary>
        
        public string? IdentityNumber { get; set; }

        /// <summary>
        /// Ngày cấp
        /// </summary>
        public DateTime? DateOfIssuanceCIC { get; set; }

        /// <summary>
        /// Nơi cấp
        /// </summary>
        public string? PlaceOfIssuanceCIC { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Điện thoại di động
        /// </summary>
        public string? Mobile { get; set; }

        /// <summary>
        /// Điện thoại cố định
        /// </summary>
        public string? LandPhone { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [Email]
        [PropNameDisplay("Email")]
        public string? Email { get; set; }

        /// <summary>
        /// Tài khoản ngân hàng
        /// </summary>
        public string? BankAccount { get; set; }

        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        public string? BankName { get; set; }

        /// <summary>
        /// Chi nhánh
        /// </summary>
        public string? BankBranch { get; set; }

        /// <summary>
        /// Là khách hàng
        /// </summary>
        public int? IsCustomer { get; set; }

        /// <summary>
        /// Là nhà cung cấp
        /// </summary>
        public int? IsGroup { get; set; }
        
        #endregion
    }
}

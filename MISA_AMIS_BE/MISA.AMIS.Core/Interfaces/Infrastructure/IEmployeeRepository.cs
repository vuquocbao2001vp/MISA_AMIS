using MISA.AMIS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Interfaces.Infrastructure
{
    public interface IEmployeeRepository: IBaseRepository<Employee>
    {
        #region Interface

        /// <summary>
        /// Phân trang dữ liệu nhân viên
        /// </summary>
        /// <param name="pageSize">Số lượng bản ghi 1 trang</param>
        /// <param name="pageNumber">Số thứ tự trang hiển thị</param>
        /// <param name="searchKey">Từ khóa tìm kiếm theo tên, mã, số điện thoại</param>
        /// <returns>Object chứa đủ các trường thông tin của người dùng và thông tin phân trang</returns>
        /// Author: VQBao - 9/10/2022
        Object GetEmployeePaging(int pageSize, int pageNumber, string? searchKey);

        /// <summary>
        /// Thực hiện lấy mã nhân viên mới = mã nhân viên cũ lớn nhất cộng 1
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        /// Author: VQBao - 10/10/2022
        string GetNewEmployeeCode();

        #endregion
    }
}

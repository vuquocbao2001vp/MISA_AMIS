using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Interfaces.Services;

namespace MISA.AMIS.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : MISABaseController<Employee>
    {
        #region Constructor

        IEmployeeService employeeService;
        public EmployeesController(IEmployeeService _employeeService) : base(_employeeService)
        {
            employeeService = _employeeService;
        }

        #endregion

        #region Controller

        /// <summary>
        /// Phân trang dữ liệu nhân viên
        /// </summary>
        /// <param name="pageSize">Số lượng bản ghi 1 trang</param>
        /// <param name="pageNumber">Số thứ tự trang hiển thị</param>
        /// <param name="searchKey">Từ khóa tìm kiếm theo tên, mã, số điện thoại</param>
        /// <returns>Object chứa đủ các trường thông tin của người dùng và thông tin phân trang</returns>
        /// Author: VQBao - 9/10/2022
        [HttpGet("filter")]
        public IActionResult GetEmployeePaging(int pageSize, int pageNumber, string? searchKey)
        {
            try
            {
                var data = employeeService.GetEmployeePaging(pageSize, pageNumber, searchKey);
                return Ok(data);
            }
            catch (Exception e)
            {
                return HandleException(e);   
            }
        }

        /// <summary>
        /// Thực hiện lấy mã nhân viên mới = mã nhân viên cũ lớn nhất cộng 1
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        /// Author: VQBao - 10/10/2022
        [HttpGet("new-employee-code")]
        public IActionResult GetNewEmployeeCode()
        {
            try
            {
                var data = employeeService.GetNewEmployeeCode();
                return Ok(data);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        /// <summary>
        /// Xuất khẩu toàn bộ nhân viên
        /// </summary>
        /// <returns>Bảng excel</returns>
        /// Author: VQBao - 12/10/2022
        [HttpGet("employee-excel")]
        public async Task<IActionResult> GetExcelFile()
        {

            try
            {
                var stream = employeeService.ExportToExcel();
                string excelName = "Danh_sach_nhan_vien.xlsx";
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        #endregion
    }
}

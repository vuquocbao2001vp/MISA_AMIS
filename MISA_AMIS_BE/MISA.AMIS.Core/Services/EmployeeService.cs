using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Enum;
using MISA.AMIS.Core.Interfaces.Infrastructure;
using MISA.AMIS.Core.Interfaces.Services;
using MISA.AMIS.Core.Resource;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Services
{
    public class EmployeeService: BaseService<Employee>, IEmployeeService
    {
        #region Constructor

        IEmployeeRepository employeeRepository;
        public EmployeeService(IEmployeeRepository _employeeRepository) : base(_employeeRepository)
        {
            employeeRepository = _employeeRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Phân trang dữ liệu nhân viên
        /// </summary>
        /// <param name="pageSize">Số lượng bản ghi 1 trang</param>
        /// <param name="pageNumber">Số thứ tự trang hiển thị</param>
        /// <param name="searchKey">Từ khóa tìm kiếm theo tên, mã, số điện thoại</param>
        /// <returns>Object chứa đủ các trường thông tin của người dùng và thông tin phân trang</returns>
        /// Author: VQBao - 9/10/2022
        public Object GetEmployeePaging(int pageSize, int pageNumber, string? searchKey)
        {
            return employeeRepository.GetEmployeePaging(pageSize, pageNumber, searchKey);
        }

        /// <summary>
        /// Thực hiện lấy mã nhân viên mới = mã nhân viên cũ lớn nhất cộng 1
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        /// Author: VQBao - 10/10/2022
        public string GetNewEmployeeCode()
        {
            return employeeRepository.GetNewEmployeeCode();
        }

        /// <summary>
        /// Xuất khẩu toàn bộ nhân viên
        /// </summary>
        /// <returns>Bảng excel</returns>
        /// Author: VQBao - 12/10/2022
        public MemoryStream ExportToExcel()
        {
            try
            {
                var listEmployees = employeeRepository.GetAll();
                var employees = listEmployees.ToList();
                var stream = new MemoryStream();
                using (var package = new ExcelPackage(stream))
                {
                    // tạo worksheet để hiển thị bảng danh sách giáo viên
                    var workSheet = package.Workbook.Worksheets.Add("Danh_sach_nhan_vien");

                    List<string> headers = new List<string>()
                    {
                        "A3", "B3", "C3", "D3", "E3", "F3", "G3", "H3", "I3", "J3", "K3", "L3", "M3"
                    };

                    List<string> employeeProps = new List<string>()
                    {
                        //"STT", "Mã nhân viên", "Tên nhân viên", "Giới tính", "Ngày sinh", "Số CMND", "Chức danh", "Tên đơn vị", "Số tài khoản", "Tên ngân hàng", "Chi nhánh TK ngân hàng", "Là khách hàng", "Là nhà cung cấp"
                        ResourceVN.OrderNumber, ResourceVN.PropEmployeeCode, ResourceVN.PropFullName, ResourceVN.PropGender, ResourceVN.PropDateOfBirth, ResourceVN.PropIdentityCard, ResourceVN.PropPosition, ResourceVN.PropOrganization, ResourceVN.PropBankAccount, ResourceVN.PropBankName, ResourceVN.PropBankBranch, ResourceVN.PropIsCustomer, ResourceVN.PropIsGroup
                    };

                    for (int i = 0; i < employeeProps.Count; i++)
                    {
                        workSheet.Cells[headers[i]].Value = employeeProps[i];
                        workSheet.Cells[headers[i]].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        workSheet.Cells[headers[i]].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                        workSheet.Cells[headers[i]].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(211, 211, 211));
                    }

                    for (int i = 0; i < employees.Count; i++)
                    {
                        // Số thứ tự
                        workSheet.Cells[i + 4, 1].Value = i + 1;
                        // Mã nhân viên
                        workSheet.Cells[i + 4, 2].Value = employees[i].EmployeeCode;
                        // Họ và tên
                        workSheet.Cells[i + 4, 3].Value = employees[i].FullName;
                        // Giới tính
                        if (employees[i].Gender == MISAEnum.Gender.Male)
                        {
                            workSheet.Cells[i + 4, 4].Value = ResourceVN.Male;
                        }
                        else if (employees[i].Gender == MISAEnum.Gender.Femail)
                        {
                            workSheet.Cells[i + 4, 4].Value = ResourceVN.Female;
                        }
                        else
                        {
                            workSheet.Cells[i + 4, 4].Value = ResourceVN.OtherGender;
                        }
                        // Ngày sinh
                        if (!string.IsNullOrEmpty(employees[i].DateOfBirth.ToString()))
                        {
                            var date = employees[i].DateOfBirth.GetValueOrDefault();
                            workSheet.Cells[i + 4, 5].Value = ConvertDateToString(date);
                        }
                        // Số chứng minh nhân dân
                        workSheet.Cells[i + 4, 6].Value = employees[i].IdentityNumber;
                        // Chức danh
                        workSheet.Cells[i + 4, 7].Value = employees[i].PositionName;
                        // Đơn vị
                        workSheet.Cells[i + 4, 8].Value = employees[i].OrganizationName;
                        // Số tài khoản
                        workSheet.Cells[i + 4, 9].Value = employees[i].BankAccount;
                        // Tên ngân hàng
                        workSheet.Cells[i + 4, 10].Value = employees[i].BankName;
                        // Chi nhành ngân hàng
                        workSheet.Cells[i + 4, 11].Value = employees[i].BankBranch;
                        // Là khách hàng
                        if (employees[i].IsCustomer == 1)
                        {
                            workSheet.Cells[i + 4, 12].Value = "✓";
                        }
                        // Là nhà cung cấp
                        if (employees[i].IsGroup == 1)
                        {
                            workSheet.Cells[i + 4, 13].Value = "✓";
                        }
                    }

                    workSheet = StyleWorkSheet(workSheet);

                    package.Save();
                }
                
                stream.Position = 0;
                return stream;
            }
            catch (Exception)
            {
                throw new MISAException(msg: ResourceVN.ExceptionExport);
            }
        }

        /// <summary>
        /// Style bảng
        /// </summary>
        /// <param name="workSheet">WorkSheet cần style</param>
        /// <returns>WorkSheet đã style</returns>
        /// Author: VQBao - 20/10/2022
        private ExcelWorksheet StyleWorkSheet(ExcelWorksheet workSheet)
        {
            // Tạo tiêu đề của bảng
            workSheet.Cells["A1:M1"].Merge = true;
            workSheet.Cells["A1"].Value = "DANH SÁCH NHÂN VIÊN";
            workSheet.Cells["A1:M1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            workSheet.Cells["A1:M1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            workSheet.Cells["A1:M1"].Style.Font.Size = 16;
            workSheet.Row(1).Height = 24;
            workSheet.Row(2).Height = 24;
            workSheet.Cells["A1"].Style.Font.Bold = true;
            workSheet.Cells["A2:M2"].Merge = true;

            // Căn chỉnh style của các header
            workSheet.Rows[3].Height = 24;
            workSheet.Rows[3].Style.Font.Bold = true;
            workSheet.Rows[3].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            workSheet.Rows[3].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

            // Tạo độ rộng hiển thị cho các cột của bảng
            workSheet.Columns.AutoFit();
            workSheet.Column(1).Width = 4;
            workSheet.Column(1).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            workSheet.Column(12).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            workSheet.Column(13).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

            return workSheet;
        }

        /// <summary>
        /// Định dạng hiển thị ngày tháng
        /// </summary>
        /// <param name="date">Ngày muốn định dạng</param>
        /// <returns>String hiển thị ngày</returns>
        /// Author: VQBao - 20/10/2022
        private string ConvertDateToString(DateTime date)
        {
            var day = date.Day.ToString();
            var month = date.Month.ToString();
            var year = date.Year.ToString();

            if (date.Day < 10)
            {
                day = "0" + date.Day.ToString();
            } 
            if(date.Month < 10)
            {
                month = "0" + date.Month.ToString();
            }

            return day + "/" + month + "/" + year;
        }

        #endregion
    }
}

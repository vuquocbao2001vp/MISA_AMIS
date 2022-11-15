using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Interfaces.Infrastructure;
using Dapper;
using MISA.AMIS.Core;
using MISA.AMIS.Core.Resource;

namespace MISA.AMIS.Infrastructure.Repository
{
    public class EmployeeRepository: BaseRepository<Employee>, IEmployeeRepository
    {
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
            try
            {
                var storeProc = $"Proc_GetEmployeePaging";

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@v_PageSize", pageSize);
                parameters.Add("@v_PageNumber", pageNumber);
                parameters.Add("@v_SearchKey", searchKey);
                parameters.Add("@v_TotalRecords", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
                parameters.Add("@v_TotalPages", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
                var data = connection.Query<Employee>(storeProc, param: parameters, commandType: System.Data.CommandType.StoredProcedure);
                int totalRecords = parameters.Get<int>("@v_TotalRecords");
                int totalPages = parameters.Get<int>("@v_TotalPages");

                var response = new
                {
                    TotalRecords = totalRecords,
                    TotalPages = totalPages,
                    PageSize = pageSize,
                    PageNumber = pageNumber,
                    Data = data,
                };
                return response;
            }
            catch (Exception)
            {
                throw new MISAException(msg: ResourceVN.ExceptionEmployeePaging);
            }
        }

        /// <summary>
        /// Thực hiện lấy mã nhân viên mới = mã nhân viên cũ lớn nhất cộng 1
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        /// Author: VQBao - 10/10/2022
        public string GetNewEmployeeCode()
        {
            try
            {
                var storeProc = "Proc_GetNewEmployeeCode";
                var data = connection.QuerySingle<string>(storeProc, commandType: System.Data.CommandType.StoredProcedure);
                string res = data.ToString();
                return res;
            }
            catch (Exception)
            {

                throw new MISAException(msg: ResourceVN.ExceptionNewCode);
            }
        }

        #endregion
    }
}

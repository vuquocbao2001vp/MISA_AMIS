using Dapper;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MISA.AMIS.Core;
using MISA.AMIS.Core.Resource;

namespace MISA.AMIS.Infrastructure.Repository
{
    public class BaseRepository<Entity>: IDisposable
    {
        #region Constructor

        protected readonly string connectionString = "";
        protected MySqlConnection connection;
        public BaseRepository()
        {
            // khởi tạo kết nối với dataabse
            connectionString =
                "Host=localhost; " +
                "Port=3306; " +
                "Database=misa.amis; " +
                "User=root; " +
                "Password=vuquocbao2001vp;";
            connection = new MySqlConnection(connectionString);
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        }
        // Đóng kết nối
        public void Dispose()
        {
            connection.Dispose();
            connection.Close();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Lấy tất cả dữ liệu trong bảng
        /// </summary>
        /// <returns>Danh sách toàn bộ bản ghi</returns>
        /// Author: VQBao - 9/10/2022
        public IEnumerable<Entity> GetAll()
        {
            try
            {
                var entity = typeof(Entity).Name;
                var storeProc = $"Proc_GetAll{entity}";
                var data = connection.Query<Entity>(storeProc, commandType: System.Data.CommandType.StoredProcedure);
                return data;
            }
            catch (Exception)
            {
                throw new MISAException(msg: ResourceVN.ExceptionGetData);
            }
        }

        /// <summary>
        /// Lấy thông tin bản ghi theo id
        /// </summary>
        /// <param name="id">Id của bản ghi</param>
        /// <returns>Bản ghi tương ứng</returns>
        /// Author: VQBao - 9/10/2022
        public Entity GetByID(string id)
        {
            try
            {
                var entity = typeof(Entity).Name;
                var storeProc = $"Proc_Get{entity}ByID";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("v_ID", id);
                var data = connection.QueryFirstOrDefault<Entity>(storeProc, parameters, commandType: System.Data.CommandType.StoredProcedure);
                return data;
            }
            catch (Exception)
            {
                throw new MISAException(msg: ResourceVN.ExceptionGetData);
            }
        }

        /// <summary>
        /// Xóa nhiều theo Id
        /// </summary>
        /// <param name="ids">Chuỗi id ngăn cách bởi dấu ','</param>
        /// <returns></returns>
        /// Author: VQBao - 9/10/2022
        public int DeleteByID(string ids)
        {
            try
            {
                var entity = typeof(Entity).Name;
                var storeProc = $"Proc_Delete{entity}ByID";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("v_IDs", ids);
                var data = connection.Execute(storeProc, parameters, commandType: System.Data.CommandType.StoredProcedure);
                return data;
            }
            catch (Exception)
            {
                throw new MISAException(msg: ResourceVN.ExceptionDelete);
            }
        }

        /// <summary>
        /// Thêm nhiều bản ghi vào trong bảng
        /// </summary>
        /// <param name="entities">Danh sách bản ghi</param>
        /// <returns>Số lượng bản ghi được thêm</returns>
        /// Author: VQBao - 9/10/2022
        public int Insert(List<Entity> entities)
        {
            try
            {
                var jsonEntities = JsonConvert.SerializeObject(entities);
                var entity = typeof(Entity).Name;
                var storeProc = $"Proc_Insert{entity}s";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("v_JsonTable", jsonEntities);
                var data = connection.Execute(storeProc, parameters, commandType: System.Data.CommandType.StoredProcedure);
                return data;
            }
            catch (Exception)
            {
                throw new MISAException(msg: ResourceVN.ExceptionInsert);
            }
        }

        /// <summary>
        /// Sửa nhiều bản ghi trong bảng
        /// </summary>
        /// <param name="entities">Danh sách bản ghi</param>
        /// <returns>Số lượng bản ghi được thêm</returns>
        /// Author: VQBao - 9/10/2022
        public int Update(List<Entity> entities)
        {
            try
            {
                var jsonEntities = JsonConvert.SerializeObject(entities);
                var entity = typeof(Entity).Name;
                var storeProc = $"Proc_Update{entity}s";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("v_JsonTable", jsonEntities);
                var data = connection.Execute(storeProc, parameters, commandType: System.Data.CommandType.StoredProcedure);
                return data;
            }
            catch (Exception)
            {
                throw new MISAException(msg: ResourceVN.ExceptionUpdate);
            }
        }

        /// <summary>
        /// Kiểm tra trùng dữ liệu trong database
        /// </summary>
        /// <param name="entity">Đối tượng kiểm tra</param>
        /// <returns>true: đã tồn tại, false: chưa tồn tại</returns>
        public bool CheckDuplicate(Entity entity)
        {
            try
            {
                var list = new List<Entity>();
                list.Add(entity);
                var jsonObject = JsonConvert.SerializeObject(list);
                var entityName = typeof(Entity).Name;
                var storeProc = $"Proc_CheckDuplicate{entityName}";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("v_JsonObject", jsonObject);
                var check = connection.QueryFirstOrDefault(storeProc, parameters, commandType: System.Data.CommandType.StoredProcedure);
                if(check == null) return false;
                return true;
            }
            catch (Exception)
            {
                throw new MISAException(msg: ResourceVN.ExceptionCheckDuplicate);
            }
        }
        #endregion
    }
}

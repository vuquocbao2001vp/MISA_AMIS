using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Interfaces.Services
{
    public interface IBaseService<Entity>
    {
        #region Interface
        /// <summary>
        /// Lấy tất cả dữ liệu trong bảng
        /// </summary>
        /// <returns>Danh sách toàn bộ bản ghi</returns>
        /// Author: VQBao - 9/10/2022
        IEnumerable<Entity> GetAll();

        /// <summary>
        /// Lấy thông tin bản ghi theo id
        /// </summary>
        /// <param name="id">Id của bản ghi</param>
        /// <returns>Bản ghi tương ứng</returns>
        /// Author: VQBao - 9/10/2022
        Entity GetByID(string id);

        /// <summary>
        /// Xóa nhiều theo Id
        /// </summary>
        /// <param name="ids">Chuỗi id ngăn cách bởi dấu ','</param>
        /// <returns></returns>
        /// Author: VQBao - 9/10/2022
        int DeleteByID(string ids);

        /// <summary>
        /// Thêm nhiều bản ghi vào trong bảng
        /// </summary>
        /// <param name="entities">Danh sách bản ghi</param>
        /// <returns>Số lượng bản ghi được thêm</returns>
        /// Author: VQBao - 9/10/2022
        int Insert(List<Entity> entities);

        /// <summary>
        /// Sửa nhiều bản ghi trong bảng
        /// </summary>
        /// <param name="entities">Danh sách bản ghi</param>
        /// <returns>Số lượng bản ghi được thêm</returns>
        /// Author: VQBao - 9/10/2022
        int Update(List<Entity> entities);

        #endregion
    }
}

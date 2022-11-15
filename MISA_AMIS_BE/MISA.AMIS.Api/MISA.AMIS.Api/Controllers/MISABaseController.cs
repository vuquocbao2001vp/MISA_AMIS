using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.Core;
using MISA.AMIS.Core.Interfaces.Services;
using MISA.AMIS.Core.Resource;


namespace MISA.AMIS.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MISABaseController<Entity> : ControllerBase
    {
        #region Constructor
        IBaseService<Entity> service;
        public MISABaseController(IBaseService<Entity> _service)
        {
            service = _service;
        }
        #endregion

        #region Controller

        /// <summary>
        /// Lấy tất cả dữ liệu trong bảng
        /// </summary>
        /// <returns>Danh sách toàn bộ bản ghi</returns>
        /// Author: VQBao - 9/10/2022
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var data = service.GetAll();
                return Ok(data);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        /// <summary>
        /// Lấy thông tin bản ghi theo id
        /// </summary>
        /// <param name="id">Id của bản ghi</param>
        /// <returns>Bản ghi tương ứng</returns>
        /// Author: VQBao - 9/10/2022
        [HttpGet("{id}")]
        public IActionResult GetByID(string id)
        {
            try
            {
                var data = service.GetByID(id);
                return Ok(data);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        /// <summary>
        /// Xóa nhiều theo Id
        /// </summary>
        /// <param name="ids">Chuỗi id ngăn cách bởi dấu ','</param>
        /// <returns></returns>
        /// Author: VQBao - 9/10/2022
        [HttpDelete]
        public IActionResult DeleteByID(string ids)
        {
            try
            {
                var data = service.DeleteByID(ids);
                return StatusCode(201, data);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        /// <summary>
        /// xử lý exception
        /// </summary>
        /// <returns>Mã lỗi, Thông tin lỗi</returns>
        /// Author: VQBao - 9/10/2022
        protected IActionResult HandleException(Exception e)
        {
            var res = new Object();
            if (!String.IsNullOrEmpty(e.Message))
            {
                res = new
                {
                    userMsg = ResourceVN.UserMessage,
                    errorMsg = e.Message
                };
            }
            else
            {
                res = new
                {
                    userMsg = ResourceVN.UserMessage,
                    errorMsg = e.Data["validateError"]
                };
            }

            if (e is MISAException)
            {
                return BadRequest(res);
            }
            return StatusCode(500, res);
        }

        /// <summary>
        /// Thêm nhiều bản ghi vào trong bảng
        /// </summary>
        /// <param name="entities">Danh sách bản ghi</param>
        /// <returns>Số lượng bản ghi được thêm</returns>
        /// Author: VQBao - 9/10/2022
        [HttpPost]
        public IActionResult Insert(List<Entity> entities)
        {
            try
            {
                var data = service.Insert(entities);
                return StatusCode(201, data);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        /// <summary>
        /// Sửa nhiều bản ghi vào trong bảng
        /// </summary>
        /// <param name="entities">Danh sách bản ghi</param>
        /// <returns>Số lượng bản ghi được thêm</returns>
        /// Author: VQBao - 9/10/2022
        [HttpPut]
        public IActionResult Update(List<Entity> entities)
        {
            try
            {
                var data = service.Update(entities);
                return StatusCode(201, data);
            }
            catch (Exception e)
            {
                return HandleException(e);
            }
        }

        #endregion
    }
}

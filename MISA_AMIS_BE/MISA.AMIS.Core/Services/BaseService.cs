using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Interfaces.Infrastructure;
using MISA.AMIS.Core.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Services
{
    public class BaseService<Entity>
    {
        #region Constructor
        protected List<string> listMsgErrors = new List<string>();
        IBaseRepository<Entity> repository;
        public BaseService(IBaseRepository<Entity> _repository)
        {
            repository = _repository;
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
            var data = repository.GetAll();
            return data;
        }

        /// <summary>
        /// Lấy thông tin bản ghi theo id
        /// </summary>
        /// <param name="id">Id của bản ghi</param>
        /// <returns>Bản ghi tương ứng</returns>
        /// Author: VQBao - 9/10/2022
        public Entity GetByID(string id)
        {
            return repository.GetByID(id);
        }

        /// <summary>
        /// Xóa nhiều theo Id
        /// </summary>
        /// <param name="ids">Chuỗi id ngăn cách bởi dấu ','</param>
        /// <returns></returns>
        /// Author: VQBao - 9/10/2022
        public int DeleteByID(string ids)
        {
            return repository.DeleteByID(ids);
        }

        /// <summary>
        /// Thêm nhiều bản ghi vào trong bảng
        /// </summary>
        /// <param name="entities">Danh sách bản ghi</param>
        /// <returns>Số lượng bản ghi được thêm</returns>
        /// Author: VQBao - 9/10/2022
        public int Insert(List<Entity> entities)
        {
            var isValid = true;
            foreach (var entity in entities)
            {
                var check = Validate(entity);
                if(check == false)
                {
                    isValid = false;
                }
            }
            if(isValid == true)
            {
                return repository.Insert(entities);
            } else
            {
                throw new MISAException(listMsgs: listMsgErrors);
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
            var isValid = true;
            foreach (var entity in entities)
            {
                var check = Validate(entity);
                if (check == false)
                {
                    isValid = false;
                }
            }
            if (isValid == true)
            {
                return repository.Update(entities);
            }
            else
            {
                throw new MISAException(listMsgs: listMsgErrors);
            }
        }

        /// <summary>
        /// Kiểm tra trùng dữ liệu trong database
        /// </summary>
        /// <param name="entity">Đối tượng kiểm tra</param>
        /// <returns>true: đã tồn tại, false: chưa tồn tại</returns>
        /// Author: VQBao - 10/10/2022
        private bool CheckDuplicate(Entity entity)
        {
            return repository.CheckDuplicate(entity);
        }

        /// <summary>
        /// Hàm kiểm tra email có hợp lệ hay không
        /// </summary>
        /// <param name="email">Tham số đầu vào muốn kiểm tra</param>
        /// <returns>true: là email, false: không là email</returns>
        /// Author: VQBao - 10/10/2020
        private bool IsEmail(string email)
        {
            Regex validateEmailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            if (validateEmailRegex.IsMatch(email)) { return true; }
            else return false;
        }

        /// <summary>
        /// Kiểm tra tính hợp lệ của bản ghi
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// Author: VQBao - 10/10/2022
        private bool Validate(Entity entity)
        {

            var isValid = true;
            var properties = entity.GetType().GetProperties();
            foreach (var property in properties)
            {
                var propName = property.Name;
                var propValue = property.GetValue(entity);

                var required = property.IsDefined(typeof(Required), false);
                var duplicated = property.IsDefined(typeof(Duplicated), false);
                var emailAttr = property.IsDefined(typeof(Email), false);

                // Check các trường dữ liệu null hoặc không có giá trị
                if (required == true && (propValue == null || propValue.ToString() == String.Empty))
                {
                    var listPropNameDisplay = property.GetCustomAttributes(typeof(PropNameDisplay), false).FirstOrDefault();
                    propName = (listPropNameDisplay as PropNameDisplay).PropName;
                    var error = propName + ResourceVN.ErrorEmpty;
                    if(!listMsgErrors.Contains(error))
                    {
                        listMsgErrors.Add(error);
                    }
                    isValid = false;
                }

                // Check trùng mã
                if (duplicated == true && !(propValue == null || propValue.ToString() == String.Empty) && CheckDuplicate(entity) == true)
                {
                    var listPropNameDisplay = property.GetCustomAttributes(typeof(PropNameDisplay), false).FirstOrDefault();
                    propName = (listPropNameDisplay as PropNameDisplay).PropName;
                    var error = propName + ResourceVN.ErrorDuplicated;
                    if (!listMsgErrors.Contains(error))
                    {
                        listMsgErrors.Add(error);
                    }
                    isValid = false;
                }

                // Check định dạng email
                if (emailAttr == true && !(propValue == null || propValue.ToString() == String.Empty) && IsEmail(propValue.ToString()) == false)
                {
                    var listPropNameDisplay = property.GetCustomAttributes(typeof(PropNameDisplay), false).FirstOrDefault();
                    propName = (listPropNameDisplay as PropNameDisplay).PropName;
                    var error = propName + ResourceVN.ErrorFormat;
                    if (!listMsgErrors.Contains(error))
                    {
                        listMsgErrors.Add(error);
                    }
                    isValid = false;
                }
            }
            return isValid;
        }


        #endregion
    }
}

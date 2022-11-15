using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Entities
{
    public class JobPosition: BaseEntity
    {
        #region Property

        /// <summary>
        /// Id chức danh
        /// </summary>
        public int PositionID { get; set; }

        /// <summary>
        /// Tên chức danh
        /// </summary>
        public string? PositionName { get; set; }

        #endregion
    }
}

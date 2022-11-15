using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Entities
{
    public class Organization: BaseEntity
    {
        #region Property

        /// <summary>
        /// Id đơn vị
        /// </summary>
        public int OrganizationID { get; set; }

        /// <summary>
        /// Tên đơn vị
        /// </summary>
        public string? OrganizationName { get; set; }

        /// <summary>
        /// Mã đơn vị
        /// </summary>
        public string? OrganizationCode { get; set; }

        #endregion
    }
}

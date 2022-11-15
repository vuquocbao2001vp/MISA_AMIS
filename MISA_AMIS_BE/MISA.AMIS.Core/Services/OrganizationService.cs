using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Interfaces.Infrastructure;
using MISA.AMIS.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Services
{
    public class OrganizationService: BaseService<Organization>, IOrganizationService
    {
        #region Constructor

        IOrganizationRepository organizationRepository;
        public OrganizationService(IOrganizationRepository _organizationRepository) : base(_organizationRepository)
        {
            organizationRepository = _organizationRepository;
        }

        #endregion
    }
}

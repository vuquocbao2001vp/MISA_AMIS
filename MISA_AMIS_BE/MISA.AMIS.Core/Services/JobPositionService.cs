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
    public class JobPositionService: BaseService<JobPosition>, IJobPositionService
    {
        #region Constructor

        IJobPositionRepository jobPositionRepository;
        public JobPositionService(IJobPositionRepository _jobPositionRepository) : base(_jobPositionRepository)
        {
            jobPositionRepository = _jobPositionRepository;
        }

        #endregion
    }
}

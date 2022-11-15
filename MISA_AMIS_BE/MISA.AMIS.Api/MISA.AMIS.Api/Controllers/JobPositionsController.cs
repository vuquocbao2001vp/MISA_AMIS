using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Interfaces.Services;

namespace MISA.AMIS.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class JobPositionsController : MISABaseController<JobPosition>
    {
        IJobPositionService jobPositionService;
        public JobPositionsController(IJobPositionService _jobPositionService) : base(_jobPositionService)
        {
            jobPositionService = _jobPositionService;
        }
    }
}

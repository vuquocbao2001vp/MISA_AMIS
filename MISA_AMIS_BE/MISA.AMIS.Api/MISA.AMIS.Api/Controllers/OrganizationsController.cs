using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Interfaces.Services;

namespace MISA.AMIS.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrganizationsController : MISABaseController<Organization>
    {
        IOrganizationService organizationService;
        public OrganizationsController(IOrganizationService _organizationService) : base(_organizationService)
        {
            organizationService = _organizationService;
        }
    }
}

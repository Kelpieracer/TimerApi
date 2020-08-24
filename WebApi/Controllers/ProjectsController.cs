using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApi.Models.Projects;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectsController : BaseController
    {
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;
        public ProjectsController(IProjectService ProjectService, IMapper mapper)
        {
            _projectService = ProjectService;
            _mapper = mapper;
        }

        //[Authorize]
        [HttpGet("{id}")]
        public  ActionResult<ProjectResponse> Read(int id)
        {
            return Ok( _projectService.Read(id));
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ProjectResponse>> Create(CreateProjectRequest model)
        {
            return Ok(await _projectService.Create(model, Account));
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<ProjectResponse>> Update(UpdateProjectRequest model)
        {
            return Ok(await _projectService.Update(model, Account.AccountId));
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProjectResponse>> Delete(int id)
        {
            return Ok(await _projectService.Delete(id, Account.AccountId));
        }
    }
}

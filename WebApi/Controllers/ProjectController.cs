using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApi.Helpers;
using WebApi.Models.Projects;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectsController : BaseController
    {
        private readonly IProjectService _ProjectService;
        private readonly IMapper _mapper;
        public ProjectsController(IProjectService ProjectService, IMapper mapper)
        {
            _ProjectService = ProjectService;
            _mapper = mapper;
        }

        [Authorize]
        [HttpPost("create")]
        public IActionResult Create(CreateProjectRequest model)
        {
            return Ok(_ProjectService.Create(model, Account));
        }

        /*
                [Authorize]
                [HttpDelete("delete")]
                public IActionResult Delete(DeleteProjectRequest model)
                {
                    return reply.Get(_ProjectService.Delete(model.ProjectId, Account));
                }

                [Authorize]
                [HttpPut("add-work-item/{id: int}")]
                public IActionResult AddWorkItem(int workItemId)
                {
                    return reply.Get(_ProjectService.AddWorkItem(workItemId, Account));
                }

                [Authorize]
                [HttpDelete("remove-work-item/{id: int}")]
                public IActionResult RemoveWorkItem(int workItemId)
                {
                    return reply.Get(_ProjectService.RemoveWorkItem(workItemId, Account));
                }

                [Authorize]
                [HttpGet("all-projects")]
                public ActionResult<List<ProjectResponse>> AllProjects()
                {
                    var result = reply.Get(_ProjectService.AllProjects(Account));
                    return result.
                }

                [Authorize]
                [HttpGet("{id: int}")]
                public ActionResult<ProjectResponse> Project(int projectId)
                {
                    return reply.Get(_ProjectService.GetProject(projectId, Account));
                }
                */
    }
}

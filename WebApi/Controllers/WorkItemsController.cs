using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers;
using WebApi.Models.WorkItems;
using WebApi.Services;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkItemsController : BaseController
    {
        private readonly IWorkItemService _workItemService;
        private readonly IMapper _mapper;
        public WorkItemsController(IWorkItemService WorkItemService, IMapper mapper)
        {
            _workItemService = WorkItemService;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<WorkItemResponse> Read(int id)
        {
            return Ok(_workItemService.Read(id));
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<WorkItemResponse>> Create(CreateWorkItemRequest model)
        {
            return Ok(await _workItemService.Create(model, Account));
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<WorkItemResponse>> Update(UpdateWorkItemRequest model)
        {
            return Ok(await _workItemService.Update(model, Account.AccountId));
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<WorkItemResponse>> Delete(int id)
        {
            return Ok(await _workItemService.Delete(id, Account.AccountId));
        }
    }
}

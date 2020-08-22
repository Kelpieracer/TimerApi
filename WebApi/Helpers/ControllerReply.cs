using Microsoft.AspNetCore.Mvc;
using WebApi.Services;
using System.Collections.Generic;

namespace WebApi.Helpers
{
    public class ControllerReply
    {
        public IActionResult Get(ServiceReply serviceReply)
        {
            switch (serviceReply.ServiceResult)
            {
                case ServiceResult.Ok:
                    return new OkObjectResult(serviceReply.item);
                case ServiceResult.Conflict:
                    return new ConflictObjectResult(serviceReply.item);
                case ServiceResult.BadRequest:
                    return new BadRequestObjectResult(serviceReply.item);
                case ServiceResult.UnAuthorized:
                    return new UnauthorizedObjectResult(serviceReply.item);
                case ServiceResult.NotFound:
                    return new NotFoundObjectResult(serviceReply.item);
                case ServiceResult.NoContent:
                    return new NoContentResult();
                case ServiceResult.Created:
                    return new CreatedResult("Created", serviceReply.item);
            }
            return new BadRequestObjectResult("Unknown error");
        }
    }
}
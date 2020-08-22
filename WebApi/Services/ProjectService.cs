using AutoMapper;
using System.Linq;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.Projects;

namespace WebApi.Services
{
    public interface IProjectService
    {
        public ServiceReply Create(CreateProjectRequest model, Account account);
        public ServiceReply Delete(int id, Account account);
    }

    public class ProjectService : IProjectService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;
        private readonly IEmailService _emailService;


        public ProjectService(
            DataContext context
            // IMapper mapper,
            // IOptions<AppSettings> appSettings,
            // IEmailService emailService
            )
        {
            _context = context;
            // _mapper = mapper;
            // _appSettings = appSettings.Value;
            // _emailService = emailService;
        }

        public ServiceReply Create(CreateProjectRequest model, Account account)
        {
            if (account == null)
                return new ServiceReply { ServiceResult = ServiceResult.UnAuthorized, item = "Unauthorized access." };

            var Project = new Project { Name = model.Name, Manager = account };
            if (_context.Projects.Any(t => t.Name == Project.Name && t.Manager.Id == Project.Manager.Id))
                return new ServiceReply { ServiceResult = ServiceResult.Conflict, item = "Item already exists." };

            _context.Projects.Add(Project);
            _context.SaveChanges();
            return new ServiceReply { ServiceResult = ServiceResult.Created, item = new CreateProjectRequest { Name = Project.Name, ProjectId = Project.ProjectId } };
        }

        public ServiceReply Delete(int id, Account account)
        {
            if (account == null)
                return new ServiceReply { ServiceResult = ServiceResult.UnAuthorized, item = "Unauthorized access." };

            var Project = _context.Projects.FirstOrDefault(t => t.ProjectId == id);
            if (Project == null)
                return new ServiceReply { ServiceResult = ServiceResult.NotFound, item = "Item not found." };

            if (Project.Manager.Id != account.Id)
                return new ServiceReply { ServiceResult = ServiceResult.UnAuthorized, item = "Unauthorized to delete this item." };

            _context.Projects.Remove(Project);
            _context.SaveChanges();
            return new ServiceReply { ServiceResult = ServiceResult.NoContent, item = Project };
        }
    }
}

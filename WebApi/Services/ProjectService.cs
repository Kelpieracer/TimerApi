using AutoMapper;
using System.Linq;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.Projects;

namespace WebApi.Services
{
    public interface IProjectService
    {
        public ProjectResponse Create(CreateProjectRequest model, Account account);
        public void Delete(int id, Account account);
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

        public ProjectResponse Create(CreateProjectRequest model, Account account)
        {
            if (account == null)
                ErrorMessages.Throw(ErrorMessages.Code.UnAuthorized);

            var Project = new Project { Name = model.Name, Manager = account };
            if (_context.Projects.Any(t => t.Name == Project.Name && t.Manager.Id == Project.Manager.Id))
                ErrorMessages.Throw(ErrorMessages.Code.Conflict);

            _context.Projects.Add(Project);
            _context.SaveChanges();
            return _mapper.Map<ProjectResponse>(Project);
        }

        public void Delete(int id, Account account)
        {
            if (account == null)
                ErrorMessages.Throw(ErrorMessages.Code.UnAuthorized);

            var Project = _context.Projects.FirstOrDefault(t => t.ProjectId == id);
            if (Project == null)
                ErrorMessages.Throw(ErrorMessages.Code.NotFound);

            if (Project.Manager.Id != account.Id)
                ErrorMessages.Throw(ErrorMessages.Code.UnAuthorized);

            _context.Projects.Remove(Project);
            _context.SaveChanges();
            return;
        }
    }
}

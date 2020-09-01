using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Migrations;
using WebApi.Models.Projects;

namespace WebApi.Repositories
{
    public interface IProjectRepository : IRepository<Project>
    {
        public Task<List<Project>> FetchProjectsAsync(ReadProjectRequest request, Account account);
    }
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        private readonly DataContext _context;
        public ProjectRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Project>> FetchProjectsAsync(ReadProjectRequest request, Account account)
        {
            var projects = _context.Projects.Where(project =>
                project.AccountId == account.AccountId ||
                project.ProjectMembers.Any(member => member.AccountId == account.AccountId));
            if (projects == null)
                return null;
            if (request.CustomerId != null)
            {
                var customerProjects = _context.Customers.Where(customer => customer.CustomerId == request.CustomerId).Select(customer => customer.Projects.Select(project => project.ProjectId));
                if (customerProjects == null)
                    return null;
                projects = projects.Where(project => customerProjects.Any(customerProject => customerProject.Contains(project.ProjectId)));
            }
            if (request.NameContains != null)
            {
                projects = projects.Where(project => project.Name.Contains(request.NameContains));
            }
            if (projects == null)
                return null;
            if(request.ProjectIds != null && request.ProjectIds.Count > 0)
            {
                projects = projects.Where(project => request.ProjectIds.Contains(project.ProjectId));
            }
            if (projects == null)
                return null;
            var fetchedProjects =  await projects.ToListAsync();
            return fetchedProjects;
        }
    }
}

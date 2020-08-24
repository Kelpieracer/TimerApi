using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Repositories
{
    public interface IProjectRepository : IRepository<Project>
    {
        //
    }
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        private readonly DataContext _context;
        public ProjectRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}

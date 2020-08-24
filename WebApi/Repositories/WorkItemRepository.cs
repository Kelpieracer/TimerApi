using System.Threading.Tasks;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Repositories
{
    public interface IWorkItemRepository : IRepository<WorkItem>
    {
        //
    }
    public class WorkItemRepository : Repository<WorkItem>, IWorkItemRepository
    {
        private readonly DataContext _context;
        public WorkItemRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}

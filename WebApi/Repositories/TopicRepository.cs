using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Repositories
{
    public interface ITopicRepository : IRepository<Topic>
    {
        //Topic MyTopicSpecificMethod();
    }
    public class TopicRepository : Repository<Topic>, ITopicRepository
    {
        private readonly DataContext _context;
        public TopicRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}

using WebApi.Models;

namespace WebApi.Models.Topics
{
    public class TopicResponse : CommonResponses
    {
        public int TopicId { get; set; }
        public string Name { get; set; }
    }
}

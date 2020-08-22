using WebApi.Models.Accounts;

namespace WebApi.Models.Topics
{
    public class TopicResponse
    {
        public int TopicId { get; set; }
        public string Name { get; set; }
        public int AccountId { get; set; }
    }
}

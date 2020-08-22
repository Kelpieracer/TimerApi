namespace WebApi.Entities
{
    public class Topic
    {
        public int TopicId { get; set; }
        public string Name { get; set; }
        public Account Manager { get; set; }
    }
}

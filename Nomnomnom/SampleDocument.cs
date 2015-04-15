using SampleRavenRepository;

namespace SampleRavenRepositoryConsumer
{
    public class SampleDocument : IDocument<int>
    {
        public string Property1 { get; set; }
        public string Property2 { get; set; }
        public int Id { get; set; }
    }
}
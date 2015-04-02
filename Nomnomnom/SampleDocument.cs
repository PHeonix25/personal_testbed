
using testbed_class_lib;

namespace Nomnomnom
{
    public class SampleDocument : IDocument<int>
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }
}

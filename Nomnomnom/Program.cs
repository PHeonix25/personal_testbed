
using Microsoft.Practices.Unity;

using testbed_class_lib;

namespace Nomnomnom
{
    public class Program
    {
        static void Main(string[] args)
        {
            var container = new UnityContainer();

            var simpleImpl = new CoolblueRaven<SampleDocument>();

            var document = new SampleDocument { Value = "Filipe sucks." };
            simpleImpl.SaveTestDocument(document);

        }
    }
}

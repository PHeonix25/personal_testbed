using System;

namespace SampleRScriptManifestThing
{
    class Program
    {
        static void Main(string[] args)
        {
            var locator = new RScriptManifestLocator(typeof(Program).Assembly);

            foreach (var resource in locator.GetAllRScriptResources())
                Console.WriteLine("General resource found: " + resource);

            foreach (var resource in locator.GetAllRScriptResources("RScripts\\common"))
                Console.WriteLine("Common resource found: " + resource);

            foreach (var resource in locator.GetAllRScriptResources("RScripts\\common\\testing"))
                Console.WriteLine("Testing resource found: " + resource);
        }
    }
}

using System;
using System.Threading;
using System.Threading.Tasks;

using SampleRavenRepository;

namespace SampleRavenRepositoryConsumer
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            var cancellationToken = new CancellationToken();
            var t1 = Task.Run(() => DoRavenOperations(), cancellationToken);
            var t2 = Task.Run(() => Thread.Sleep(new TimeSpan(0, 1, 0, 0)), cancellationToken);
            Task.WaitAll(t1, t2);
        }

        private static void DoRavenOperations()
        {
            var ravenRepository = new RavenRepository<SampleDocument>();

            var document = new SampleDocument { Property1 = "1", Property2 = "2" };

            document = ravenRepository.Create(document);

            document = ravenRepository.Get(document.Id);

            if(DateTime.Now.Ticks % 2 == 0)
            {
                document.Property2 = "Changed to 7";
                ravenRepository.Update(document);
            }

            ravenRepository.Delete(document.Id);
        }
    }
}
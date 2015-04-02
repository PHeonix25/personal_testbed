using System;
using System.Collections.Generic;
using System.Linq;
using Raven.Client;
using Raven.Client.Document;

namespace testbed_class_lib
{
    public class RavenRepository<TDocument> : IRavenRepository<TDocument, int>
        where TDocument : class, IDocument<int>
    {
        private const int BATCHING_BATCH_SIZE = 1024;
        private readonly IDocumentStore _documentStore;

        public RavenRepository()
        {
            _documentStore = new DocumentStore() { ConnectionStringName = "RavenConnectionString" }; // "DataDir = ~\..\App_Data\RavenDB"
        }
        public RavenRepository(String connectionStringName)
        {
            _documentStore = new DocumentStore() { ConnectionStringName = connectionStringName };
        }
        public RavenRepository(IDocumentStore documentStore)
        {
            _documentStore = documentStore;
        }

        public TDocument Get(int id)
        {
            TDocument result;
            using (IDocumentSession session = _documentStore.OpenSession())
                result = session.Load<TDocument>(id);

            return result;
        }

        public TDocument Set(TDocument value)
        {
            using (IDocumentSession session = _documentStore.OpenSession())
            {
                session.Store(value);
                session.SaveChanges();
            }
            return value;
        }

        public bool Delete(int id)
        {
            var success = false;

            using (IDocumentSession session = _documentStore.OpenSession())
            {
                TDocument result = session.Load<TDocument>(id);
                if (result != null)
                {
                    session.Delete(result);
                    session.SaveChanges();
                    success = true;
                }
            }

            return success;
        }

        public IEnumerable<TDocument> GetAll()
        {
            var documents = new List<TDocument>();

            using (IDocumentSession session = _documentStore.OpenSession())
            {
                RavenQueryStatistics statistics;
                var query = session.Query<TDocument>().Statistics(out statistics).Take(BATCHING_BATCH_SIZE);

                documents.AddRange(query.ToList());

                while (documents.Count() != statistics.TotalResults)
                {
                    query = session.Query<TDocument>().Skip(documents.Count()).Take(BATCHING_BATCH_SIZE);
                    documents.AddRange(query.ToList());
                }
            }

            return documents;
        }

        public bool SetAll(IEnumerable<TDocument> documents)
        {
            using (IDocumentSession session = _documentStore.OpenSession())
            {
                foreach (var document in documents)
                    session.Store(document);
                session.SaveChanges();
            }

            return true;
        }
    }
}

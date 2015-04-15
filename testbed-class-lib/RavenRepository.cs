using System;
using System.Collections.Generic;
using System.Linq;

using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Embedded;

namespace SampleRavenRepository
{
    public class RavenRepository<TDocumentType>
        : IRavenRepository<TDocumentType, int>
        where TDocumentType : class, IDocument<int>
    {
        private readonly IDocumentStore _documentStore;

        public RavenRepository()
            : this(
                new EmbeddableDocumentStore
                {
                    ConnectionStringName = RavenDbConstants.DEFAULT_CONNECTION_STRING,
                    UseEmbeddedHttpServer = true
                })
        {
        }

        public RavenRepository(String connectionStringName)
            : this(new DocumentStore {ConnectionStringName = connectionStringName})
        {
        }

        private RavenRepository(IDocumentStore documentStore)
        {
            _documentStore = documentStore;
            _documentStore.Initialize();
        }

        public TDocumentType Create(TDocumentType document)
        {
            using(IDocumentSession session = _documentStore.OpenSession())
            {
                session.Store(document);
                session.SaveChanges();
            }
            return document;
        }

        public bool CreateAll(IEnumerable<TDocumentType> documents)
        {
            using(IDocumentSession session = _documentStore.OpenSession())
            {
                foreach(var document in documents)
                    session.Store(document);
                session.SaveChanges();
            }

            return true;
        }

        public TDocumentType Get(int id)
        {
            using(IDocumentSession session = _documentStore.OpenSession())
                return session.Load<TDocumentType>(id);
        }

        public IEnumerable<TDocumentType> GetAll()
        {
            var documents = new List<TDocumentType>();

            using(IDocumentSession session = _documentStore.OpenSession())
            {
                RavenQueryStatistics statistics;
                var query =
                    session.Query<TDocumentType>()
                           .Statistics(out statistics)
                           .Take(RavenDbConstants.DEFAULT_BATCHING_BATCH_SIZE);

                documents.AddRange(query.ToList());

                while(documents.Count() != statistics.TotalResults)
                {
                    query =
                        session.Query<TDocumentType>()
                               .Skip(documents.Count())
                               .Take(RavenDbConstants.DEFAULT_BATCHING_BATCH_SIZE);
                    documents.AddRange(query.ToList());
                }
            }

            return documents;
        }

        public TDocumentType Update(TDocumentType document)
        {
            using(IDocumentSession session = _documentStore.OpenSession())
            {
                session.Store(document);
                session.SaveChanges();
            }
            return document;
        }

        public bool UpdateAll(IEnumerable<TDocumentType> documents)
        {
            using(IDocumentSession session = _documentStore.OpenSession())
            {
                foreach(var document in documents)
                    session.Store(document);
                session.SaveChanges();
            }

            return true;
        }

        public bool Delete(int id)
        {
            using(IDocumentSession session = _documentStore.OpenSession())
            {
                var key = String.Format("{0}/{1}", typeof(TDocumentType).Name, id);
                session.Advanced.DocumentStore.DatabaseCommands.Delete(key, null);
                session.SaveChanges();
            }

            return true;
        }

        [Obsolete("This function hasn't been fully implemented.")]
        public IEnumerable<TDocumentType> GetAll_FuturePlanAsYetUntested()
        {
            var documents = new List<TDocumentType>();

            using(IDocumentSession session = _documentStore.OpenSession())
            {
                var query =
                    session.Query<TDocumentType>()
                           .Skip(documents.Count)
                           .Take(RavenDbConstants.DEFAULT_BATCHING_BATCH_SIZE);
                while(query.Any())
                    documents.AddRange(query.ToList());
            }

            return documents;
        }
    }
}
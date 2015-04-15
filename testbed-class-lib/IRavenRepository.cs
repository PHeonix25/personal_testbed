using System;
using System.Collections.Generic;

namespace SampleRavenRepository
{
    internal interface IRavenRepository<TDocument, in TIdentifierType>
        where TDocument : class, IDocument<TIdentifierType>
    {
        TDocument Get(TIdentifierType id);
        IEnumerable<TDocument> GetAll();

        TDocument Create(TDocument document);
        bool CreateAll(IEnumerable<TDocument> documents);

        TDocument Update(TDocument document);
        bool UpdateAll(IEnumerable<TDocument> documents);

        bool Delete(TIdentifierType id);
    }
}
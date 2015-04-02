using System.Collections.Generic;

namespace testbed_class_lib
{
    public interface IRavenRepository<TDocument, in TIdentifierType> 
        where TDocument : class, IDocument<TIdentifierType>
    {
        TDocument Get(TIdentifierType id);
        IEnumerable<TDocument> GetAll();

        TDocument Set(TDocument value);
        bool SetAll(IEnumerable<TDocument> documents); 

        bool Delete(TIdentifierType id);
    }
}

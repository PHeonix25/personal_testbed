namespace testbed_class_lib
{
    public class CoolblueRaven<T> where T : class, IDocument<int>
    {
        private readonly RavenRepository<T> _repository;

        public CoolblueRaven(RavenRepository<T> repository)
        {
            _repository = repository;
        }

        public void SaveTestDocument(T document)
        {
            _repository.Set(document);
        }
    }
}

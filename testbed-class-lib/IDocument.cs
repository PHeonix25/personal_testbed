namespace SampleRavenRepository
{
    public interface IDocument<TIdentifierType>
    {
        TIdentifierType Id { get; set; }
    }
}
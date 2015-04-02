namespace testbed_class_lib
{
    public interface IDocument<TIdentifierType>
    {
        TIdentifierType Id { get; set; }
    }
}

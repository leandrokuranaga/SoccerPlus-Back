namespace SoccerPlus.Infra.Http
{
    public interface IStorageFile
    {
        string Name { get; }
        string ContentType { get; }
        string Extension { get; }
        Stream Content { get; }
    }

}

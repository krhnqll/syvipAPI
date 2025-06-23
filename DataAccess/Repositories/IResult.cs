namespace DataAccess.Repositories
{
    public interface IResult<T>
    {
        bool Success { get; }
        string Message { get; }
        T Data { get; }
    }
}
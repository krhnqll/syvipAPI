namespace DataAccess.Service.Abstract
{
    public interface IDataResult<T> : IResult
    {
        T Data { get; }
    }
}
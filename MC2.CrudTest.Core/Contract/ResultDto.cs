namespace MC2.CrudTest.Core.Contract;

public interface IResult<T>
{
    T Result { get; }
}

public class ResultDto<T> : IResult<T>
{
    public bool IsOk { get; set; }
    public T Result { get; set; }
}
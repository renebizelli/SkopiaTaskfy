namespace Skopia.ReneBizelli.Taskfy.Api.Utils;

public record ResultOne <T> where T : class
{
    public T? Data { get; set; }

    public ResultOne(T? data)
    {
        Data = data;
    }
}

public record ResultMany<T> where T : class
{
    public int Count { get { return Data.Count(); } }
    public IList<T> Data { get; set; } = [];

    public ResultMany(IEnumerable<T> data)
    {
        Data = data.ToList();
    }
}

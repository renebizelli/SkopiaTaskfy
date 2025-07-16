using OneOf;
using Skopia.ReneBizelli.Taskfy.Api.Utils;

namespace Skopia.ReneBizelli.Taskfy.Api.Features.Reports.Performance;

public class Response : OneOfBase<ResultMany<PerformaceReportItem>, ErrorType>
{
    public Response(OneOf<ResultMany<PerformaceReportItem>, ErrorType> input) : base(input) { }

    public static implicit operator Response(ResultMany<PerformaceReportItem> _) => new Response(_);
    public static implicit operator Response(ErrorType error) => new Response(error);
}

 

public record PerformaceReportItem
{
    public User? User { get; set; }
    public int Count { get; set; }
}

public record User
{
    public Guid UserExportId { get; set; }
    public string Name { get; set; } = string.Empty;
}
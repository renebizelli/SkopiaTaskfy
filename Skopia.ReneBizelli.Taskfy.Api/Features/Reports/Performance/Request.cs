using MediatR;

namespace Skopia.ReneBizelli.Taskfy.Api.Features.Reports.Performance;

public record Request : IRequest<Response>
{
    public int Days { get; set; }
    public Request(int days)
    {
        Days = days;
    }
}

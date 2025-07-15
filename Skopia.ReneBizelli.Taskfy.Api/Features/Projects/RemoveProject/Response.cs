using OneOf;
using Skopia.ReneBizelli.Taskfy.Api.Utils;

namespace Skopia.ReneBizelli.Taskfy.Api.Features.TaskItems.RemoveProject;

public class Response : OneOfBase<ResponseType, ErrorType>
{
    public Response(OneOf<ResponseType, ErrorType> input) : base(input) { }

    public static implicit operator Response(ResponseType _) => new Response(_);
    public static implicit operator Response(ErrorType error) => new Response(error);
}


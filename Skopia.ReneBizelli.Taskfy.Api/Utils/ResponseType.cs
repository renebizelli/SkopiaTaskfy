using System.Net;

namespace Skopia.ReneBizelli.Taskfy.Api.Utils;

public abstract record ResponseType(HttpStatusCode httpStatusCode);
public abstract record ErrorType(HttpStatusCode HttpStatusCode) : ResponseType(HttpStatusCode);

public record AcceptType() : ResponseType(HttpStatusCode.Accepted);
public record InternalServerErrorType() : ResponseType(HttpStatusCode.InternalServerError);
public record NotFoundErrorType() : ErrorType(HttpStatusCode.NotFound);
public record BadRequestErrorType() : ErrorType(HttpStatusCode.BadRequest);
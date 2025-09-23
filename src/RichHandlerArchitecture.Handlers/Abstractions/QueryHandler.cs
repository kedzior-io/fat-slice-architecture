using RichHandlerArchitecture.Infrastructure.Persistence;
using Serilog;

namespace RichHandlerArchitecture.Handlers.Abstractions;

public abstract class QueryHandler<TQuery, TResponse>(IHandlerContext context) : Handler<TQuery, TResponse> where TQuery : IHandlerMessage<IHandlerResponse<TResponse>>
{
    protected readonly ILogger Logger = context.Logger;
    protected readonly IDbContext DbContext = context.DbContext;
}
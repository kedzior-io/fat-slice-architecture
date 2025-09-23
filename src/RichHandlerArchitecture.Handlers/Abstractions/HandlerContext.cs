using RichHandlerArchitecture.Infrastructure.Persistence;
using Serilog;

namespace RichHandlerArchitecture.Handlers.Abstractions;

public sealed class HandlerContext(IDbContext dbContext, ILogger logger) : IHandlerContext
{
    public IDbContext DbContext { get; private set; } = dbContext;
    public ILogger Logger { get; private set; } = logger;
}
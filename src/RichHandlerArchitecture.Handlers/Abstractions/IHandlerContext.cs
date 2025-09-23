using RichHandlerArchitecture.Infrastructure.Persistence;
using Serilog;

namespace RichHandlerArchitecture.Handlers.Abstractions;

public interface IHandlerContext
{
    IDbContext DbContext { get; }
    ILogger Logger { get; }
}
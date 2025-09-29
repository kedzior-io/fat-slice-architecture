using FatSliceArchitecture.Infrastructure.Persistence;
using Serilog;

namespace FatSliceArchitecture.Handlers;

public interface IHandlerContext
{
    IDbContext DbContext { get; }
    ILogger Logger { get; }
}
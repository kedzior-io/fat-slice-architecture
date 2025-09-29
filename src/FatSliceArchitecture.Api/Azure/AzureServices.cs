using FatSliceArchitecture.Core.Constants;
using FatSliceArchitecture.Infrastructure.Providers;
using Azure.Messaging.ServiceBus;

namespace FatSliceArchitecture.Api.Azure;

public static class AzureServices
{
    public static IServiceCollection AddServiceBus(this IServiceCollection services)
    {
        services.AddSingleton<ServiceBusClient>(_ =>
        {
            var serviceBusConnectionString = ConnectionStrings.ServiceBusConnection;

            if (string.IsNullOrWhiteSpace(serviceBusConnectionString))
            {
                throw new ApplicationException("ServiceBus Connection string is missing");
            }

            return new(serviceBusConnectionString);
        });

        services.AddSingleton<IServiceBusProvider, ServiceBusProvider>();

        return services;
    }
}
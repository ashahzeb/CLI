using Microsoft.Extensions.DependencyInjection;
using NFT.Application.Domain;
using NFT.Application.Domain.Repositories;
using NFT.Application.Helpers;
using NFT.Application.Processors;
using NFT.Application.Storage;

namespace NFT.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            .AddSingleton<IOwnershipData, OwnershipData>()
            .AddTransient<IJsonHelper<Transaction>, JsonHelper<Transaction>>()
            .AddTransient<ITransactionRepository, TransactionRepository>()
            .AddTransient<ITransactionProcessor, TransactionProcessor>();
        return services;
    }
}
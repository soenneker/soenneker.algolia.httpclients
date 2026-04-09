using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Algolia.HttpClients.Abstract;
using Soenneker.Utils.HttpClientCache.Registrar;

namespace Soenneker.Algolia.HttpClients.Registrars;

/// <summary>
/// Registers the OpenAPI HttpClient wrapper for dependency injection.
/// </summary>
public static class AlgoliaOpenApiHttpClientRegistrar
{
    /// <summary>
    /// Adds <see cref="AlgoliaOpenApiHttpClient"/> as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddAlgoliaOpenApiHttpClientAsSingleton(this IServiceCollection services)
    {
        services.AddHttpClientCacheAsSingleton()
                .TryAddSingleton<IAlgoliaOpenApiHttpClient, AlgoliaOpenApiHttpClient>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="AlgoliaOpenApiHttpClient"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddAlgoliaOpenApiHttpClientAsScoped(this IServiceCollection services)
    {
        services.AddHttpClientCacheAsSingleton()
                .TryAddScoped<IAlgoliaOpenApiHttpClient, AlgoliaOpenApiHttpClient>();

        return services;
    }
}

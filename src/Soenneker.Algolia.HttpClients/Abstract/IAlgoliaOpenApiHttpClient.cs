using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;

namespace Soenneker.Algolia.HttpClients.Abstract;

/// <summary>
/// Provides the cached, authenticated <see cref="HttpClient"/> used by the Algolia OpenAPI client.
/// </summary>
public interface IAlgoliaOpenApiHttpClient: IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Gets the shared named HTTP client, creating and configuring it on first use.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel client creation.</param>
    /// <returns>The cached HTTP client.</returns>
    ValueTask<HttpClient> Get(CancellationToken cancellationToken = default);
}

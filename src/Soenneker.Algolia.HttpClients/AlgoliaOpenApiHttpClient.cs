using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Soenneker.Dtos.HttpClientOptions;
using Soenneker.Extensions.Configuration;
using Soenneker.Algolia.HttpClients.Abstract;
using Soenneker.Utils.HttpClientCache.Abstract;

namespace Soenneker.Algolia.HttpClients;

public sealed class AlgoliaOpenApiHttpClient : IAlgoliaOpenApiHttpClient
{
    private readonly IHttpClientCache _httpClientCache;
    private readonly IConfiguration _config;

    public AlgoliaOpenApiHttpClient(IHttpClientCache httpClientCache, IConfiguration config)
    {
        _httpClientCache = httpClientCache;
        _config = config;
    }

    public ValueTask<HttpClient> Get(CancellationToken cancellationToken = default)
    {
        return _httpClientCache.Get(nameof(AlgoliaOpenApiHttpClient), _config, static config =>
        {
            var apiKey = config.GetValueStrict<string>("Algolia:ApiKey");
            var applicationId = config.GetValueStrict<string>("Algolia:ApplicationId");
            var baseUrl = config.GetValueStrict<string>("Algolia:ClientBaseUrl");
            string authHeaderName = config["Algolia:AuthHeaderName"] ?? "X-Algolia-API-Key";
            string authHeaderValueTemplate = config["Algolia:AuthHeaderValueTemplate"] ?? "{token}";
            string authHeaderValue = authHeaderValueTemplate.Replace("{token}", apiKey, StringComparison.Ordinal);

            return new HttpClientOptions
            {
                BaseAddress = new Uri(baseUrl),
                DefaultRequestHeaders = new Dictionary<string, string>
                {
                    {authHeaderName, authHeaderValue},
                    {"X-Algolia-Application-Id", applicationId},
                }
            };
        }, cancellationToken);
    }

    public void Dispose()
    {
        // The singleton cache owns the named client. A scoped provider must not remove it.
    }

    public ValueTask DisposeAsync()
    {
        // Kept for API compatibility; the singleton cache owns the named client.
        return ValueTask.CompletedTask;
    }
}

[![](https://img.shields.io/nuget/v/soenneker.algolia.httpclients.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.algolia.httpclients/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.algolia.httpclients/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.algolia.httpclients/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.algolia.httpclients.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.algolia.httpclients/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.algolia.httpclients/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.algolia.httpclients/actions/workflows/codeql.yml)

# Soenneker.Algolia.HttpClients

Provides a cached, authenticated `HttpClient` for the Algolia OpenAPI client.

## Installation

```bash
dotnet add package Soenneker.Algolia.HttpClients
```

## Configuration

```json
{
  "Algolia": {
    "ApplicationId": "your-application-id",
    "ApiKey": "your-api-key",
    "ClientBaseUrl": "https://analytics.eu.algolia.com"
  }
}
```

All three settings are required. Algolia exposes different hosts for different products and regions, so `ClientBaseUrl` must match the generated service you intend to call.

The client sends `X-Algolia-Application-Id` and `X-Algolia-API-Key` by default. Advanced integrations can override the API-key header with `Algolia:AuthHeaderName` and format its value with `Algolia:AuthHeaderValueTemplate`, using `{token}` as the API-key placeholder.

## Registration

```csharp
using Soenneker.Algolia.HttpClients.Registrars;

services.AddAlgoliaOpenApiHttpClientAsSingleton();
```

`AddAlgoliaOpenApiHttpClientAsScoped()` is also available. Both registrations reuse the singleton HTTP-client cache.

## Usage

```csharp
using Soenneker.Algolia.HttpClients.Abstract;

public sealed class AlgoliaTransport
{
    private readonly IAlgoliaOpenApiHttpClient _clientProvider;

    public AlgoliaTransport(IAlgoliaOpenApiHttpClient clientProvider)
    {
        _clientProvider = clientProvider;
    }

    public async Task<HttpResponseMessage> Send(HttpRequestMessage request, CancellationToken cancellationToken = default)
    {
        HttpClient client = await _clientProvider.Get(cancellationToken);
        return await client.SendAsync(request, cancellationToken);
    }
}
```

`Get()` creates the named client on first use and returns that cached instance afterward. Configuration changes do not rebuild an existing client. The dependency-injection container owns resolved providers, and disposing a scoped provider does not remove the shared client.

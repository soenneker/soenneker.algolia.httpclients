using Soenneker.Algolia.HttpClients.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Algolia.HttpClients.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class AlgoliaOpenApiHttpClientTests : HostedUnitTest
{
    private readonly IAlgoliaOpenApiHttpClient _httpclient;

    public AlgoliaOpenApiHttpClientTests(Host host) : base(host)
    {
        _httpclient = Resolve<IAlgoliaOpenApiHttpClient>(true);
    }

    [Test]
    public void Default()
    {

    }
}

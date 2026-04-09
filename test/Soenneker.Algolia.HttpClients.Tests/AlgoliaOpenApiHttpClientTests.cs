using Soenneker.Algolia.HttpClients.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;

namespace Soenneker.Algolia.HttpClients.Tests;

[Collection("Collection")]
public sealed class AlgoliaOpenApiHttpClientTests : FixturedUnitTest
{
    private readonly IAlgoliaOpenApiHttpClient _httpclient;

    public AlgoliaOpenApiHttpClientTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _httpclient = Resolve<IAlgoliaOpenApiHttpClient>(true);
    }

    [Fact]
    public void Default()
    {

    }
}

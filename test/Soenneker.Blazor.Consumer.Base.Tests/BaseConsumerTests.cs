using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Soenneker.Blazor.Consumer.Base.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;
using Xunit.Abstractions;

namespace Soenneker.Blazor.Consumer.Base.Tests;

[Collection("Collection")]
public class BaseConsumerTests : FixturedUnitTest
{
    private readonly IBaseConsumer _util;

    public BaseConsumerTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _util = Resolve<IBaseConsumer>(true);
    }
}

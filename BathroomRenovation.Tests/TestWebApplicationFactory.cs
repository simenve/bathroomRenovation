using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace BathroomRenovation.Tests
{
    public class TestWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {
        private readonly IConfiguration _configuration;
        private readonly IServiceCollection _services;

        public TestWebApplicationFactory(IServiceCollection services) : this(new ConfigurationBuilder().Build(), services)
        { }

        public TestWebApplicationFactory(IConfiguration configuration) : this(configuration, new ServiceCollection())
        { }

        public TestWebApplicationFactory(IConfiguration configuration, IServiceCollection services)
        {
            _configuration = configuration;
            _services = services;
        }

        protected override void ConfigureClient(HttpClient client)
        {
            base.ConfigureClient(client);
        }

        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.UseEnvironment("Test");

            builder.ConfigureServices(services => services.Add(_services));

            return base.CreateHost(builder);
        }
    }
}

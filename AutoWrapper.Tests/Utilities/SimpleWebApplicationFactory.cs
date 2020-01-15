using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Hosting;

namespace AutoWrapper.WebAPI.Tests.Utilities
{
    public class SimpleWebApplicationFactory : WebApplicationFactory<Startup>
    {
        public SimpleWebApplicationFactory()
        {            
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseStartup<Startup>()
                   .ConfigureServices(services =>
                   {      
                       
                   });
        }
    }
}

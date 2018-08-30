using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Trov.IdentityServer4.Admin.Controllers;
using Trov.IdentityServer4.EntityFramework.DbContexts;
using Xunit;

namespace Trov.IdentityServer4.Admin.UnitTests.Controllers
{
    public class HomeControllerTests
    {
        private readonly IServiceProvider _serviceProvider;

        public HomeControllerTests()
        {
            var efServiceProvider = new ServiceCollection().AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

            var services = new ServiceCollection();
            services.AddDbContext<AdminDbContext>(b => b.UseInMemoryDatabase(Guid.NewGuid().ToString()).UseInternalServiceProvider(efServiceProvider));
            services.AddLogging();

            _serviceProvider = services.BuildServiceProvider();
        }

        [Fact]
        public void GetIndex()
        {
            // Arrange
            var logger = _serviceProvider.GetRequiredService<ILogger<ConfigurationController>>();

            var controller = new HomeController(logger);
            
            // Action
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Null(viewResult.ViewName);

            Assert.NotNull(viewResult.ViewData);
        }
    }
}

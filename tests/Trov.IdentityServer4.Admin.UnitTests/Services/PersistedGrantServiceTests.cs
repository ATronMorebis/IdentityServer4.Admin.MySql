using System;
using System.Threading.Tasks;
using FluentAssertions;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Moq;
using Trov.IdentityServer4.Admin.BusinessLogic.Repositories;
using Trov.IdentityServer4.Admin.BusinessLogic.Resources;
using Trov.IdentityServer4.Admin.BusinessLogic.Services;
using Trov.IdentityServer4.Admin.UnitTests.Mocks;
using Trov.IdentityServer4.EntityFramework.DbContexts;
using Xunit;

namespace Trov.IdentityServer4.Admin.UnitTests.Services
{
    public class PersistedGrantServiceTests
    {
        public PersistedGrantServiceTests()
        {
            var databaseName = Guid.NewGuid().ToString();

            _dbAdminContextOptions = new DbContextOptionsBuilder<AdminDbContext>()
                .UseInMemoryDatabase(databaseName)
                .Options;

            _dbConfigurationContextOptions = new DbContextOptionsBuilder<ConfigurationDbContext>()
                .UseInMemoryDatabase(databaseName)
                .Options;

            _storeOptions = new ConfigurationStoreOptions();
            _operationalStore = new OperationalStoreOptions();
        }

        private readonly DbContextOptions<AdminDbContext> _dbAdminContextOptions;
        private readonly DbContextOptions<ConfigurationDbContext> _dbConfigurationContextOptions;
        private readonly ConfigurationStoreOptions _storeOptions;
        private readonly OperationalStoreOptions _operationalStore;

        [Fact]
        public async Task GetPersitedGrantAsync()
        {
            using (var context = new ConfigurationDbContext(_dbConfigurationContextOptions, _storeOptions, _operationalStore))
            {
                var persistedGrantRepository = new PersistedGrantRepository(new AdminDbContext(_dbAdminContextOptions), context);

                var localizerMock = new Mock<IPersistedGrantServiceResources>();
                var localizer = localizerMock.Object;

                var persistedGrantService = new PersistedGrantService(persistedGrantRepository, localizer);

                //Generate persisted grant
                var persistedGrantKey = Guid.NewGuid().ToString();
                var persistedGrant = PersistedGrantMock.GenerateRandomPersistedGrant(persistedGrantKey);

                //Try add new persisted grant
                await context.PersistedGrants.AddAsync(persistedGrant);
                await context.SaveChangesAsync();

                //Try get persisted grant
                var persistedGrantAdded = await persistedGrantService.GetPersitedGrantAsync(persistedGrantKey);

                //Assert
                persistedGrant.ShouldBeEquivalentTo(persistedGrantAdded);
            }
        }

        [Fact]
        public async Task DeletePersistedGrantAsync()
        {
            using (var context = new ConfigurationDbContext(_dbConfigurationContextOptions, _storeOptions, _operationalStore))
            {
                var persistedGrantRepository = new PersistedGrantRepository(new AdminDbContext(_dbAdminContextOptions), context);

                var localizerMock = new Mock<IPersistedGrantServiceResources>();
                var localizer = localizerMock.Object;

                var persistedGrantService = new PersistedGrantService(persistedGrantRepository, localizer);

                //Generate persisted grant
                var persistedGrantKey = Guid.NewGuid().ToString();
                var persistedGrant = PersistedGrantMock.GenerateRandomPersistedGrant(persistedGrantKey);

                //Try add new persisted grant
                await context.PersistedGrants.AddAsync(persistedGrant);
                await context.SaveChangesAsync();

                //Try delete persisted grant
                await persistedGrantService.DeletePersistedGrantAsync(persistedGrantKey);

                var grant = await persistedGrantRepository.GetPersitedGrantAsync(persistedGrantKey);

                //Assert
                grant.Should().BeNull();
            }
        }

        [Fact]
        public async Task DeletePersistedGrantsAsync()
        {
            using (var context = new ConfigurationDbContext(_dbConfigurationContextOptions, _storeOptions, _operationalStore))
            {
                var persistedGrantRepository = new PersistedGrantRepository(new AdminDbContext(_dbAdminContextOptions), context);

                var localizerMock = new Mock<IPersistedGrantServiceResources>();
                var localizer = localizerMock.Object;

                var persistedGrantService = new PersistedGrantService(persistedGrantRepository, localizer);

                const int subjectId = 1;

                for (var i = 0; i < 4; i++)
                {
                    //Generate persisted grant
                    var persistedGrantKey = Guid.NewGuid().ToString();
                    var persistedGrant = PersistedGrantMock.GenerateRandomPersistedGrant(persistedGrantKey, subjectId);

                    //Try add new persisted grant
                    await context.PersistedGrants.AddAsync(persistedGrant);
                }

                await context.SaveChangesAsync();

                //Try delete persisted grant
                await persistedGrantService.DeletePersistedGrantsAsync(subjectId);

                var grant = await persistedGrantRepository.GetPersitedGrantsByUser(subjectId.ToString());

                //Assert
                grant.TotalCount.Should().Be(0);
            }
        }
    }
}
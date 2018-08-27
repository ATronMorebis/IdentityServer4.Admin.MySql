using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Extensions;
using IdentityServer4.EntityFramework.Interfaces;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;

namespace Skoruba.IdentityServer4.Admin.EntityFramework.DbContexts
{
    public class ConfigurationDbContext : DbContext, IConfigurationDbContext, IPersistedGrantDbContext
    {
        private readonly ConfigurationStoreOptions _storeOptions;
        private readonly OperationalStoreOptions _operationalOptions;

        public ConfigurationDbContext(DbContextOptions<ConfigurationDbContext> options,
            ConfigurationStoreOptions storeOptions,
            OperationalStoreOptions operationalOptions) 
            : base(options)
        {
            _storeOptions = storeOptions;
            _operationalOptions = operationalOptions;
        }

        public DbSet<PersistedGrant> PersistedGrants { get; set; }
        
        public DbSet<Client> Clients { get; set; }

        public DbSet<IdentityResource> IdentityResources { get; set; }

        public DbSet<ApiResource> ApiResources { get; set; }
        
        public DbSet<ApiSecret> ApiSecrets { get; set; }

        public DbSet<ApiScope> ApiScopes { get; set; }

        public DbSet<ApiScopeClaim> ApiScopeClaims { get; set; }

        public DbSet<IdentityClaim> IdentityClaims { get; set; }

        public DbSet<ApiResourceClaim> ApiResourceClaims { get; set; }

        public DbSet<ClientGrantType> ClientGrantTypes { get; set; }

        public DbSet<ClientScope> ClientScopes { get; set; }

        public DbSet<ClientSecret> ClientSecrets { get; set; }

        public DbSet<ClientPostLogoutRedirectUri> ClientPostLogoutRedirectUris { get; set; }

        public DbSet<ClientCorsOrigin> ClientCorsOrigins { get; set; }

        public DbSet<ClientIdPRestriction> ClientIdPRestrictions { get; set; }

        public DbSet<ClientRedirectUri> ClientRedirectUris { get; set; }

        public DbSet<ClientClaim> ClientClaims { get; set; }

        public DbSet<ClientProperty> ClientProperties { get; set; }

        //public DbSet<Log> Logs { get; set; }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureClientContext(_storeOptions);
            builder.ConfigureResourcesContext(_storeOptions);
            builder.ConfigurePersistedGrantContext(_operationalOptions);
        }
    }
}

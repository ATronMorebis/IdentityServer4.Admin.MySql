using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Trov.IdentityServer4.Admin.BusinessLogic.Dtos.Common;
using Trov.IdentityServer4.Admin.BusinessLogic.Dtos.Enums;
using Trov.IdentityServer4.Admin.BusinessLogic.Helpers;
using Trov.IdentityServer4.EntityFramework.DbContexts;
using Trov.IdentityServer4.EntityFramework.Entities;

namespace Trov.IdentityServer4.Admin.BusinessLogic.Repositories
{
    public class PersistedGrantRepository : IPersistedGrantRepository
    {
        private readonly AdminDbContext _dbContext;
        private readonly ConfigurationDbContext _configDbContext;

        public bool AutoSaveChanges { get; set; } = true;

        public PersistedGrantRepository(AdminDbContext dbContext, ConfigurationDbContext configDbContext)
        {
            _dbContext = dbContext;
            _configDbContext = configDbContext;
        }

        public async Task<PagedList<PersistedGrantDataView>> GetPersitedGrantsByUsers(string search, int page = 1, int pageSize = 10)
        {
            var pagedList = new PagedList<PersistedGrantDataView>();
            var users = await _dbContext.Users.ToListAsync();
            var persistedGrants = await _configDbContext.PersistedGrants.ToListAsync();

            var persistedGrantByUsers = (from pe in persistedGrants
                                         join us in users on Convert.ToInt32(pe.SubjectId) equals us.Id into per
                                         from us in per.DefaultIfEmpty()
                                         select new PersistedGrantDataView
                                         {
                                             SubjectId = pe.SubjectId,
                                             SubjectName = us == null ? string.Empty : us.UserName
                                         })
                .Distinct().Where(x => string.IsNullOrEmpty(search) || (x.SubjectId.Contains(search) || x.SubjectName.Contains(search)))
                .AsEnumerable().ToList();


            var persistedGrantsData = persistedGrantByUsers.OrderBy(x => x.SubjectId).Skip(page * pageSize).Take(pageSize).ToList();
            var persistedGrantsDataCount = persistedGrantByUsers.Count;

            pagedList.Data.AddRange(persistedGrantsData);
            pagedList.TotalCount = persistedGrantsDataCount;
            pagedList.PageSize = pageSize;

            return pagedList;
        }

        public async Task<PagedList<PersistedGrant>> GetPersitedGrantsByUser(string subjectId, int page = 1, int pageSize = 10)
        {
            var pagedList = new PagedList<PersistedGrant>();

            var persistedGrantsData = await _configDbContext.PersistedGrants.Where(x => x.SubjectId == subjectId).Select(x => new PersistedGrant()
            {
                SubjectId = x.SubjectId,
                Type = x.Type,
                Key = x.Key,
                ClientId = x.ClientId,
                Data = x.Data,
                Expiration = x.Expiration,
                CreationTime = x.CreationTime
            }).PageBy(x => x.SubjectId, page, pageSize).ToListAsync();

            var persistedGrantsCount = await _configDbContext.PersistedGrants.Where(x => x.SubjectId == subjectId).CountAsync();

            pagedList.Data.AddRange(persistedGrantsData);
            pagedList.TotalCount = persistedGrantsCount;
            pagedList.PageSize = pageSize;

            return pagedList;
        }

        public Task<PersistedGrant> GetPersitedGrantAsync(string key)
        {
            return _configDbContext.PersistedGrants.SingleOrDefaultAsync(x => x.Key == key);
        }

        public async Task<int> DeletePersistedGrantAsync(string key)
        {
            var persistedGrant = await _configDbContext.PersistedGrants.Where(x => x.Key == key).SingleOrDefaultAsync();

            _configDbContext.PersistedGrants.Remove(persistedGrant);

            return await AutoSaveChangesAsync();
        }

        public Task<bool> ExistsPersistedGrantsAsync(string subjectId)
        {
            return _configDbContext.PersistedGrants.AnyAsync(x => x.SubjectId == subjectId);
        }

        public async Task<int> DeletePersistedGrantsAsync(int userId)
        {
            var grants = await _configDbContext.PersistedGrants.Where(x => x.SubjectId == userId.ToString()).ToListAsync();

            _configDbContext.RemoveRange(grants);

            return await AutoSaveChangesAsync();
        }

        private async Task<int> AutoSaveChangesAsync()
        {
            return AutoSaveChanges ? await _configDbContext.SaveChangesAsync() : (int)SavedStatus.WillBeSavedExplicitly;
        }

        public async Task<int> SaveAllChangesAsync()
        {
            return await _configDbContext.SaveChangesAsync();
        }
    }
}
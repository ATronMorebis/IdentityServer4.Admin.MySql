using System.Threading.Tasks;
using Trov.IdentityServer4.Admin.BusinessLogic.Dtos.Common;
using Trov.IdentityServer4.EntityFramework.DbContexts;
using Trov.IdentityServer4.EntityFramework.Entities;

namespace Trov.IdentityServer4.Admin.BusinessLogic.Repositories
{
    public class LogRepository : ILogRepository
    {
        private readonly AdminDbContext _dbContext;

        public LogRepository(AdminDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PagedList<Log>> GetLogsAsync(string search, int page = 1, int pageSize = 10)
        {
            var pagedList = new PagedList<Log>();
            //Expression<Func<Log, bool>> searchCondition = x => x.LogEvent.Contains(search) || x.Message.Contains(search) || x.Exception.Contains(search);
            //var logs = await _dbContext.Logs
            //    .WhereIf(!string.IsNullOrEmpty(search), searchCondition)                
            //    .PageBy(x => x.Id, page, pageSize)
            //    .ToListAsync();

            //pagedList.Data.AddRange(logs);
            //pagedList.PageSize = pageSize;
            //pagedList.TotalCount = await _dbContext.Logs.WhereIf(!string.IsNullOrEmpty(search), searchCondition).CountAsync();

            return pagedList;
        }
    }
}
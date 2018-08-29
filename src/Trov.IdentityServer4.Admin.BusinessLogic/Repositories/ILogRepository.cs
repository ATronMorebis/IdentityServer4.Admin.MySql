using System.Threading.Tasks;
using Trov.IdentityServer4.Admin.BusinessLogic.Dtos.Common;
using Trov.IdentityServer4.EntityFramework.Entities;

namespace Trov.IdentityServer4.Admin.BusinessLogic.Repositories
{
    public interface ILogRepository
    {
        Task<PagedList<Log>> GetLogsAsync(string search, int page = 1, int pageSize = 10);
    }
}
using System.Collections.Generic;

namespace Trov.IdentityServer4.Admin.BusinessLogic.Dtos.Log
{
    public class LogsDto
    {
        public LogsDto()
        {
            Logs = new List<LogDto>();
        }

        public List<LogDto> Logs { get; set; }

        public int TotalCount { get; set; }

        public int PageSize { get; set; }
    }
}

using System.Collections.Generic;
using Trov.IdentityServer4.Admin.BusinessLogic.Dtos.Common;
using Trov.IdentityServer4.Admin.BusinessLogic.Dtos.Identity.Base;

namespace Trov.IdentityServer4.Admin.BusinessLogic.Dtos.Identity
{
    public class UserRolesDto : BaseUserRolesDto<int, int>
    {
        public UserRolesDto()
        {
           Roles = new List<RoleDto>(); 
        }
        
        public List<SelectItem> RolesList { get; set; }

        public List<RoleDto> Roles { get; set; }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }
    }
}

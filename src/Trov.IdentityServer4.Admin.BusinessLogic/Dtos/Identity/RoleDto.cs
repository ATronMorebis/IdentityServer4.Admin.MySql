using System.ComponentModel.DataAnnotations;
using Trov.IdentityServer4.Admin.BusinessLogic.Dtos.Identity.Base;

namespace Trov.IdentityServer4.Admin.BusinessLogic.Dtos.Identity
{
    public class RoleDto : BaseRoleDto<int>
    {      
        [Required]
        public string Name { get; set; }
    }
}
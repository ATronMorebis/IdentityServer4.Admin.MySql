using System.ComponentModel.DataAnnotations;
using Trov.IdentityServer4.Admin.BusinessLogic.Dtos.Identity.Base;

namespace Trov.IdentityServer4.Admin.BusinessLogic.Dtos.Identity
{
    public class UserChangePasswordDto : BaseUserChangePasswordDto<int>
    {        
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}

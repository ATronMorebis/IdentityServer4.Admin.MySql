using System.Collections.Generic;

namespace Trov.IdentityServer4.Admin.BusinessLogic.Dtos.Configuration
{
	public class IdentityResourcesDto
	{
		public IdentityResourcesDto()
		{
			IdentityResources = new List<IdentityResourceDto>();
		}

		public int PageSize { get; set; }

		public int TotalCount { get; set; }

		public List<IdentityResourceDto> IdentityResources { get; set; }
	}
}
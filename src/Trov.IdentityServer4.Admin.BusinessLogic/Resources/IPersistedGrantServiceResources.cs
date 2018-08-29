using Trov.IdentityServer4.Admin.BusinessLogic.Helpers;

namespace Trov.IdentityServer4.Admin.BusinessLogic.Resources
{
    public interface IPersistedGrantServiceResources
    {
        ResourceMessage PersistedGrantDoesNotExist();

        ResourceMessage PersistedGrantWithSubjectIdDoesNotExist();
    }
}

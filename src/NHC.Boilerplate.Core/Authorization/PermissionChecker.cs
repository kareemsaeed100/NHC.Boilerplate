using Abp.Authorization;
using NHC.Boilerplate.Authorization.Roles;
using NHC.Boilerplate.Authorization.Users;

namespace NHC.Boilerplate.Authorization;

public class PermissionChecker : PermissionChecker<Role, User>
{
    public PermissionChecker(UserManager userManager)
        : base(userManager)
    {
    }
}

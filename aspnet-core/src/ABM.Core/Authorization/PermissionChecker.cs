using Abp.Authorization;
using ABM.Authorization.Roles;
using ABM.Authorization.Users;

namespace ABM.Authorization;

public class PermissionChecker : PermissionChecker<Role, User>
{
    public PermissionChecker(UserManager userManager)
        : base(userManager)
    {
    }
}

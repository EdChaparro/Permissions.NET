using System.Collections.Generic;

namespace IntrepidProducts.Permissions
{
    public interface IPermission
    {
        string Resource { get; }
        string Action { get; }
        bool IsAuthorized(string resource, BuiltInPermissionAction action);
        bool IsAuthorized(string resource, string action);
        IEnumerable<IPermission> AllPermissions();
    }
}
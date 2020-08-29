using System.Collections.Generic;

namespace IntrepidProducts.Permissions
{
    public class AnyonePermission : Permission
    {
        public AnyonePermission(BuiltInPermissionAction action, string resource) : base(action, resource)
        {}

        public AnyonePermission(string action, string resource) : base(action, resource)
        {}

        public override bool IsAuthorized(string resource, BuiltInPermissionAction action)
        {
            return true;
        }

        public override bool IsAuthorized(string resource, string action)
        {
            return true;
        }

        public override IEnumerable<IPermission> AllPermissions()
        {
            return new List<IPermission> { this };
        }
    }
}
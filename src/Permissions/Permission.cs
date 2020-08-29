using System.Collections.Generic;
using System.Linq;
using intrepidproducts.common;

namespace intrepidproducts.permissions
{
    public class Permission : IPermission
    {
        public Permission(BuiltInPermissionAction action, string resource)
            : this(EnumHelper.GetEnumDescription(action), resource)
        {}

        public Permission(string action, string resource)
        {
            Action = action;
            Resource = resource;
        }

        public string Action { get; }
        public string Resource { get; }

        public virtual bool IsAuthorized(string resource, BuiltInPermissionAction action)
        {
            return IsAuthorized(resource, EnumHelper.GetEnumDescription(action));
        }

        public virtual bool IsAuthorized(string resource, string action)
        {
            return AllPermissions().Any(x => x.Action == action);
        }

        private IEnumerable<IPermission> _allPermissions;

        public virtual IEnumerable<IPermission> AllPermissions()
        {
            return _allPermissions ??= BuildPermissionCollection();
        }

        /// <summary>
        /// Adds present and implied permissions into a collection of
        /// concrete permission objects.  For example an EDIT permission
        /// would AllPermissions into two Permission objects of EDIT and READ.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<IPermission> BuildPermissionCollection()
        {
            var actionEnum = EnumHelper.GetEnumFromString<BuiltInPermissionAction>(Action);
            if (actionEnum == null)
            {
                return new List<IPermission> { this };
            }

            switch (actionEnum)
            {
                case BuiltInPermissionAction.Create:
                    return new List<IPermission>
                    {
                        this,
                        new Permission(action: BuiltInPermissionAction.Edit, resource: Resource),
                        new Permission(action: BuiltInPermissionAction.Read, resource: Resource),
                    };

                case BuiltInPermissionAction.Edit:
                    return new List<IPermission>
                    {
                        this,
                        new Permission (action: BuiltInPermissionAction.Read, resource: Resource),
                    };
            }
            return new List<IPermission> { this };
        }

        public override string ToString()
        {
            return $"Resource = {Resource}, Action = {Action}";
        }
    }
}
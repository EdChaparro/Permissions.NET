using System;

namespace IntrepidProducts.Permissions
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class RequiredPermissionAttribute : Attribute, IPermissionAttribute
    {
        public RequiredPermissionAttribute(string resource, BuiltInPermissionAction action)
            : this(resource, action.ToString())
        { }


        public RequiredPermissionAttribute(string resource, string action)
        {
            Permission = new Permission(action, resource);
        }

        public IPermission Permission { get; private set; }
    }
}
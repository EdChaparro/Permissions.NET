using System;
using System.Collections.Generic;
using System.Globalization;

namespace intrepidproducts.permissions
{
    public interface IResourcePermissionRetriever
    {
        IPermission? GetRequiredPermissionFor(Type type, string methodName);
    }

    public class ResourcePermissionRetriever : IResourcePermissionRetriever
    {
        private readonly TextInfo _textInfo = new CultureInfo("en-US").TextInfo;

        public IPermission? GetRequiredPermissionFor(Type type, string methodName)
        {
            if ((type == null) || (methodName == null))
            {
                return null;
            }

            var standardRestVerbs = new List<string> { "get", "post", "put" };
            var methodNameLowerCase = methodName.ToLower();
            var methodNameProperCased = methodName;

            if (standardRestVerbs.Contains(methodNameLowerCase))
            {
                methodNameProperCased = _textInfo.ToTitleCase(methodNameLowerCase);
            }

            var methodInfo = type.GetMethod(methodNameProperCased);

            if (methodInfo == null)
            {
                return null;
            }

            var attributes = methodInfo.GetCustomAttributes(typeof(IPermissionAttribute), false);

            foreach (var attr in attributes)
            {
                if (attr is IPermissionAttribute)
                {
                    return ((IPermissionAttribute)attr).Permission;
                }
            }

            return null;
        }
    }
}
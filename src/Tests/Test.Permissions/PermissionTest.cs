using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace intrepidproducts.permissions.test
{
    [TestClass]
    public class PermissionTest
    {
        #region IsAuthorized
        [TestMethod]
        public void ShouldNotAllowOtherPermissionsWhenOnlyReadGranted()
        {
            var permission = new Permission(action: "Read", resource: "Foo");
            Assert.IsFalse(permission.IsAuthorized("Foo", "Create"));
            Assert.IsFalse(permission.IsAuthorized("Foo", "Edit"));
            Assert.IsTrue(permission.IsAuthorized("Foo", "Read"));
        }

        [TestMethod]
        public void ShouldAcceptAllPrivilegesWhenCreateGranted()
        {
            var permission = new Permission(action: "Create", resource: "Foo");
            Assert.IsTrue(permission.IsAuthorized("Foo", "Create"));
            Assert.IsTrue(permission.IsAuthorized("Foo", "Edit"));
            Assert.IsTrue(permission.IsAuthorized("Foo", "Read"));
        }

        [TestMethod]
        public void ShouldNotAcceptCreatePrivilegeWhenOnlyEditGranted()
        {
            var permission = new Permission(action: "Edit", resource: "Foo");
            Assert.IsFalse(permission.IsAuthorized("Foo", "Create"));
            Assert.IsTrue(permission.IsAuthorized("Foo", "Edit"));
            Assert.IsTrue(permission.IsAuthorized("Foo", "Read"));
        }

        [TestMethod]
        public void ShouldNotAuthorizeAnUnknownResource()
        {
            var permission = new Permission(action: "Edit", resource: "Foo");
            Assert.IsFalse(permission.IsAuthorized("Bar", "Create"));
        }

        [TestMethod]
        public void ShouldNotAuthorizeAnUnknownAction()
        {
            var permission = new Permission(action: "Garbage", resource: "Foo");
            Assert.IsFalse(permission.IsAuthorized("Bar", "Create"));
        }
        #endregion

        #region AllPermissions
        [TestMethod]
        public void ShouldItemizeAllPermissionsForCreate()
        {
            var permission = new Permission(action: "Create", resource: "Foo");

            var permissions = permission.AllPermissions().ToList();
            Assert.AreEqual(3, permissions.Count());

            Assert.IsTrue(permissions.Any(x => x.Action == "Create"));
            Assert.IsTrue(permissions.Any(x => x.Action == "Edit"));
            Assert.IsTrue(permissions.Any(x => x.Action == "Read"));
        }

        [TestMethod]
        public void ShouldItemizeAllPermissionsForEdit()
        {
            var permission = new Permission(action: "Edit", resource: "Foo");

            var permissions = permission.AllPermissions().ToList();
            Assert.AreEqual(2, permissions.Count());

            Assert.IsTrue(permissions.Any(x => x.Action == "Edit"));
            Assert.IsTrue(permissions.Any(x => x.Action == "Read"));
        }


        [TestMethod]
        public void ShouldItemizeAllPermissionsForRead()
        {
            var permission = new Permission(action: "Read", resource: "Foo");

            var permissions = permission.AllPermissions().ToList();
            Assert.AreEqual(1, permissions.Count());

            Assert.IsTrue(permissions.Any(x => x.Action == "Read"));
        }
        #endregion
    }
}
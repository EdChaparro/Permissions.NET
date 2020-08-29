namespace IntrepidProducts.Permissions
{
    public interface IPermissionAttribute
    {
        IPermission Permission { get; }
    }
}
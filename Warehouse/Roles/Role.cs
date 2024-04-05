namespace Warehouse.Roles;

public static class Role
{
    public static UserRole CurrentUserRole { get; set; }

    public enum UserRole
    {
        Coordinator,
        Employee
    }
}
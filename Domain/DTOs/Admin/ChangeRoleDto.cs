using Domain.Enums.Roles;

namespace Domain.DTOs.Admin;

public class ChangeRoleDto
{
    public required Guid RoleId { get; set; }
    public RoleEnum Role { get; set; }
}
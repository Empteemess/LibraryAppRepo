using System.ComponentModel.DataAnnotations;
using Domain.Enums.Roles;
using Domain.Helper;

namespace Domain.Entities;

public class User : BaseEntity
{
    [MaxLength(15)]
    public string? FirstName { get; set; }
    [MaxLength(15)]
    public string? LastName { get; set; }

    [MaxLength(50)]
    public string? Email { get; set; }

    [MaxLength(150)] public string? Password { get; set; }

    public RoleEnum? Role { get; set; }

    public ICollection<BookUser>? BookUsers { get; set; }
}
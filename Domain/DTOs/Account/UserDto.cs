using System.ComponentModel.DataAnnotations;
using Domain.Enums.Roles;

namespace Domain.DTOs.Account;

public class UserDto
{
    [MaxLength(20)]
    public string? FirstName { get; set; }
    [MaxLength(20)]
    public string? LastName { get; set; }

    [MaxLength(40)]
    [Required]
    public string? Email { get; set; }
    [MaxLength(40)]
    [Required]
    public string? Password { get; set; }

    public RoleEnum? Role { get; set; }
}
using System.ComponentModel.DataAnnotations;
using Domain.DTOs.BookDtos;
using Domain.Enums.Roles;

namespace Domain.DTOs.UserDtos;

public class ResponseUserDto
{
    public Guid Id { get; set; }
    [MaxLength(20)]
    public string? FirstName { get; set; }
    [MaxLength(20)]
    public string? LastName { get; set; }

    [MaxLength(40)]
    [Required]
    public string? Email { get; set; }

    public RoleEnum? Role { get; set; }

    public IEnumerable<BookDto>? Books { get; set; }
}
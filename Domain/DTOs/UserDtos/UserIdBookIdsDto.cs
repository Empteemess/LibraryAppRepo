namespace Domain.DTOs.UserDtos;

public class UserIdBookIdsDto
{
    public required Guid UserId { get; set; }
    public required IEnumerable<Guid> BookIds { get; set; }
}
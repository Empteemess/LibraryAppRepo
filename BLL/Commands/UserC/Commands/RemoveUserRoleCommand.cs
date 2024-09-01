using MediatR;

namespace BLL.Commands.UserC.Commands;

public record RemoveUserRoleCommand(Guid UserId) : IRequest;

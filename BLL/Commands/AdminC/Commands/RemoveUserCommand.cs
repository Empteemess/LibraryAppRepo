using Domain.DTOs.UserDtos;
using MediatR;

namespace BLL.Commands.Admin.Commands;

public record RemoveUserCommand(UserIdsDto Ids) : IRequest;
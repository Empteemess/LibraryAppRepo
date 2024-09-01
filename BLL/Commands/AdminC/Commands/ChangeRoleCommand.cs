using Domain.DTOs.Admin;
using MediatR;

namespace BLL.Commands.Admin.Commands;

public record ChangeRoleCommand(ChangeRoleDto ChangeRoleDto) : IRequest ;

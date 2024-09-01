using Domain.DTOs.Account;
using Domain.Entities;
using MediatR;

namespace BLL.Commands.Admin.Commands;

public record AddUserCommand(UserDto User) : IRequest<User>;
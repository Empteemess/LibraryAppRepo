using Domain.DTOs.UserDtos;
using MediatR;

namespace BLL.Commands.UserC.Commands;

public record ReturnBookCommand(UserIdBookIdsDto UserIdBookIdsDto) : IRequest;

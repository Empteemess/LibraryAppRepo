using Domain.DTOs.UserDtos;
using MediatR;

namespace BLL.Commands.UserC.Commands;

public record BorrowBookCommand(UserIdBookIdsDto UserIdBookIdsDto) : IRequest;
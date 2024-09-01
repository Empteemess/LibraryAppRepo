using Domain.DTOs.AuthDtos;
using MediatR;

namespace BLL.Commands.AuthorC.Commands;

public record UpdateAuthorCommand(UpdateAuthorDto UpdateAuthorDto) : IRequest;

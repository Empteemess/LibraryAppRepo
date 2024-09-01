using Domain.DTOs;
using Domain.DTOs.AuthDtos;
using MediatR;

namespace BLL.Commands.AuthorC.Commands;

public record RemoveAuthorByIdCommand(AuthorIdsDto AuthorIds):IRequest;
using Domain.DTOs.BookDtos;
using MediatR;

namespace BLL.Commands.BookC.Commands;

public record AddBookWithAuthorsCommand(AddBookWithAuthorsDto AddBookWithAuthorsDto) : IRequest;
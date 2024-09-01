using Domain.DTOs.BookDtos;
using MediatR;

namespace BLL.Commands.BookC.Commands;

public record UpdateBookCommand(UpdateBookDto UpdateBookDto) : IRequest;
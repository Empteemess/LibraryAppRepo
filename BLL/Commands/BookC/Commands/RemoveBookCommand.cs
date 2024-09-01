using Domain.DTOs.AuthDtos;
using Domain.DTOs.BookDtos;
using MediatR;

namespace BLL.Commands.BookC.Commands;

public record RemoveBookCommand(BookIds BookIds):IRequest;
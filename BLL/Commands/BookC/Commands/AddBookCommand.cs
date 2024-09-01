using Domain.DTOs.BookDtos;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace BLL.Commands.BookC.Commands;

public record AddBookCommand(BookDto BookDto):IRequest;

using MediatR;
using Microsoft.AspNetCore.Http;

namespace BLL.Commands.ImageC.Commands;

public record AddImageInBookCommand(IFormFile FormFile,Guid BookId) : IRequest; 
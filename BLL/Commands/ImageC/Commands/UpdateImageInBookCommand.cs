using MediatR;
using Microsoft.AspNetCore.Http;

namespace BLL.Commands.ImageC.Commands;

public record UpdateImageInBookCommand(IFormFile FormFile,Guid BookId):IRequest;

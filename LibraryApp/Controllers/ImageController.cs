using BLL.Commands.ImageC.Commands;
using Domain.Enums.Roles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = nameof(RoleEnum.Manager))]
public class ImageController : ControllerBase
{
    private const string ErrorMessage = "Wong Model";
    private readonly IMediator _mediator;

    public ImageController(IMediator mediator)
    {
        _mediator = mediator;
    }    
    
    [HttpPut]
    public async Task<IActionResult> UpdateImg([FromForm] IFormFile formFile, Guid bookId)
    {
        if (!ModelState.IsValid) return BadRequest(ErrorMessage);

        try
        {
            var command = new UpdateImageInBookCommand(formFile, bookId);
            await _mediator.Send(command);

            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddImgToBook([FromForm] IFormFile formFile, Guid bookId)
    {
        if (!ModelState.IsValid) return BadRequest(ErrorMessage);

        try
        {
            var command = new AddImageInBookCommand(formFile, bookId);
            await _mediator.Send(command);

            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }    
    
}
using BLL.Commands.Admin.Commands;
using BLL.Commands.UserC.Commands;
using BLL.Queries.UserQ.Query;
using Domain.CustomException;
using Domain.DTOs.UserDtos;
using Domain.Enums.Roles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = nameof(RoleEnum.User))]
public class UserController : ControllerBase
{
    private const string ErrorMessage = "Wrong Model.";
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("ret")]
    public async Task<IActionResult> ReturnBook([FromBody] UserIdBookIdsDto userIdBookIdsDto)
    {
        try
        {
            if (!ModelState.IsValid) return BadRequest(ErrorMessage);
            var command = new ReturnBookCommand(userIdBookIdsDto);
            await _mediator.Send(command);
            return Ok();
        }
        catch (LibraryException e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("borr")]
    public async Task<IActionResult> BorrowBook([FromBody] UserIdBookIdsDto userIdBookIdsDto)
    {
        try
        {
            if (!ModelState.IsValid) return BadRequest(ErrorMessage);
            var command = new BorrowBookCommand(userIdBookIdsDto);
            await _mediator.Send(command);
            return Ok();
        }
        catch (LibraryException e)
        {
            return BadRequest(e.Message);
        }
    }
    
}
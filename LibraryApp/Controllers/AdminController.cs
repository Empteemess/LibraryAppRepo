using BLL.Commands.Admin.Commands;
using BLL.Commands.UserC.Commands;
using BLL.Queries.UserQ.Query;
using Domain.CustomException;
using Domain.DTOs.Admin;
using Domain.Enums.Roles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = nameof(RoleEnum.Admin))]
public class AdminController : ControllerBase
{
    private readonly IMediator _mediator;

    public AdminController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut]
    public async Task<IActionResult> UpdateRole(ChangeRoleDto changeRoleDto)
    {
        try
        {
            if (Enum.IsDefined(typeof(RoleEnum), changeRoleDto.Role)) return BadRequest();
            
            var command = new ChangeRoleCommand(changeRoleDto);
            await _mediator.Send(command);
            return Ok();
        }
        catch (LibraryException e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpDelete]
    public async Task<IActionResult> RemoveRole(Guid userId)
    {
        try
        {
            var query = new RemoveUserRoleCommand(userId);
            await _mediator.Send(query);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetUserById(Guid userId)
    {
        try
        {
            var query = new GetUserbyIdQuery(userId);
            var user = await _mediator.Send(query);
            return Ok(user);
        }
        catch (LibraryException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllUser()
    {
        var users = await _mediator.Send(new GetAllUserQuery());
        return Ok(users);
    }
}
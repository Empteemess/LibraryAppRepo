using BLL.Commands.Admin.Commands;
using BLL.Commands.UserC.Commands;
using BLL.Queries.TokenQ.Query;
using BLL.Queries.UserQ.Query;
using Domain.DTOs.Account;
using Domain.DTOs.UserDtos;
using Domain.Enums.Roles;
using Domain.Helper;
using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;
    private const string ErrorMessage = "Wong Model";

    public AccountController(IMediator mediator,IUnitOfWork unitOfWork)
    {
        _mediator = mediator;
        _unitOfWork = unitOfWork;
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] UserDto userDto)
    {
        if (!ModelState.IsValid) return BadRequest(ErrorMessage);

        var checkCommand = new CheckUserByEmailQuery(userDto.Email!);
        var checkResult = await _mediator.Send(checkCommand);

        if (checkResult) return BadRequest("Already exists.");

        userDto.Role = RoleEnum.User;
        
        var userCommand = new AddUserCommand(userDto);
        var user =await _mediator.Send(userCommand);

        var tokenQuery = new GetTokenQuery(user.Id);
        var token = await _mediator.Send(tokenQuery);

        return Ok(new ReturnUserDto()
        {
            UserName = userDto.FirstName!,
            Email = userDto.Email!,
            Token = token
        });
    }

    [HttpGet]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        var user = await _unitOfWork.UserRepository.GetUserByEmail(loginDto.Email);

        var check = PasswordHashHelper.VerifyPasswordAsync(loginDto.Password, user.Password!);

        if (!check) return BadRequest("Doesn't Exists.");

        var tokenQuery = new GetTokenQuery(user.Id);
        var token = await _mediator.Send(tokenQuery);

        return Ok(new ReturnUserDto
        {
            Email = loginDto.Email,
            UserName = user.FirstName!,
            Token = token
        });
    }
    [HttpDelete]
    public async Task<IActionResult> RemoveAccount([FromBody] UserIdsDto userIds)
    {
        try
        {
            var command = new RemoveUserCommand(userIds);
            await _mediator.Send(command);
            return Ok("Removed Successfully");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
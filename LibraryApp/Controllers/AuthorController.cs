using BLL.Commands.AuthorC.Commands;
using BLL.Queries.AuthQ.Queries;
using Domain.CustomException;
using Domain.DTOs.AuthDtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorController : ControllerBase
{
    private const string ErrorMessage = "Wrong Model";
    private readonly IMediator _mediator;

    public AuthorController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{authorId:guid}")]
    public async Task<IActionResult> GetById(Guid authorId)
    {
        try
        {
            var query = new GetAuthorByIdQuery(authorId);
            var author = await _mediator.Send(query);
            return Ok(author);
        }
        catch (LibraryException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var authors = new GetAllAuthorsQuery();
            var result = await _mediator.Send(authors);

            return Ok(result);
        }
        catch (LibraryException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [Authorize(Roles = "AdminC,Manager")]
    public async Task<IActionResult> AddAuthor([FromBody] AddAuthorDto addAuthorDto)
    {
        try
        {
            if (!ModelState.IsValid) return BadRequest(ErrorMessage);

            var command = new AddAuthorCommand(addAuthorDto);
            await _mediator.Send(command);

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    
    [HttpPost("book")]
    [Authorize(Roles = "AdminC,Manager")]
    public async Task<IActionResult> AddBookInAuthor([FromBody] AuthorIdBookIds authorIdBookIds)
    {
        try
        {
            if (!ModelState.IsValid) return BadRequest(ErrorMessage);

            var command = new AddBookInAuthorCommand(authorIdBookIds);
            await _mediator.Send(command);

            return Ok();
        }
        catch (LibraryException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete]
    [Authorize(Roles = "AdminC,Manager")]
    public async Task<IActionResult> RemoveAuthor([FromBody] AuthorIdsDto authorIds)
    {
        try
        {
            var command = new RemoveAuthorByIdCommand(authorIds);
            await _mediator.Send(command);
            return Ok();
        }
        catch (LibraryException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    [Authorize(Roles = "AdminC,Manager")]
    public async Task<IActionResult> Update([FromBody] UpdateAuthorDto updateAuthorDto)
    {
        try
        {
            var command = new UpdateAuthorCommand(updateAuthorDto);
            await _mediator.Send(command);
            return Ok();
        }
        catch (LibraryException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
using Azure.Core;
using BLL.Commands.BookC.Commands;
using BLL.Commands.ImageC.Commands;
using BLL.Queries.BookQ.Queries;
using Domain.CustomException;
using Domain.DTOs.BookDtos;
using Domain.Enums.Roles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
    private const string ErrorMessage = "Wong Model";
    private readonly IMediator _mediator;

    public BookController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("author")]
    [Authorize(Roles = "AdminC,Manager")]
    public async Task<IActionResult> AddWithAuth([FromBody] AddBookWithAuthorsDto addBookWithAuthorsDto)
    {
        try
        {
            if (!ModelState.IsValid) return BadRequest(ErrorMessage);

            var command = new AddBookWithAuthorsCommand(addBookWithAuthorsDto);
            await _mediator.Send(command);

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    [Authorize(Roles = "AdminC,Manager")]
    public async Task<IActionResult> Update([FromBody] UpdateBookDto updateBookDto)
    {
        try
        {
            var command = new UpdateBookCommand(updateBookDto);
            await _mediator.Send(command);
            return Ok();
        }
        catch (LibraryException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete]
    [Authorize(Roles = nameof(RoleEnum.Manager))]
    public async Task<IActionResult> Remove([FromBody] BookIds bookIds)
    {
        try
        {
            var command = new RemoveBookCommand(bookIds);
            await _mediator.Send(command);
            return Ok();
        }
        catch (LibraryException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [Authorize(Roles = nameof(RoleEnum.Manager))]
    public async Task<IActionResult> Add([FromBody] BookDto bookDto)
    {
        if (!ModelState.IsValid) return BadRequest(ErrorMessage);

        try
        {
            var command = new AddBookCommand(bookDto);
            await _mediator.Send(command);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{bookId:guid}")]
    public async Task<IActionResult> GetById(Guid bookId)
    {
        try
        {
            var query = new GetBookByIdQuery(bookId);
            var book = await _mediator.Send(query);

            return Ok(book);
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
            var query = new GetAllBooksQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
using Domain.DTOs;
using Domain.DTOs.AuthDtos;
using Domain.Entities;

namespace Domain.Mapper;

public static class AuthorBookMapper
{
    public static AuthorBookDto ToAuthorBookDto(this AuthorBook authorBook)
    {
        var authorBookDto = new AuthorBookDto
        {
            AuthorId = authorBook.AuthorId,
            BookId = authorBook.BookId
        };

        return authorBookDto;
    }
    
    public static AuthorBook ToAuthorBook(this AuthorBookDto authorBook)
    {
        var authorBookDto = new AuthorBook
        {
            AuthorId = authorBook.AuthorId,
            BookId = authorBook.BookId
        };

        return authorBookDto;
    }
}
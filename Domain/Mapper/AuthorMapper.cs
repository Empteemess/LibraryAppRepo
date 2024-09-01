using Domain.DTOs;
using Domain.DTOs.AuthDtos;
using Domain.DTOs.BookDtos;
using Domain.Entities;

namespace Domain.Mapper;

public static class AuthorMapper
{
    public static AddAuthorDto ToAuthorDto(this Author author)
    {
        var authorDto = new AddAuthorDto
        {
            FirstName = author.FirstName,
            LastName = author.LastName,
            DateOfBirth = author.DateOfBirth
        };
        return authorDto;
    }
    
    public static Author ToAuthor(this AddAuthorDto addAuthor)
    {
        var authorMapped = new Author
        {
            FirstName = addAuthor.FirstName,
            LastName = addAuthor.LastName,
            DateOfBirth = addAuthor.DateOfBirth
        };
        return authorMapped;
    }

    public static Author ToAuthor(this UpdateAuthorDto updateAuthorDto)
    {
        var mappedAuthor = new Author
        {
            Id = updateAuthorDto.Id,
            FirstName = updateAuthorDto.FirstName,
            LastName = updateAuthorDto.LastName,
            DateOfBirth = updateAuthorDto.DateOfBirth
        };
        return mappedAuthor;
    }
    
  
}
using Domain.DTOs.BookDtos;
using Domain.Entities;

namespace Domain.Mapper;

public static class BookMapper
{
    public static BookDto ToBookDto(this Book book)
    {
        var bookDto = new BookDto
        {
            Title = book.Title,
            Description = book.Descrption,
            TotalCount = book.TotalCount,
            CurrentCount = book.CurrentCount,
            Image = book.Image

        };
        return bookDto;
    }
    
    public static Book ToBook(this BookDto book)
    {
        var bookMapped = new Book
        {
            Title = book.Title!,
            Descrption = book.Description!,
            TotalCount = book.TotalCount,
            CurrentCount = book.CurrentCount,
            Image = book.Image
        };
        return bookMapped;
    }

    public static Book ToBook(this UpdateBookDto updateBook)
    {
        var bookMapped = new Book
        {
            Id = updateBook.BookId,
            Title = updateBook.Title!,
            Descrption = updateBook.Descrption!,
            TotalCount = updateBook.TotalCount,
            CurrentCount = updateBook.CurrentCount,
            Image = updateBook.Image
        };
        return bookMapped;
    }
}
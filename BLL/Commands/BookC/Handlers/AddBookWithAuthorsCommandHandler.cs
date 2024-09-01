using BLL.Commands.BookC.Commands;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Mapper;
using MediatR;

namespace BLL.Commands.BookC.Handlers;

public class AddBookWithAuthorsCommandHandler : IRequestHandler<AddBookWithAuthorsCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public AddBookWithAuthorsCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(AddBookWithAuthorsCommand request, CancellationToken cancellationToken)
    {
        var book = request.AddBookWithAuthorsDto.BookDto.ToBook();

        await _unitOfWork.BookRepository.AddAsync(book);
        await _unitOfWork.SaveAsync();

        foreach (var auth in request.AddBookWithAuthorsDto.AddAuthorDtos!)
        {
            var author = auth.ToAuthor();
            await _unitOfWork.AuthorRepository.AddAsync(author);
            await _unitOfWork.SaveAsync();

            var bookAuthor = new AuthorBook()
            {
                AuthorId = author.Id,
                BookId = book.Id
            };

            await _unitOfWork.AuthorBookRepository.AddAsync(bookAuthor);
            await _unitOfWork.SaveAsync();
        }

        return Unit.Value;
    }
}
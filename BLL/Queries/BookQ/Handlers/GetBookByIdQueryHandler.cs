using BLL.Queries.BookQ.Queries;
using Domain.CustomException;
using Domain.DTOs.BookDtos;
using Domain.Helper;
using Domain.Interfaces;
using MediatR;

namespace BLL.Queries.BookQ.Handlers;

public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, ResponseBookDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetBookByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseBookDto> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        await CheckHelper.Check(request.Id, _unitOfWork.BookRepository);
        
        var book = await _unitOfWork.BookRepository.GetByIdAsync(request.Id);

        if (book.AuthorBooks is not null)
        {
            var result = new ResponseBookDto
            {
                Id = book.Id,
                Title = book.Title,
                Descrption = book.Descrption,
                CurrentCount = book.CurrentCount,
                TotalCount = book.TotalCount,
                Image = book.Image,
                Authors = book.AuthorBooks.Select(x => new AllBookAuthorDto
                {
                    Id = x.Author!.Id,
                    FirstName = x.Author.FirstName,
                    LastName = x.Author.LastName,
                    DateOfBirth = x.Author.DateOfBirth
                })
            };

            return result;
        }
        else
        {
            throw new LibraryException();
        }
        
    }
}
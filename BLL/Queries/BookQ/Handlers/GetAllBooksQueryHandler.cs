using BLL.Queries.BookQ.Queries;
using Domain.DTOs.BookDtos;
using Domain.Interfaces;
using MediatR;

namespace BLL.Queries.BookQ.Handlers;

public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery,IEnumerable<ResponseBookDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllBooksQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<IEnumerable<ResponseBookDto>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
    {
        var book = await _unitOfWork.BookRepository.GetAllAsync();

        var result = book.Select(x =>
        
            new ResponseBookDto
            {
                Id = x.Id,
                Title = x.Title,
                Descrption = x.Descrption,
                TotalCount = x.TotalCount,
                CurrentCount = x.CurrentCount,
                Image = x.Image,
                Authors = x.AuthorBooks!.Select(au => new AllBookAuthorDto
                {
                    Id = au.AuthorId,
                    FirstName = au.Author!.FirstName,
                    LastName = au.Author.LastName,
                    DateOfBirth = au.Author.DateOfBirth
                })
            }
        );

        return result;
    }
}
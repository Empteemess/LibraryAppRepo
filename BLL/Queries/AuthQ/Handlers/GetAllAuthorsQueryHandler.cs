using BLL.Queries.AuthQ.Queries;
using Domain.DTOs.AuthDtos;
using Domain.DTOs.BookDtos;
using Domain.Interfaces;
using MediatR;

namespace BLL.Queries.AuthQ.Handlers;

public class GetAllAuthorsQueryHandler : IRequestHandler<GetAllAuthorsQuery,IEnumerable<AuthorsWithBookIdsDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllAuthorsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<IEnumerable<AuthorsWithBookIdsDto>> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
    {
        var author = await _unitOfWork.AuthorRepository.GetAllAsync();
        
        
        var result = author.Select(x => new AuthorsWithBookIdsDto()
        {
            Id = x.Id,
            FirstName = x.FirstName,
            LastName = x.LastName,
            DateOfBirth = x.DateOfBirth,
            Books = x.AuthorBooks!.Select(bn => new BookNamesDto()
            {
                Id = bn.Book!.Id,
                Title = bn.Book.Title,
            })
            
        });

        return result;
    }
}
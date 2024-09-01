using BLL.Queries.AuthQ.Queries;
using Domain.CustomException;
using Domain.DTOs.AuthDtos;
using Domain.DTOs.BookDtos;
using Domain.Helper;
using Domain.Interfaces;
using MediatR;

namespace BLL.Queries.AuthQ.Handlers;

public class GetAuthorByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery,AuthorsWithBookIdsDto>
{
    private const string ErrorMessage = "Wrong Model.";
    private readonly IUnitOfWork _unitOfWork;

    public GetAuthorByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<AuthorsWithBookIdsDto> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
    {
        await CheckHelper.Check(request.AuthorId, _unitOfWork.AuthorRepository);
        
        var author = await _unitOfWork.AuthorRepository.GetByIdAsync(request.AuthorId);

        if (author.AuthorBooks is null) throw new LibraryException(ErrorMessage);
        
        var result = new AuthorsWithBookIdsDto()
        {
            Id = author.Id,
            FirstName = author.FirstName,
            LastName = author.LastName,
            DateOfBirth = author.DateOfBirth,
            Books = author.AuthorBooks!.Select(bn => new BookNamesDto()
            {
                Id = bn.Book!.Id,
                Title = bn.Book.Title
            })
        };

        return result;
    }
}
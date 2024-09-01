using BLL.Queries.UserQ.Query;
using Domain.DTOs.BookDtos;
using Domain.DTOs.UserDtos;
using Domain.Interfaces;
using MediatR;

namespace BLL.Queries.UserQ.Handlers;

public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQuery,IEnumerable<ResponseUserDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllUserQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<IEnumerable<ResponseUserDto>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
    {
        var users =  await _unitOfWork.UserRepository.GetAllAsync();

        var result = users.Select(x => 
            new ResponseUserDto
        {
            Id = x.Id,
            FirstName = x.FirstName,
            LastName = x.LastName,
            Email = x.Email,
            Role = x.Role,
            Books = x.BookUsers!.Select(b => new BookDto()
            {
                Id = b.Book!.Id,
                Title = b.Book!.Title,
                Description = b.Book.Descrption,
                TotalCount = b.Book.TotalCount,
                CurrentCount = b.Book.CurrentCount,
                Image = b.Book.Image
            })
        });

        return result;
    }
}
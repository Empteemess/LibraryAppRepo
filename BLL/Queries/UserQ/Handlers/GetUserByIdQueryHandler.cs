using BLL.Queries.UserQ.Query;
using Domain.DTOs.BookDtos;
using Domain.DTOs.UserDtos;
using Domain.Helper;
using Domain.Interfaces;
using MediatR;

namespace BLL.Queries.UserQ.Handlers;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserbyIdQuery, ResponseUserDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetUserByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseUserDto> Handle(GetUserbyIdQuery request, CancellationToken cancellationToken)
    {
        await CheckHelper.Check(request.UserId, _unitOfWork.UserRepository);

        var users = await _unitOfWork.UserRepository.GetByIdAsync(request.UserId);

        var result =
            new ResponseUserDto
            {
                Id = users.Id,
                FirstName = users.FirstName,
                LastName = users.LastName,
                Email = users.Email,
                Role = users.Role,
                Books = users.BookUsers!.Select(b => new BookDto()
                {
                    Title = b.Book!.Title,
                    Description = b.Book.Descrption,
                    TotalCount = b.Book.TotalCount,
                    CurrentCount = b.Book.CurrentCount,
                    Image = b.Book.Image
                })
            };
        return result;
    }
}
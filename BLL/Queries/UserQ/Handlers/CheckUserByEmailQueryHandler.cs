using BLL.Queries.UserQ.Query;
using Domain.Interfaces;
using MediatR;

namespace BLL.Queries.UserQ.Handlers;

public class CheckUserByEmailQueryHandler : IRequestHandler<CheckUserByEmailQuery,bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public CheckUserByEmailQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<bool> Handle(CheckUserByEmailQuery request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.GetAllAsync();

        var check = user.Any(x => x.Email == request.UserEmail);

        return check;
    }
}
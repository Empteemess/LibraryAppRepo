using BLL.Commands.Admin.Commands;
using BLL.Commands.UserC.Commands;
using Domain.Helper;
using Domain.Interfaces;
using MediatR;

namespace BLL.Commands.UserC.Handlers;

public class RemoveUserCommandHandler : IRequestHandler<RemoveUserCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public RemoveUserCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
    {
        foreach (var userId in request.Ids.UserIds!)
        {
            await CheckHelper.Check(userId, _unitOfWork.UserRepository);

            await _unitOfWork.UserRepository.RemoveByIdAsync(userId);
            await _unitOfWork.SaveAsync();
        }

        return Unit.Value;
    }
}
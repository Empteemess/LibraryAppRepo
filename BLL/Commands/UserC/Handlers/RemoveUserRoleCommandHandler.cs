using BLL.Commands.UserC.Commands;
using Domain.Enums.Roles;
using Domain.Interfaces;
using MediatR;

namespace BLL.Commands.UserC.Handlers;

public class RemoveUserRoleCommandHandler : IRequestHandler<RemoveUserRoleCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public RemoveUserRoleCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(RemoveUserRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.GetByIdAsync(request.UserId);

        user.Role = RoleEnum.None;

        await _unitOfWork.UserRepository.UpdateAsync(user);
        await _unitOfWork.SaveAsync();

        return Unit.Value;
    }
}
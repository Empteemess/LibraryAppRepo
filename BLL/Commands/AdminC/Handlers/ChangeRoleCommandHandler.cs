using BLL.Commands.Admin.Commands;
using Domain.CustomException;
using Domain.Interfaces;
using MediatR;

namespace BLL.Commands.Admin.Handlers;

public class ChangeRoleCommandHandler : IRequestHandler<ChangeRoleCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public ChangeRoleCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(ChangeRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.GetByIdAsync(request.ChangeRoleDto.RoleId);

        if (user.Email is null) throw new LibraryException();

        user.Role = request.ChangeRoleDto.Role;

        await _unitOfWork.UserRepository.UpdateAsync(user);
        await _unitOfWork.SaveAsync();

        return Unit.Value;
    }
}
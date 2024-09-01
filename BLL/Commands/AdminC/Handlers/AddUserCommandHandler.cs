using BLL.Commands.Admin.Commands;
using BLL.Commands.UserC.Commands;
using Domain.Entities;
using Domain.Enums.Roles;
using Domain.Helper;
using Domain.Interfaces;
using Domain.Mapper;
using MediatR;

namespace BLL.Commands.UserC.Handlers;

public class AddUserCommandHandler : IRequestHandler<AddUserCommand, User>
{
    private readonly IUnitOfWork _unitOfWork;

    public AddUserCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<User> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        var user = request.User.ToUser();

        user.Role = RoleEnum.User;
        user.Password = user.Password!.HashPassword();

        await _unitOfWork.UserRepository.AddAsync(user);
        await _unitOfWork.SaveAsync();

        return user;
    }
}
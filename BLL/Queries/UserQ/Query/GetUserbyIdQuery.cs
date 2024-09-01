using Domain.DTOs.UserDtos;
using MediatR;

namespace BLL.Queries.UserQ.Query;

public record GetUserbyIdQuery(Guid UserId) : IRequest<ResponseUserDto>;
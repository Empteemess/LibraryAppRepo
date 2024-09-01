using Domain.DTOs;
using Domain.DTOs.UserDtos;
using Domain.Entities;
using MediatR;

namespace BLL.Queries.UserQ.Query;

public record GetAllUserQuery() : IRequest<IEnumerable<ResponseUserDto>>;
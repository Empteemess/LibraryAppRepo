using Domain.DTOs.Account;
using Domain.Entities;
using MediatR;

namespace BLL.Queries.TokenQ.Query;

public record GetTokenQuery(Guid UserId) : IRequest<string>;
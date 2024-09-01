using MediatR;

namespace BLL.Queries.UserQ.Query;

public record CheckUserByEmailQuery(string UserEmail):IRequest<bool>;
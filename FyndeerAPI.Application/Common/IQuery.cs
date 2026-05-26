using MediatR;

namespace FyndeerAPI.Application.Common;

/// <summary>Query that reads state and returns a value.</summary>
public interface IQuery<T> : IRequest<ServiceResult<T>> { }

/// <summary>Handler for a query.</summary>
public interface IQueryHandler<TQuery, T> : IRequestHandler<TQuery, ServiceResult<T>>
    where TQuery : IQuery<T> { }

using MediatR;

namespace FyndeerAPI.Application.Common;

/// <summary>Command that mutates state and returns no data.</summary>
public interface ICommand : IRequest<ServiceResult> { }

/// <summary>Command that mutates state and returns a value.</summary>
public interface ICommand<T> : IRequest<ServiceResult<T>> { }

/// <summary>Handler for a command with no return data.</summary>
public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, ServiceResult>
    where TCommand : ICommand { }

/// <summary>Handler for a command that returns a value.</summary>
public interface ICommandHandler<TCommand, T> : IRequestHandler<TCommand, ServiceResult<T>>
    where TCommand : ICommand<T> { }

namespace Zip.InstallmentsService.Api.Common.Common;

public interface ICommandResult : IOperationResult
{
    CommandResultType ResultType { get; }
}

public interface ICommandResult<T> : ICommandResult, IOperationResult<T>
{
}
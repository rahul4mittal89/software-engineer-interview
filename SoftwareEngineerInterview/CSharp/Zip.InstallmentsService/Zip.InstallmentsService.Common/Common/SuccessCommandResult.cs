namespace Zip.InstallmentsService.Api.Common.Common;

public class SuccessCommandResult : OperationResult, ICommandResult
{
    public CommandResultType ResultType => CommandResultType.Success;
}

public class SuccessCommandResult<T> : OperationResult<T>, ICommandResult<T>
{
    public CommandResultType ResultType => CommandResultType.Success;
}
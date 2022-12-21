using System.Collections.Generic;

namespace Zip.InstallmentsService.Api.Common.Common;

public interface IOperationResult
{
    bool Success { get; }
    IReadOnlyCollection<IOperationError> Errors { get; }
}

public interface IOperationResult<out T> : IOperationResult
{
    T Result { get; }
}
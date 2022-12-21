using System;
using System.Collections.Generic;
using System.Linq;

namespace Zip.InstallmentsService.Api.Common.Common;

public class OperationResult : IOperationResult
{
    private readonly List<IOperationError> _errors;

    public OperationResult()
    {
        _errors = new List<IOperationError>();
    }

    public bool Success => !_errors.Any();

    public IReadOnlyCollection<IOperationError> Errors => _errors.ToList();

    public void AddError(IOperationError error)
    {
        if (error == null) throw new ArgumentNullException(nameof(error));

        _errors.Add(error);
    }

    public void AddErrors(IEnumerable<IOperationError> errors)
    {
        if (errors == null) throw new ArgumentNullException(nameof(errors));

        var operationErrors = errors.ToList();
        if (operationErrors.Any(e => e == null))
            throw new ArgumentException($"{nameof(errors)} contains errors which are null", nameof(errors));

        _errors.AddRange(operationErrors);
    }
}

public class OperationResult<T> : OperationResult, IOperationResult<T>
{
    public T Result { get; private set; }

    public void SetResult(T result)
    {
        if (result == null) throw new ArgumentNullException(nameof(result));

        Result = result;
    }
}
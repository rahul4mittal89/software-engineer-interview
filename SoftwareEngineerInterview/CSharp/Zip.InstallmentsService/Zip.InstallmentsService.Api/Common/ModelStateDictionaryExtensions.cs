using Microsoft.AspNetCore.Mvc.ModelBinding;
using Zip.InstallmentsService.Api.Common.Common;

namespace Zip.InstallmentsService.Api.Common;

/// <summary>
/// Extension Class for Model/Request Validation
/// </summary>
public static class ModelStateDictionaryExtensions
{
    /// <summary>
    /// Add Validation Errors to Model
    /// </summary>
    /// <param name="modelState"></param>
    /// <param name="operationErrors"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public static void AddModelErrors(this ModelStateDictionary modelState,
        IEnumerable<IOperationError> operationErrors)
    {
        if (modelState == null) return;

        if (operationErrors == null) throw new ArgumentNullException(nameof(operationErrors));

        foreach (var error in operationErrors) modelState.AddModelError(error.ErrorKey, error.ErrorMessage);
    }
}
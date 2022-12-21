using System;

namespace Zip.InstallmentsService.Api.Domain.Core.SharedKernel.Interfaces;

public interface IUpdateAuditable
{
    DateTime LastUpdated { get; }
}
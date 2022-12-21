using System;

namespace Zip.InstallmentsService.Api.Domain.Core.SharedKernel.Interfaces;

public interface ICreationAuditable
{
    DateTime CreationTime { get; }
}
namespace Zip.InstallmentsService.Api.Domain.Core.SharedKernel;

public abstract class StatefulObject
{
    public ObjectState State { get; protected set; }

    protected virtual void SetStateToUpdated()
    {
        if (State != ObjectState.Created) State = ObjectState.Updated;
    }
}
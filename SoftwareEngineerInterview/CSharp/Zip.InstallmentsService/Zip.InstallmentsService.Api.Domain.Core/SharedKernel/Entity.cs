using System;

namespace Zip.InstallmentsService.Api.Domain.Core.SharedKernel;

public abstract class Entity : StatefulObject
{
    private Guid _id;

    protected Entity()
    {
        _id = Guid.NewGuid();
        State = ObjectState.Created;
    }

    public Guid Id
    {
        get => _id;
        protected set
        {
            if (value == Guid.Empty)
                throw new ArgumentException($"{nameof(Id)} cannot be set to an empty guid", nameof(Id));

            _id = value;
        }
    }

    public override bool Equals(object obj)
    {
        var other = obj as Entity;

        if (ReferenceEquals(other, null))
            return false;

        if (ReferenceEquals(this, other))
            return true;

        if (GetType() != other.GetType())
            return false;

        if (Id == Guid.Empty || other.Id == Guid.Empty)
            return false;

        return Id == other.Id;
    }

    public static bool operator ==(Entity a, Entity b)
    {
        if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
            return true;

        if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
            return false;

        return a.Equals(b);
    }

    public static bool operator !=(Entity a, Entity b)
    {
        return !(a == b);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
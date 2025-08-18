namespace Domain.Core.Shared;

public abstract class Entity<TId> where TId : notnull
{
    protected Entity(TId id)
    {
        Id = id;
    }

    protected Entity()
    {
    } // For EF Core

    public TId Id { get; protected set; }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (obj is not Entity<TId> other) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id.Equals(other.Id);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public static bool operator ==(Entity<TId>? left, Entity<TId>? right)
    {
        return EqualOperator(left, right);
    }

    public static bool operator !=(Entity<TId>? left, Entity<TId>? right)
    {
        return NotEqualOperator(left, right);
    }

    protected static bool EqualOperator(Entity<TId>? left, Entity<TId>? right)
    {
        if (left is null && right is null) return true;
        if (left is null || right is null) return false;
        return left.Equals(right);
    }

    protected static bool NotEqualOperator(Entity<TId>? left, Entity<TId>? right)
    {
        return !EqualOperator(left, right);
    }
}
using BuberDinner.Domain.Common.Models;

namespace BuberDinner.Domain.MenuAggregate.ValueObjects;

public sealed class MenuItemId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    private MenuItemId(Guid value)
    {
        Value = value;
    }

    public static MenuItemId Create(Guid id)
    {
        return new MenuItemId(id);
    }

    public static MenuItemId CreateUnique()
    {
        return new(Guid.NewGuid());
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
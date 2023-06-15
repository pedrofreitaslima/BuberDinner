using BuberDinner.Domain.Common.Models;

namespace BuberDinner.Domain.MenuAggregate.ValueObjects;

public sealed class MenuId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set;}

    private MenuId(Guid value)
    {
        Value = value;
    }

    public static MenuId Create(Guid id)
    {
        return new MenuId(id);
    }
    
    public static MenuId CreateUnique()
    {
        return new(Guid.NewGuid());
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
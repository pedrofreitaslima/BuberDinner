using BuberDinner.Domain.Common.Models;

namespace BuberDinner.Domain.MenuAggregate.ValueObjects;

public sealed class MenuSectionId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    private MenuSectionId(Guid value)
    {
        Value = value;
    }

    public static MenuSectionId Create(Guid id)
    {
        return new MenuSectionId(id);
    }

    public static MenuSectionId CreateUnique()
    {
        return new(Guid.NewGuid());
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
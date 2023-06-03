using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.Menu.ValueObjects;

namespace BuberDinner.Domain.Menu.Enitites;

public sealed class MenuSection : Entity<MenuSectionId>
{
    private readonly List<MenuItem> _items = new List<MenuItem>();
    public string Name { get; }
    public string Description { get; }

    public IReadOnlyList<MenuItem> Items => _items.AsReadOnly();

    private MenuSection(
        MenuSectionId menuSectionId,
        string name,
        string description)
        : base(menuSectionId)
    {
        Name = name;
        Description = description;
    }

    public static MenuSection Create(
        string name,
        string description)
    {
        return new(
            MenuSectionId.CreateUnique(),
            name,
            description);
    }
}
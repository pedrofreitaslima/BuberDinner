using BuberDinner.Application.Persistence;
using BuberDinner.Domain.MenuAggregate;

namespace BuberDinner.Infrastructure.Persistence;

public class MenuRepository : IMenuRepository
{
    private static readonly List<Menu> _menus = new List<Menu>();
    
    public void Add(Menu menu)
    {
        _menus.Add(menu);
    }
}
using BuberDinner.Domain.MenuAggregate;

namespace BuberDinner.Application.Persistence;

public interface IMenuRepository
{
    void Add(Menu menu);
}
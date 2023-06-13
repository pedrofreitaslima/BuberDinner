using BuberDinner.Domain.MenuAggregate;
using MediatR;
using ErrorOr;

namespace BuberDinner.Application.Menus.Commands;

public record CreateMenuCommand(
    string Name,
    string Description,
    List<MenuSectionCommand> Sections,
    string HostId) : IRequest<ErrorOr<Menu>>;
    
public record MenuSectionCommand(
    string Name, 
    string Description, 
    List<MenuItemCommand> Items);
    
public record MenuItemCommand(
    string Name,
    string Description);
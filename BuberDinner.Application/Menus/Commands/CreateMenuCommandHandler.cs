using BuberDinner.Application.Persistence;
using BuberDinner.Domain.HostAggregate.ValueObjects;
using BuberDinner.Domain.MenuAggregate;
using BuberDinner.Domain.MenuAggregate.Enitites;
using MediatR;
using ErrorOr;
using System.Linq;

namespace BuberDinner.Application.Menus.Commands;

public class CreateMenuCommandHandler :
    IRequestHandler<CreateMenuCommand, ErrorOr<Menu>>
{
    private readonly IMenuRepository _repository;

    public CreateMenuCommandHandler(IMenuRepository repository)
    {
        _repository = repository;
    }
    public async Task<ErrorOr<Menu>> Handle(CreateMenuCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        
       // create Menu
       var menu = Menu.Create(
           hostId: HostId.Create(request.HostId),
           name: request.Name,
           description: request.Description,
           sections: request.Sections.ConvertAll(section => MenuSection.Create(
               section.Name,
               section.Description,
               section.Items.ConvertAll(item => MenuItem.Create(
                   item.Name,
                   item.Description)))));
       
       // persist menu
       _repository.Add(menu);
       
       // return menu
       return menu;
    }
}
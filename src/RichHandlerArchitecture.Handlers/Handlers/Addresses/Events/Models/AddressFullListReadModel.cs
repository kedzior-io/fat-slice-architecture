using RichHandlerArchitecture.Domain.Customers.Events;

namespace RichHandlerArchitecture.Handlers.Handlers.Addresses.Events.Models;

public class AddressFullListReadModel(AddressCreatedEvent addressCreatedEvent) : AddressReadModel(addressCreatedEvent)
{
}
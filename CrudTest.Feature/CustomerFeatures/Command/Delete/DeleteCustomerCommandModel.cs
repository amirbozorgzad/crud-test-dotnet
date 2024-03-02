using MediatR;

namespace CrudTest.Feature.CustomerFeatures.Command.Delete;

public class DeleteCustomerCommandModel : IRequest<long>
{
    public long Id { get; set; }
}
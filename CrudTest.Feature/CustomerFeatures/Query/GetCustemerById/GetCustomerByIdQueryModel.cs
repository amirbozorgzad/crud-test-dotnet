using CrudTest.Core.Context.Model;
using MediatR;

namespace CrudTest.Feature.CustomerFeatures.Query.GetCustemerById;

public class GetCustomerByIdQueryModel : IRequest<Customer>
{
    public long Id { get; set; }
}
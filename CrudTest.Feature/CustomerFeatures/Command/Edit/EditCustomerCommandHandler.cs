using CrudTest.Core.Context.CustomerContext;
using CrudTest.Core.Context.Model;
using CrudTest.Feature.CustomerFeatures.Command.Add;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CrudTest.Feature.CustomerFeatures.Command.Edit;

public class EditCustomerCommandHandler : IRequestHandler<EditCustomerCommandModel, long>
{
    private readonly ICustomerContext _customerContext;
    private readonly IValidator<EditCustomerCommandModel> _editValidator;

    public EditCustomerCommandHandler(ICustomerContext customerContext, IValidator<EditCustomerCommandModel> editValidator)
    {
        _customerContext = customerContext;
        _editValidator = editValidator;
    }

    public async Task<long> Handle(EditCustomerCommandModel request, CancellationToken cancellationToken)
    {
        var validationResult = await _editValidator.ValidateAsync(request, cancellationToken);
        if (validationResult.IsValid)
        {
            Customer? customer =
                await _customerContext.Customer.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (customer == null) return default;

            customer.FirstName = request.FirstName;
            customer.LastName = request.LastName;
            customer.Email = request.Email;
            customer.PhoneNumber = request.PhoneNumber;
            customer.DateOfBirth = request.DateOfBirth;
            customer.BankAccountNumber = request.BankAccountNumber;

            _customerContext.Customer.Update(customer);
            await _customerContext.SaveChangesAsync();
            return customer.Id;
        }
        throw new ValidationException(validationResult.Errors);
    }
}
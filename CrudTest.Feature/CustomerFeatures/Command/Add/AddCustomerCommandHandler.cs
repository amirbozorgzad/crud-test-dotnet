using CrudTest.Core.Context.CustomerContext;
using CrudTest.Core.Context.Model;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CrudTest.Feature.CustomerFeatures.Command.Add;

public class AddCustomerCommandHandler : IRequestHandler<AddCustomerCommandModel, long>
{
    private readonly ICustomerContext _customerContext;
    private readonly IValidator<AddCustomerCommandModel> _validator;

    public AddCustomerCommandHandler(ICustomerContext customerContext, IValidator<AddCustomerCommandModel> validator)
    {
        _customerContext = customerContext;
        _validator = validator;
    }

    public async Task<long> Handle(AddCustomerCommandModel request, CancellationToken cancellationToken)
    {
        ValidationResult? validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (validationResult.IsValid)
        {
            bool isEmailInUse =
                await _customerContext.Customer.AnyAsync(c => c.Email == request.Email, cancellationToken);
            if (isEmailInUse) return default;
            Customer customer = new()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                BankAccountNumber = request.BankAccountNumber,
                DateOfBirth = request.DateOfBirth
            };

            await _customerContext.Customer.AddAsync(customer, cancellationToken);
            await _customerContext.SaveChangesAsync();
            return customer.Id;
        }

        throw new ValidationException(validationResult.Errors);
    }
}
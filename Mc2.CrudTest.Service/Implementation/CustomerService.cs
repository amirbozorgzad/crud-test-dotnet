using Core.ShareExtension;
using FluentValidation;
using FluentValidation.Results;
using MC2.CrudTest.Core.Contract;
using Mc2.CrudTest.Core.Contract.Customer;
using MC2.CrudTest.Core.Domain;
using MC2.CrudTest.Core.Domain.Model;
using MC2.CrudTest.Service.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace MC2.CrudTest.Service.Implementation;

public class CustomerService : ICustomerService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator _validator;

    public CustomerService(IUnitOfWork unitOfWork, IValidator<CustomerDto> validator)
    {
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async ValueTask<ResultDto<List<Customer>?>> GetCustomers(long? id)
    {
        IQueryable<Customer> customers = _unitOfWork.GenericRepository<Customer>().GetAll();
        if (id.IsNotNull()) customers = customers.Where(c => c.Id == id);
        return new ResultDto<List<Customer>?>
        {
            IsOk = true,
            Result = await customers.ToListAsync()
        };
    }

    public async ValueTask<ResultDto<string>> DeleteCustomer(long id)
    {
        List<Customer>? existingCustomer = (await GetCustomers(id)).Result;
        if (existingCustomer.IsNullOrEmpty())
            return new ResultDto<string>
            {
                IsOk = false,
                Result = "The Customer does not exist"
            };
        _unitOfWork.GenericRepository<Customer>().Delete(existingCustomer.FirstOrDefault());
        await _unitOfWork.SaveAsync();
        return new ResultDto<string>
        {
            IsOk = true,
            Result = "The Customer deleted successfully"
        };
    }

    public async ValueTask<ResultDto<string>> UpdateCustomer(CustomerDto dto, long id)
    {
        ValidationContext<CustomerDto> validationContext = new(dto);
        ValidationResult? validationResult = await _validator.ValidateAsync(validationContext);
        if (validationResult.IsValid)
        {
            List<Customer>? existingCustomer = (await GetCustomers(id)).Result;
            if (existingCustomer.IsNullOrEmpty())
                return new ResultDto<string>
                {
                    IsOk = false,
                    Result = "The Customer does not exist"
                };
            if (await IsEmailInUse(dto.Email, id))
                return new ResultDto<string>
                {
                    IsOk = false,
                    Result = "The Email has been exist"
                };

            if (existingCustomer.IsNotNullOrEmpty())
            {
                Customer customerToUpdate = dto.MapToEntity(existingCustomer.FirstOrDefault());
                _unitOfWork.GenericRepository<Customer>().Update(customerToUpdate);
                await _unitOfWork.SaveAsync();
            }

            return new ResultDto<string>
            {
                IsOk = true,
                Result = "The Customer has been updated"
            };
        }

        throw new ValidationException(validationResult.Errors);
    }

    public async ValueTask<ResultDto<string>> CreateCustomer(CustomerDto dto)
    {
        ValidationContext<CustomerDto> validationContext = new(dto);
        ValidationResult? validationResult = await _validator.ValidateAsync(validationContext);
        if (validationResult.IsValid)
        {
            if (await IsEmailInUse(dto.Email))
                return new ResultDto<string>
                {
                    IsOk = false,
                    Result = "The Email has been exist"
                };


            Customer newCustomer = dto.MapToEntity();
            await _unitOfWork.GenericRepository<Customer>().InsertAsync(newCustomer);
            await _unitOfWork.SaveAsync();
            return new ResultDto<string>
            {
                IsOk = true,
                Result = "The Customer has been created"
            };
        }

        throw new ValidationException(validationResult.Errors);
    }


    private async ValueTask<bool> IsEmailInUse(string email, long? id = null)
    {
        if (id.IsNotNull() &&
            (await _unitOfWork.GenericRepository<Customer>().GetByIDAsync(id))?.Email == email) return false;
        bool a = await _unitOfWork.GenericRepository<Customer>().GetAll().AnyAsync(c => c.Email == email);
        return a;
    }
}
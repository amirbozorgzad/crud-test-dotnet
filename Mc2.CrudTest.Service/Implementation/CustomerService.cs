using Core.ShareExtension;
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

    public CustomerService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async ValueTask<ResultDto<List<Customer>?>> GetCustomers(long? id)
    {
        try
        {
            IQueryable<Customer> customers = _unitOfWork.GenericRepository<Customer>().GetAll();
            if (id.IsNotNull()) customers = customers.Where(c => c.Id == id);
            return new ResultDto<List<Customer>?>
            {
                IsOk = true,
                Result = await customers.ToListAsync()
            };
        }
        catch (Exception e)
        {
            return new ResultDto<List<Customer>?>
            {
                IsOk = false,
                Result = null
            };
        }
    }

    public async ValueTask<ResultDto<(string message, long id)>> UpdateCustomer(CustomerDto dto)
    {
        try
        {
            List<Customer>? existingCustomer = (await GetCustomers(dto.Id)).Result;
            if (existingCustomer.IsNullOrEmpty())
                return new ResultDto<(string message, long id)>
                {
                    IsOk = false,
                    Result = new ValueTuple<string, long> { Item1 = "The Customer does not exist", Item2 = 0 }
                };
            if (await IsEmailInUse(dto.Email, dto.Id))
                return new ResultDto<(string message, long id)>
                {
                    IsOk = false,
                    Result = new ValueTuple<string, long> { Item1 = "The Email has been exist", Item2 = 0 }
                };

            if (existingCustomer.IsNotNullOrEmpty())
            {
                Customer customerToUpdate = dto.MapToEntity(existingCustomer.FirstOrDefault());
                _unitOfWork.GenericRepository<Customer>().Update(customerToUpdate);
                await _unitOfWork.SaveAsync();
            }

            return new ResultDto<(string message, long id)>
            {
                IsOk = true,
                Result = new ValueTuple<string, long> { Item1 = "The Customer has been updated", Item2 = (long)dto.Id }
            };
        }
        catch (Exception e)
        {
            return new ResultDto<(string message, long id)>
            {
                IsOk = false,
                Result = new ValueTuple<string, long> { Item1 = "An error occured", Item2 = 0 }
            };
        }
    }

    public async ValueTask<ResultDto<(string message, long id)>> CreateCustomer(CustomerDto dto)
    {
        try
        {
            if (await IsEmailInUse(dto.Email))
                return new ResultDto<(string message, long id)>
                {
                    IsOk = false,
                    Result = new ValueTuple<string, long> { Item1 = "The Email has been exist", Item2 = 0 }
                };


            Customer newCustomer = dto.MapToEntity();
            _unitOfWork.GenericRepository<Customer>().Insert(newCustomer);
            await _unitOfWork.SaveAsync();
            return new ResultDto<(string message, long id)>
            {
                IsOk = true,
                Result = new ValueTuple<string, long>
                    { Item1 = "The Customer has been created", Item2 = newCustomer.Id }
            };
        }

        catch (Exception e)
        {
            return new ResultDto<(string message, long id)>
            {
                IsOk = false,
                Result = new ValueTuple<string, long> { Item1 = "An error occured", Item2 = 0 }
            };
        }
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


    private async ValueTask<bool> IsEmailInUse(string email, long? id = null)
    {
        if (id.IsNotNull() &&
            (await _unitOfWork.GenericRepository<Customer>().GetByIDAsync(id))?.Email == email) return false;
        return await _unitOfWork.GenericRepository<Customer>().GetAll().AnyAsync(c => c.Email == email);
    }
}
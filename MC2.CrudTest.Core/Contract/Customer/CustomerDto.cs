namespace Mc2.CrudTest.Core.Contract.Customer;

public class CustomerDto
{
    public CustomerDto(MC2.CrudTest.Core.Domain.Model.Customer entity)
    {
        if (entity == null) return;
        Id = entity.Id;
        FirstName = entity.FirstName;
        LastName = entity.LastName;
        DateOfBirth = entity.DateOfBirth;
        PhoneNumber = entity.PhoneNumber;
        Email = entity.Email;
        BankAccountNumber = entity.BankAccountNumber;
    }

    public long? Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string BankAccountNumber { get; set; }

    public MC2.CrudTest.Core.Domain.Model.Customer MapToEntity(
        MC2.CrudTest.Core.Domain.Model.Customer existingCustomer = null)
    {
        MC2.CrudTest.Core.Domain.Model.Customer entity =
            existingCustomer ?? new MC2.CrudTest.Core.Domain.Model.Customer();
        entity.Id = existingCustomer.Id;
        entity.FirstName = existingCustomer.FirstName;
        entity.LastName = existingCustomer.LastName;
        entity.Email = existingCustomer.Email;
        entity.DateOfBirth = existingCustomer.DateOfBirth;
        entity.PhoneNumber = existingCustomer.PhoneNumber;
        entity.BankAccountNumber = existingCustomer.BankAccountNumber;
        return entity;
    }
}
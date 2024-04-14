using System.Text.RegularExpressions;
using CrudTest.Feature.CustomerFeatures.Command.Add;
using FluentValidation;
using PhoneNumbers;

namespace CrudTest.Feature.CustomerFeatures.Validator;

public class CustomerValidator<T> : AbstractValidator<T> where T : AddCustomerCommandModel
{
    public CustomerValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty().WithMessage("Please specify a first name");
        RuleFor(x => x.Email).Must(IsValidEmail).WithMessage("Email format is not valid");
        RuleFor(x => x.PhoneNumber).Must(IsValidPhoneNumber).WithMessage("Phone number format is not valid");
        RuleFor(x => x.BankAccountNumber).Must(IsValidAccountNumber).WithMessage("Account number format is not valid");
        RuleFor(x => x.DateOfBirth).LessThan(DateTime.Now.Date)
            .WithMessage("Date of birth should not be greater than today's date");
    }

    private bool IsValidEmail(string email)
    {
        Regex regex = new(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        var match = regex.Match(email);
        return match.Success;
    }

    private bool IsValidAccountNumber(string number)
    {
        Regex regex = new("^\\w{1,17}$");
        var match = regex.Match(number);
        return match.Success;
    }

    private bool IsValidPhoneNumber(string phoneNumber)
    {
        var phoneNumberUtil = PhoneNumberUtil.GetInstance();
        try
        {
            var number = phoneNumberUtil.Parse(phoneNumber, "IR");
            return phoneNumberUtil.IsValidNumber(number);
        }
        catch
        {
            return false;
        }
    }
}
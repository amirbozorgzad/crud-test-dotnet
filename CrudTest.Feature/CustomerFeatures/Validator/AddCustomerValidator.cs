using System.Text.RegularExpressions;
using CrudTest.Feature.CustomerFeatures.Command.Add;
using FluentValidation;
using PhoneNumbers;

namespace CrudTest.Feature.CustomerFeatures.Validator;

public class AddCustomerValidator : AbstractValidator<AddCustomerCommandModel>
{
    public AddCustomerValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty().WithMessage("Please specify a first name");
        RuleFor(x => x.Email).Must(IsValidEmail).WithMessage("Email format is not valid");
        RuleFor(x => x.PhoneNumber).Must(IsValidPhoneNumber).WithMessage("Phone number format is not valid");
        RuleFor(x => x.BankAccountNumber).Must(IsValidAccountNumber).WithMessage("Account number format is not valid");
        RuleFor(x => x.DateOfBirth).LessThan(DateTime.Now.Date)
            .WithMessage("Date of birth should not greater than today date");
    }

    private bool IsValidEmail(string email)
    {
        Regex regex = new(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        Match match = regex.Match(email);
        if (match.Success)
            return true;
        return false;
    }

    private bool IsValidAccountNumber(string number)
    {
        Regex regex = new("^\\w{1,17}$");
        Match match = regex.Match(number);
        if (match.Success)
            return true;
        return false;
    }

    private bool IsValidPhoneNumber(string phoneNumber)
    {
        PhoneNumberUtil? phoneNumberUtil = PhoneNumberUtil.GetInstance();
        try
        {
            PhoneNumber number = phoneNumberUtil.Parse(phoneNumber, "IR");
            return phoneNumberUtil.IsValidNumber(number);
        }
        catch
        {
            return false;
        }
    }
}
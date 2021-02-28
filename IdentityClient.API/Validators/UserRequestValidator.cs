using FluentValidation;
using IdentityClient.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace IdentityClient.API.Validators
{
    public class UserRequestValidator : AbstractValidator<User>
    {
        private static readonly UserRequestValidator instance = new UserRequestValidator();
        public static UserRequestValidator Instance => instance;
        private UserRequestValidator()
        {
            RuleFor(r => r.PersonalId)
                .NotNull().WithMessage("პირადი ნომერი არ შეიძლება იყოს ცარიელი!")
                .Length(11).WithMessage("პირადი ნომერი უნდა შედგებოდეს 11 ციფრისგან")
                .Must(p => long.TryParse(p, out long n)).WithMessage("პირადი ნომერი უნდა შეიცავდეს მხოლოდ ციფრებს!");

            RuleFor(r => r.UserName)
                .NotNull().WithMessage("მომხმარებლის სახელი არ შეიძლება იყოს ცარიელი!")
                .MinimumLength(6).WithMessage("მომხმარებლის სახელი უნდა შედგებოდეს 6 ან მეტი სიმბოლოსგან!");

            RuleFor(r => r.PasswordHash)
               .NotNull().WithMessage("მომხმარებლის პაროლი არ შეიძლება იყოს ცარიელი!");
               //.Must(p=> Regex.IsMatch(p, "^[0-9a-fA-F]{32}$", RegexOptions.Compiled)).WithMessage("");
            
            RuleFor(r => r.Salary).NotNull().GreaterThan(0).Unless(r => !r.IsEmployed).WithMessage("მონებს არ ვიღებთ!");

            RuleFor(r => r.ResidentialAddress.Country).NotNull().NotEmpty().WithMessage("ველი ქვეყანა არ შეიძლება იყოს ცარიელი!");
            RuleFor(r => r.ResidentialAddress.City).NotNull().NotEmpty().WithMessage("ველი ქალაქი არ შეიძლება იყოს ცარიელი!");
            RuleFor(r => r.ResidentialAddress.Street).NotNull().NotEmpty().WithMessage("ველი ქუჩა არ შეიძლება იყოს ცარიელი!");
            RuleFor(r => r.ResidentialAddress.Building).NotNull().NotEmpty().WithMessage("ველი შენობა არ შეიძლება იყოს ცარიელი!");
            RuleFor(r => r.ResidentialAddress.Apartment).NotNull().NotEmpty().WithMessage("ველი ბინა არ შეიძლება იყოს ცარიელი!");

        }

        public ValidationProblemDetails ValidateRequest(User request)
        {
            var validationResult = this.Validate(request);
            ValidationProblemDetails result = null;
            if (!validationResult.IsValid)
            {
                Dictionary<string, string[]> errorsDict = validationResult.Errors.GroupBy(e => e.PropertyName)
                                                                          .ToDictionary(k => k.Key, v => v.Select(e => e.ErrorMessage).ToArray());
                result = new ValidationProblemDetails(errorsDict);
            }
            return result;
        }

    }
}

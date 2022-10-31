using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Models;
using FluentValidation;

namespace API.ValidatorsFluent
{
    public class AddUserValidator : AbstractValidator<User>
    {
        public AddUserValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().NotNull();
            RuleFor(x => x.EmailAddress).NotEmpty();
            RuleFor(x => x.FullName).NotEmpty();
        }

    }
}
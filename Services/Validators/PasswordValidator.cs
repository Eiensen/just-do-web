using JustDo_Web.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JustDo_Web.ServerApp.Services.Validators
{
    public class PasswordValidator : IPasswordValidator<User>
    {
        private int requiredUppercaseLetters;

        public PasswordValidator(int letters)
        {
            requiredUppercaseLetters = letters;
        }
        public Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user, string password)
        {
            LinkedList<IdentityError> errors = new LinkedList<IdentityError>();
            string patternSeparate = @"([A-Z]{1,})";
            string patternTogether = @"([A-Z]{2,})";
            var matchSeparate = Regex.Matches(password, patternSeparate);
            var matchTogether = Regex.Matches(password, patternTogether);
            if (matchSeparate.Count < requiredUppercaseLetters && matchTogether.Count < 1)
            {
                errors.AddLast(new IdentityError
                {
                    Description = $"The password should contain {requiredUppercaseLetters} or more uppercase letters"
                });
            }
            return Task.FromResult(errors.Count == 0 ?
                IdentityResult.Success : IdentityResult.Failed(errors.ToArray()));
        }
    }
}

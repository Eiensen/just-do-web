using JustDo_Web.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JustDo_Web.ServerApp.Services.Validators
{
    public class PasswordValidator: IPasswordValidator<User>
    {
        public int RequiredUppercaseLetters { get; set; }

        public PasswordValidator(int letters)
        {
            RequiredUppercaseLetters = letters;
        }
        public Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user, string password)
        {
            LinkedList<IdentityError> errors = new LinkedList<IdentityError>();

            //todo: доделать правильную проверку на наличие 2 и более заглавных букв в пароле
            string pattern = "^[A-Z]{2,}$"; // на данный момент шаблон не рабочий

            if(!Regex.IsMatch(password, pattern))
            {
                errors.AddFirst(new IdentityError
                {
                    Description = $"The password should contain {RequiredUppercaseLetters} or more uppercase letters"
                });
            }
            return Task.FromResult(errors.Count == 0 ?
                IdentityResult.Success : IdentityResult.Failed(errors.ToArray()));
        }
    }
}

using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace JustDo_Web.Helpers
{
    public static class ErrorHelper
    {
        public static List<string> AddIdentityErrors(IdentityResult identityResult)
        {
            var errors = new List<string>();

            foreach (var e in identityResult.Errors)
            {
                errors.Add(e.Description);
            }

            return errors;
        }

        public static List<string> AddError(string description)
        {
            var errors = new List<string>
            {
                description
            };

            return errors;
        }
    }
}

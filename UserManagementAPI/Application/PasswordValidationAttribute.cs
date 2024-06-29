namespace UserManagementAPI.Application
{
    // PasswordValidationAttribute.cs

    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class PasswordValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var password = value as string;
            if (password == null)
            {
                return false;
            }

            // Password must be at least 8 characters
            if (password.Length < 8)
            {
                return false;
            }

            // Should contain at least one lowercase character
            if (!Regex.IsMatch(password, "[a-z]"))
            {
                return false;
            }

            // Should contain at least one uppercase character
            if (!Regex.IsMatch(password, "[A-Z]"))
            {
                return false;
            }

            // Should contain at least two digits
            if (!Regex.IsMatch(password, @"\d{2,}"))
            {
                return false;
            }

            // Should contain at least one symbol (non-word character)
            if (!Regex.IsMatch(password, @"\W"))
            {
                return false;
            }

            return true;
        }
    }

}

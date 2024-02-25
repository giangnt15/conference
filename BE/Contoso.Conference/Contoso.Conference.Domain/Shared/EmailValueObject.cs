using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Contoso.Conference.Domain.Shared
{
    public partial record EmailValueObject
    {
        [GeneratedRegex(@"^\\w+([\\.-]?\\w+)*@\\w+([\\.-]?\\w+)*(\\.\\D{2,3})+$")]
        public static partial Regex EmailRegex();
        public string Email { get; set; }

        private EmailValueObject(string email)
        {
            if (!EmailRegex().IsMatch(email)) {
                throw new ArgumentException($"{nameof(email)} is not a valid email address!");
            }
            Email = email;
        }

        public static EmailValueObject FromEmail(string email) => new(email);

        public static implicit operator string(EmailValueObject obj) => obj.Email;
        public static implicit operator EmailValueObject(string email) => new(email);
    }
}

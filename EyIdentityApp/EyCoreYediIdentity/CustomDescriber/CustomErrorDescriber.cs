using Microsoft.AspNetCore.Identity;

namespace EyCoreYediIdentity.CustomDescriber
{
    public class CustomErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError PasswordTooShort(int length)
        {
            return new()
            {
                Code = "PasswordTooShort",
                Description = $"Parola en az {length} karakter olmalıdır!"
            };
        }
        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            return new()
            {
                Code = "PasswordRequiresNonAlphanumeric",
                Description = "Parola en az bir alfanümerik(~?! vs.) karakter içermelidir!"
            };
        }
        public override IdentityError DuplicateUserName(string userName)
        {
            return new()
            {
                Code = "DuplicateUserName",
                Description = $"{userName} zaten alınmış!"
            };
        }
        public override IdentityError PasswordRequiresLower()
        {
            return new()
            {
                Code = "PasswordRequiresLower",
                Description = "Parolanız en az 1 tane küçük harf('a'-'z') içermelidir!"
            };
        }
        public override IdentityError PasswordRequiresUpper()
        {
            return new()
            {
                Code = "PasswordRequiresUpper",
                Description = "Parolanız en az 1 tane büyük harf('A'-'Z') içermelidir!"
            };
        }
    }
}

using System.ComponentModel.DataAnnotations;

namespace NSE.Identity.API.Models
{
    public class UserRegister
    {
        [Required(ErrorMessage = "The field {0} is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [EmailAddress(ErrorMessage = "The field {0} is not a valid e-mail")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(100, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 6)]
        public string? Password { get; set; }

        [Compare("Password", ErrorMessage = "Password do not match")]
        public string? PasswordConfirm { get; set;}

    }

    public class UserLogin
    {
        [Required(ErrorMessage = "The field {0} is required")]
        [EmailAddress(ErrorMessage = "The field {0} is not a valid e-mail")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(100, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 6)]
        public string? Password { get; set; }
        
    }

    public class UserResponseLogin
    {
        public string? AccessToken { get; set; }

        public double ExpiresIn { get; set; }

        public UserToken? userToken { get; set; }

    }

    public class UserToken
    {
        public string? Id { get; set; }

        public string? Email { get; set; }

        public IEnumerable<UserClaim>? Claims { get; set; }
    }

    public class UserClaim
    {
        public string? Value { get; set; }

        public string? Type { get; set; }
    }


}

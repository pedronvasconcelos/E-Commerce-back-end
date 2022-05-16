using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace NSE.WebApp.MVC.Models
{
    public class UserRegister
    {
        [Required(ErrorMessage = "The field {0} is required")]
        [EmailAddress(ErrorMessage = "The field {0} is not a valid e-mail")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(100, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 6)]
        public string? Password { get; set; }
        
        [DisplayName("Confirm your password")]
        [Compare("Password", ErrorMessage = "Password do not match")]
        public string? PasswordConfirm { get; set; }

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


    public class UserLoginResponse
    {
        public string? AcessToken { get; set; }

        public double ExpiresIn { get; set; }

        public UserToken? UserToken { get; set; }

        public ResponseResult ResponseResult { get; set; }
        
    }
    public class UserToken
    {
        public string? Id { get; set; }
        public string? Email { get; set; }
        public IEnumerable<Claim>? Claims { get; set; }
    }

    public class UserClaim
    {
        public string? Value { get; set; }
        public string? Type { get; set; }
    }


}

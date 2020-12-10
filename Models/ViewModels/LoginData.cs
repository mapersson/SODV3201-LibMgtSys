using System.ComponentModel.DataAnnotations;

namespace SODV3201_LibMgtSys.Models.ViewModels
{
    public class LoginData
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}
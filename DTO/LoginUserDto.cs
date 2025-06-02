using System.ComponentModel.DataAnnotations;

namespace CodeVote.DTO
{
    public class LoginUserDto
    {
        [Required(ErrorMessage = "Username is required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}

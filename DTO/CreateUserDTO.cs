using System.ComponentModel.DataAnnotations;


namespace CodeVote.DTO
{
    public class CreateUserDTO
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string PasswordHash { get; set; }

    }
}

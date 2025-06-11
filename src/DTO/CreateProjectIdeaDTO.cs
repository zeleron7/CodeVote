using System.ComponentModel.DataAnnotations;

namespace CodeVote.src.DTO
{
    public class CreateProjectIdeaDTO
    {
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace UserService.Dtos
{
    public class UserCreateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string PasswordHash { get; set; }
    }
}

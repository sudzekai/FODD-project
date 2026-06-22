using System.ComponentModel.DataAnnotations;

namespace Shared.dtos.categories
{
    public class CategoryWriteDTO
    {
        public CategoryWriteDTO()
        {
            
        }
        public CategoryWriteDTO(string name)
        {
            Name = name;
        }

        [Required(ErrorMessage = "Name обязательно")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Name должно быть длиной от 1 до 50 символов")]
        public string Name { get; set; } = null!;
    }
}

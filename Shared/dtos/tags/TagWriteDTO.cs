using System.ComponentModel.DataAnnotations;

namespace Shared.dtos.tags
{
    public class TagWriteDTO
    {
        public TagWriteDTO()
        {
            
        }
        public TagWriteDTO(string name)
        {
            Name = name;
        }

        [Required(ErrorMessage = "Name обязательно")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Name должно быть длиной от 1 до 50 символов")]
        public string Name { get; set; } = null!;
    }
}

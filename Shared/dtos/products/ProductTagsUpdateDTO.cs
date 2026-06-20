using System.ComponentModel.DataAnnotations;

namespace Shared.dtos.products
{
    public class ProductTagsUpdateDTO
    {
        public ProductTagsUpdateDTO()
        {

        }
        public ProductTagsUpdateDTO(List<int> tagIds)
        {
            TagIds = tagIds;
        }

        [Required(ErrorMessage = "Список id тэгов обязателен")]
        public List<int> TagIds { get; set; }
    }
}

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

        public List<int> TagIds { get; set; }
    }
}

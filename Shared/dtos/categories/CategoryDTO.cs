namespace Shared.dtos.categories
{
    public class CategoryDTO
    {
        public CategoryDTO()
        {

        }

        public CategoryDTO(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }

        public string Name { get; set; } = null!;
    }
}

namespace Shared.dtos.tags
{
    public class TagDTO
    {
        public TagDTO()
        {
                
        }

        public TagDTO(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }

        public string Name { get; set; } = null!;
    }
}

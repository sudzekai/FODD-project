namespace Shared.dtos.manufacturers
{
    public class ManufacturerDTO
    {
        public ManufacturerDTO()
        {
                
        }

        public ManufacturerDTO(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }

        public string Name { get; set; } = null!;
    }
}

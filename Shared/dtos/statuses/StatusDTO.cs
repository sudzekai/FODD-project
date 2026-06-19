namespace Shared.dtos.statuses
{
    public class StatusDTO
    {
        public StatusDTO()
        {
                
        }

        public StatusDTO(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }

        public string Name { get; set; } = null!;
    }
}

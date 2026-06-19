namespace Shared.dtos.roles
{
    public class RoleDTO
    {
        public RoleDTO() { }

        public RoleDTO(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}

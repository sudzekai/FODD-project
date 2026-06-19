namespace Shared.dtos.suppliers
{
    public class SupplierDTO
    {
        public SupplierDTO()
        {
                
        }

        public SupplierDTO(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }

        public string Name { get; set; } = null!;
    }
}

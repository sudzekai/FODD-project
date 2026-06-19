using System.ComponentModel.DataAnnotations;

namespace Shared.dtos.products
{
    public class ProductRelationsUpdateDTO
    {
        public ProductRelationsUpdateDTO()
        {
            
        }

        public ProductRelationsUpdateDTO(int supplierId, int manufacturerId)
        {
            SupplierId = supplierId;
            ManufacturerId = manufacturerId;
        }


        [Required(ErrorMessage = "SupplierId обязателен")]
        [Range(0, int.MaxValue, ErrorMessage = "SupplierId должен быть больше 0")]
        public int SupplierId { get; set; }
        [Required(ErrorMessage = "ManufacturerId обязателен")]
        [Range(0, int.MaxValue, ErrorMessage = "ManufacturerId должен быть больше 0")]
        public int ManufacturerId { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Shared.dtos.orders
{
    public class OrderDeliveryUpdateDTO
    {
        public OrderDeliveryUpdateDTO()
        {
            
        }

        public OrderDeliveryUpdateDTO(DateTime deliveryDate, int receiptCode)
        {
            DeliveryDate = deliveryDate;
            ReceiptCode = receiptCode;
        }

        [Required(ErrorMessage = "DeliveryDate обязателен")]
        public DateTime DeliveryDate { get; set; }


        [Required(ErrorMessage = "ReceiptCode обязателен")]
        [Range(1, int.MaxValue, ErrorMessage = "ReceiptCode должен быть больше 0")]
        public int ReceiptCode { get; set; }
    }
}

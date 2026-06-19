namespace Shared.dtos.orders
{
    public class OrderSimpleDTO
    {
        public OrderSimpleDTO()
        {
            
        }

        public OrderSimpleDTO(int id, DateTime creationDateTime, DateTime? deliveryDate, int? receiptCode, int statusId, int? userId)
        {
            Id = id;
            CreationDateTime = creationDateTime;
            DeliveryDate = deliveryDate;
            ReceiptCode = receiptCode;
            StatusId = statusId;
            UserId = userId;
        }

        public int Id { get; set; }
        public int? UserId { get; set; }

        public DateTime CreationDateTime { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public int? ReceiptCode { get; set; }

        public int StatusId { get; set; }
    }
}

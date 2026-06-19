using System.Net.NetworkInformation;

namespace Shared.dtos.orders
{
    public class OrderDTO
    {
        public OrderDTO()
        {
            
        }

        public OrderDTO(int id, DateTime creationDateTime, DateTime? deliveryDate, int? receiptCode, int statusId, List<int> productIds, int? userId)
        {
            Id = id;
            CreationDateTime = creationDateTime;
            DeliveryDate = deliveryDate;
            ReceiptCode = receiptCode;
            StatusId = statusId;
            ProductIds = productIds;
            UserId = userId;
        }

        public int Id { get; set; }

        public DateTime CreationDateTime { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public int? ReceiptCode { get; set; }

        public int StatusId { get; set; }

        public int? UserId { get; set; }

        public List<int> ProductIds { get; set; }
    }
}

namespace FoodDelivery.Entities.Dto
{
    public class FoodOrderForAddDto
    {
        public int CustomerId { get; set; }
        public int AddressId { get; set; }
        public int UserId { get; set; }
        public int OrderStatusId { get; set; }
        public int RestaurantId { get; set; }
        public decimal DeliveryFee { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime OrderDateTime { get; set; }
        public DateTime RequestedDeliveryOrderDate { get; set; }
    }
}

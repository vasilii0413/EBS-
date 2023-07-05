namespace FoodDelivery.Entities.Dto
{
    public class FoodItemForUpdateDto
    {
        public string? FoodItemName { get; set; }
        public decimal FoodItemPrice { get; set; }
        public int FoodCategoryId { get; set; }
    }
}

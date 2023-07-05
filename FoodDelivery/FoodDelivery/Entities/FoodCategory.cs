namespace FoodDelivery.Entities
{
    public class FoodCategory
    {
        public int FoodCategoryId { get; set; }
        public string? FoodCategoryName { get; set; }
        public int RestaurantId { get; set; }
    }
}

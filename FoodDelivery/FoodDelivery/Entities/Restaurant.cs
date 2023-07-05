namespace FoodDelivery.Entities
{
    public class Restaurant
    {
        public int restaurant_id {  get; set; }
        public string? name { get; set; }
        public int address_id { get; set; }
        public List<Addresses> addresses { get; set; } = new List<Addresses>();
    }
}

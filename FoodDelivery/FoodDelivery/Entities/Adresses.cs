namespace FoodDelivery.Entities
{
    public class Addresses
    {
        public int address_id { get; set; }
        public  int HouseNumber { get; set;}
        public string? StreetName { get; set;}
        public string? City { get; set;}
        public string? PostalCode { get; set;}

        public static implicit operator List<object>(Addresses v)
        {
            throw new NotImplementedException();
        }
    }
}

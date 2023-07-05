namespace FoodDelivery.Entities.Dto
{
    public class AddressForUpdateDto
    {
        public int HouseNumber { get; set; }
        public string? StreetName { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }
    }
}

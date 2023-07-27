namespace CarRental.Data.Rentals
{
    public static class RentalEndpoints
    {
        public static void MapRentalEndpoints(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapControllerRoute(
                name: "rentals_index",
                pattern: "/rentals",
                defaults: new { controller = "Rentals", action = "IndexRental" }
            );

            endpoints.MapControllerRoute(
                name: "rentals_details",
                pattern: "/rentals/details/{id}",
                defaults: new { controller = "Rentals", action = "DetailsRental" }
            );

            endpoints.MapControllerRoute(
                name: "rentals_create",
                pattern: "/rentals/create/{vehicleId}",
                defaults: new { controller = "Rentals", action = "CreateRental" }
            );

            endpoints.MapControllerRoute(
                name: "rentals_edit",
                pattern: "/rentals/edit/{id}",
                defaults: new { controller = "Rentals", action = "EditRental" }
            );

            endpoints.MapControllerRoute(
                name: "rentals_delete",
                pattern: "/rentals/delete/{id}",
                defaults: new { controller = "Rentals", action = "DeleteRental" }
            );
        }
    }
}

namespace CarRental.Data.Fuels
{
    public static class FuelEndpoints
    {
        public static void MapFuelEndpoints(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapControllerRoute(
                name: "fuels_index",
                pattern: "/fuels",
                defaults: new { controller = "Fuels", action = "IndexFuel" }
            );

            endpoints.MapControllerRoute(
                name: "fuels_details",
                pattern: "/fuels/details/{id}",
                defaults: new { controller = "Fuels", action = "Details" }
            );

            endpoints.MapControllerRoute(
                name: "fuels_create",
                pattern: "/fuels/create",
                defaults: new { controller = "Fuels", action = "CreateFuel" }
            );

            endpoints.MapControllerRoute(
                name: "fuels_edit",
                pattern: "/fuels/edit/{id}",
                defaults: new { controller = "Fuels", action = "EditFuel" }
            );

            endpoints.MapControllerRoute(
                name: "fuels_delete",
                pattern: "/fuels/delete/{id}",
                defaults: new { controller = "Fuels", action = "DeleteFuel" }
            );
        }
    }
}

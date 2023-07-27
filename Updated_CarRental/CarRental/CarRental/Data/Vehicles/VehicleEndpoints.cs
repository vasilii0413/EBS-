namespace CarRental.Data.Vehicles
{
    public static class VehicleEndpoints
    {
        public static void MapVehiclesEndpoints(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapControllerRoute(
                name: "vehicles_index",
                pattern: "/vehicles",
                defaults: new { controller = "Vehicles", action = "IndexVehicle" }
            );

            endpoints.MapControllerRoute(
                name: "vehicles_details",
                pattern: "/vehicles/details/{id}",
                defaults: new { controller = "Vehicles", action = "DetailsVehicle" }
            );

            endpoints.MapControllerRoute(
                name: "vehicles_create",
                pattern: "/vehicles/create",
                defaults: new { controller = "Vehicles", action = "CreateVehicle" }
            );

            endpoints.MapControllerRoute(
                name: "vehicles_edit",
                pattern: "/vehicles/edit/{id}",
                defaults: new { controller = "Vehicles", action = "EditVehicle" }
            );
        }
    }
}




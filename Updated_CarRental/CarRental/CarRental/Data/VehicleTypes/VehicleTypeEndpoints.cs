namespace CarRental.Data.VehicleTypes
{
    public static class VehicleTypesEndpoints
    {
        public static void MapVehicleTypesEndpoints(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapControllerRoute(
                name: "vehicleTypes_index",
                pattern: "/vehicleTypes",
                defaults: new { controller = "VehicleTypes", action = "IndexVehicleType" }
            );

            endpoints.MapControllerRoute(
                name: "vehicleTypes_details",
                pattern: "/vehicleTypes/details/{id}",
                defaults: new { controller = "VehicleTypes", action = "DetailsVehicleType" }
            );

            endpoints.MapControllerRoute(
                name: "vehicleTypes_create",
                pattern: "/vehicleTypes/create",
                defaults: new { controller = "VehicleTypes", action = "CreateVehicleType" }
            );

            endpoints.MapControllerRoute(
                name: "vehicleTypes_edit",
                pattern: "/vehicleTypes/edit/{id}",
                defaults: new { controller = "VehicleTypes", action = "EditVehicleType" }
            );

            endpoints.MapControllerRoute(
                name: "vehicleTypes_delete",
                pattern: "/vehicleTypes/delete/{id}",
                defaults: new { controller = "VehicleTypes", action = "DeleteVehicleType" }
            );
        }
    }
}

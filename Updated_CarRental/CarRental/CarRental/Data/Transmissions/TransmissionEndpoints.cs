namespace CarRental.Data.Transmissions
{
    public static class TransmissionEndpoints
    {
        public static void MapTransmissionEndpoints(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapControllerRoute(
                name: "transmissions_index",
                pattern: "/transmissions",
                defaults: new { controller = "Transmissions", action = "IndexTransmission" }
            );

            endpoints.MapControllerRoute(
                name: "transmissions_details",
                pattern: "/transmissions/details/{id}",
                defaults: new { controller = "Transmissions", action = "DetailsTransmission" }
            );

            endpoints.MapControllerRoute(
                name: "transmissions_create",
                pattern: "/transmissions/create",
                defaults: new { controller = "Transmissions", action = "CreateTransmission" }
            );

            endpoints.MapControllerRoute(
                name: "transmissions_edit",
                pattern: "/transmissions/edit/{id}",
                defaults: new { controller = "Transmissions", action = "EditTransmission" }
            );

            endpoints.MapControllerRoute(
                name: "transmissions_delete",
                pattern: "/transmissions/delete/{id}",
                defaults: new { controller = "Transmissions", action = "DeleteTransmission" }
            );
        }
    }
}

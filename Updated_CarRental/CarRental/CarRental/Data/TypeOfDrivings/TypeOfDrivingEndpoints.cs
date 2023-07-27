namespace CarRental.Data.TypeOfDrivings
{
    public static class TypeOfDrivingEndpoints
    {
        public static void MapTypeOfDrivingsEndpoints(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapControllerRoute(
                name: "typeofdrivings_index",
                pattern: "/typeofdrivings",
                defaults: new { controller = "TypeOfDrivings", action = "IndexTypeOfDriving" }
            );

            endpoints.MapControllerRoute(
                name: "typeofdrivings_details",
                pattern: "/typeofdrivings/details/{id}",
                defaults: new { controller = "TypeOfDrivings", action = "DetailsTypeOfDriving" }
            );

            endpoints.MapControllerRoute(
                name: "typeofdrivings_create",
                pattern: "/typeofdrivings/create",
                defaults: new { controller = "TypeOfDrivings", action = "CreateTypeOfDriving" }
            );

            endpoints.MapControllerRoute(
                name: "typeofdrivings_edit",
                pattern: "/typeofdrivings/edit/{id}",
                defaults: new { controller = "TypeOfDrivings", action = "EditTypeOfDriving" }
            );

            endpoints.MapControllerRoute(
                name: "typeofdrivings_delete",
                pattern: "/typeofdrivings/delete/{id}",
                defaults: new { controller = "TypeOfDrivings", action = "DeleteTypeOfDriving" }
            );
        }
    }
}

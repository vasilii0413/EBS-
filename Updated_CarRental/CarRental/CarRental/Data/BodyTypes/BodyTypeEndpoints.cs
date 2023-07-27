namespace CarRental.Data.BodyTypes
{
    public static class BodyTypeEndpoints
    {
        public static void MapBodyTypeEndpoints(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapControllerRoute(
                name: "bodytypes_index",
                pattern: "/bodytypes",
                defaults: new { controller = "BodyTypes", action = "IndexBody" }
            );

            endpoints.MapControllerRoute(
                name: "bodytypes_details",
                pattern: "/bodytypes/details/{id}",
                defaults: new { controller = "BodyTypes", action = "DetailsBody" }
            );

            endpoints.MapControllerRoute(
                name: "bodytypes_create",
                pattern: "/bodytypes/create",
                defaults: new { controller = "BodyTypes", action = "CreateBody" }
            );

            endpoints.MapControllerRoute(
                name: "bodytypes_edit",
                pattern: "/bodytypes/edit/{id}",
                defaults: new { controller = "BodyTypes", action = "EditBody" }
            );

            endpoints.MapControllerRoute(
                name: "bodytypes_delete",
                pattern: "/bodytypes/delete/{id}",
                defaults: new { controller = "BodyTypes", action = "DeleteBody" }
            );
        }
    }
}

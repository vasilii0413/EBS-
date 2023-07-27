namespace CarRental.Data.ReservationStatus
{
    public static class ReservationStatusEndpoints
    {
        public static void MapReservationStatusEndpoints(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapControllerRoute(
                name: "reservationstatus_index",
                pattern: "/reservationstatus",
                defaults: new { controller = "ReservationStatus", action = "IndexReservation" }
            );

            endpoints.MapControllerRoute(
                name: "reservationstatus_details",
                pattern: "/reservationstatus/details/{id}",
                defaults: new { controller = "ReservationStatus", action = "DetailsReservation" }
            );

            endpoints.MapControllerRoute(
                name: "reservationstatus_create",
                pattern: "/reservationstatus/create",
                defaults: new { controller = "ReservationStatus", action = "CreateReservation" }
            );

            endpoints.MapControllerRoute(
                name: "reservationstatus_edit",
                pattern: "/reservationstatus/edit/{id}",
                defaults: new { controller = "ReservationStatus", action = "EditReservation" }
            );

            endpoints.MapControllerRoute(
                name: "reservationstatus_delete",
                pattern: "/reservationstatus/delete/{id}",
                defaults: new { controller = "ReservationStatus", action = "DeleteReservation" }
            );
        }
    }
}

namespace on_the_sky.core.Dto
{
    public class TravelDto
    {
        public int TravelId { get; set; }
        public int AmountTickets { get; set; }
        public int FlightId { get; set; }
        public int UserId { get; set; }

        public FlightDto Flight { get; set; }
        public UserDto User { get; set; }

    }
}

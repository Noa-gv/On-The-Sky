using System.ComponentModel.DataAnnotations;
using on_the_sky.core.entities;

namespace on_the_sky
{
    public class Travel
    {
        [Key]
        public int TravelId { get; set; } 

        public int AmountTickets { get; set; }

        public int FlightId { get; set; }
        public Flight Flight { get; set; }

        public int UserId { get; set; }
        public Users User { get; set; }
    }
}

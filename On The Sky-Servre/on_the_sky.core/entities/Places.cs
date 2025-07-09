using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace on_the_sky
{
    public class Places
    {
        [Key]
        public int countryid { get; set; }
        public string country { get; set; }
        public List<Flight>? flight_list { get; set; }
    }
}

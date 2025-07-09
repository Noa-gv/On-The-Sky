using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace on_the_sky
{
    public class Flight
    {
        [Key]
        public int flightid { get; set; }
        public DateTime flighttime { get; set; }
        public int countryID { get; set; }
        public int Price { get; set; } //שדה מחיר כרטיס טיסה
        public int Maximum { get; set; }
        public int amount { get; set; }
        public List<Travel>? Travels { get; set; }
        public Places country { get; set; }
    }
}


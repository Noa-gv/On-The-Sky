namespace on_the_sky.models
{
    public class FlightPostModel
    {
     
        public DateTime flighttime { get; set; }
        public int countryID { get; set; }
        public int Price { get; set; } //הוספת מחיר כרטיס טיסה
        public int Maximum { get; set; }
        public int amount { get; set; }
    }
}

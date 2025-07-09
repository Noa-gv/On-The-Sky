using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using on_the_sky.core.Dto;
using on_the_sky.core.entities;
using on_the_sky.core.services;
using on_the_sky.models;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;

namespace on_the_sky.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // כל הפעולות דורשות התחברות
    public class TravelsController : ControllerBase
    {
        private readonly ItravelService _travelservice;
        private readonly IFlightService _flightService;
        private readonly IMapper _mapper;

        public TravelsController(ItravelService travelservice, IFlightService flightService, IMapper map)
        {
            _travelservice = travelservice;
            _flightService = flightService;
            _mapper = map;
        }

        // GET: api/Travels - רק למנהלים
        [HttpGet]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult> Get()
        {
            var travelsList = await _travelservice.GetAll();
            var travels = _mapper.Map<IEnumerable<TravelDto>>(travelsList);
            return Ok(travels);
        }

        // GET: api/Travels/5 - לפי מזהה נסיעה
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var travel = await _travelservice.GetById(id);
            if (travel != null)
            {
                var dto = _mapper.Map<TravelDto>(travel);
                return Ok(dto);
            }
            return NotFound();
        }

        // GET api/Travels/my - כל הנסיעות של המשתמש המחובר
        [HttpGet("my")]
        public async Task<ActionResult<IEnumerable<TravelDto>>> GetMyTravels()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");
            if (userIdClaim == null)
                return Unauthorized("UserId claim missing");

            int userId = int.Parse(userIdClaim.Value);
            var travelsList = await _travelservice.GetByUserId(userId);
            var travelsDto = _mapper.Map<IEnumerable<TravelDto>>(travelsList);
            return Ok(travelsDto);
        }

        // POST api/Travels
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TravelPostModel t)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");
            if (userIdClaim == null)
                return Unauthorized("UserId claim missing");

            int userId = int.Parse(userIdClaim.Value);
            var newTravel = _mapper.Map<Travel>(t);
            newTravel.UserId = userId;

            var flight = await _flightService.GetById(newTravel.FlightId);
            if (flight == null)
                return NotFound("Flight not found");

            // בדיקת זמין: כמות הנוסעים החדשה לא תגרום לחריגה מהמקסימום
            if (flight.amount + newTravel.AmountTickets > flight.Maximum)
                return BadRequest("Cannot add tickets: exceeds maximum allowed for flight.");

            // מוסיפים הזמנה
            await _travelservice.ADD(newTravel);

            // מעדכנים את כמות הנוסעים בטיסה
            flight.amount += newTravel.AmountTickets;
            await _flightService.Put(flight.flightid, flight);

            return Ok();
        }

        // PUT api/Travels/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] TravelPostModel t)
        {
            var travelToUpdate = await _travelservice.GetById(id);
            if (travelToUpdate == null)
                return NotFound();

            int oldAmountTickets = travelToUpdate.AmountTickets;
            int oldFlightId = travelToUpdate.FlightId;

            // בודקים את הטיסה הישנה והטיסה החדשה
            var oldFlight = await _flightService.GetById(oldFlightId);
            var newFlight = await _flightService.GetById(t.FlightId);

            if (newFlight == null)
                return NotFound("New flight not found");

            // מחשבים את הכמות שתישאר בטיסות לאחר העדכון
            int oldFlightNewAmount = oldFlight != null ? oldFlight.amount - oldAmountTickets : 0;
            int newFlightNewAmount = (t.FlightId == oldFlightId)
                ? (oldFlightNewAmount + t.AmountTickets)  // אם הטיסה לא השתנתה, מחזירים את הכרטיסים שנחסרו ומוסיפים את החדשים
                : (newFlight.amount + t.AmountTickets);

            // בדיקה שלא יורדים מתחת ל-0 בטיסה הישנה
            if (oldFlight != null && oldFlightNewAmount < 0)
                return BadRequest("Invalid ticket amount: flight passengers cannot be negative.");

            // בדיקה שלא עולים על המקסימום בטיסה החדשה
            if (newFlightNewAmount > newFlight.Maximum)
                return BadRequest("Cannot update tickets: exceeds maximum allowed for new flight.");

            // מעדכנים את הזמנת הנסיעה
            travelToUpdate.FlightId = t.FlightId;
            travelToUpdate.AmountTickets = t.AmountTickets;

            var result = await _travelservice.Put(id, travelToUpdate);

            // מעדכנים את הכמויות בטיסות
            if (oldFlight != null)
            {
                oldFlight.amount = oldFlightNewAmount;
                await _flightService.Put(oldFlight.flightid, oldFlight);
            }

            newFlight.amount = newFlightNewAmount;
            await _flightService.Put(newFlight.flightid, newFlight);

            return Ok(result);
        }

        // DELETE api/Travels/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var travel = await _travelservice.GetById(id);
            if (travel == null)
                return NotFound();

            var deletedTravel = await _travelservice.Delete(id);

            if (deletedTravel != null)
            {
                var flight = await _flightService.GetById(deletedTravel.FlightId);
                if (flight != null)
                {
                    flight.amount -= deletedTravel.AmountTickets;
                    if (flight.amount < 0) flight.amount = 0;
                    await _flightService.Put(flight.flightid, flight);
                }
                return Ok();
            }

            return NotFound();
        }

    }
}

using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using on_the_sky.core.Dto;
using on_the_sky.core.services;
using on_the_sky.models;
using on_the_sky.service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace on_the_sky.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "manager")]
    public class FlightController : ControllerBase
    {
        private readonly IFlightService _flightservice ;
        private readonly IMapper _mapper;


        public FlightController(IFlightService flightservice, IMapper map)
        {
            _flightservice = flightservice;
            _mapper = map;
        }

        // GET: api/<TravelsController>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Get()
        {
            var FlightList = await _flightservice.Getlist();
            var Flights= _mapper.Map<IEnumerable<FlightDto>>(FlightList);
            return Ok( Flights);
        }

        // GET api/<TravelsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var fly = await _flightservice.GetById(id);
            var Flight= _mapper.Map<FlightDto>(fly);
            if (fly != null) { 
                return Ok(Flight);
            }
            return NotFound() ;
        }


        // POST api/<TravelsController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] FlightPostModel f)
        {
            var newfly = _mapper.Map<Flight>(f);
            await _flightservice.ADD(newfly);

            return Ok();
        }

        // PUT api/<TravelsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] FlightPostModel f)
        {
            //    var newfly = new Flight { flighttime = f.flighttime, countryID = f.countryID, Maximum = f.Maximum, amount = f.amount };
            var fly = await _flightservice.Put(id , _mapper.Map<Flight>(f));
            if (fly != null)
            {
                return Ok(fly);
            }
            return NotFound();                        
        }

        // DELETE api/<TravelsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var fly= await _flightservice.Delete(id);
            if (fly != null)
            {
                return Ok();
            }
            return NotFound();

        }
}
}

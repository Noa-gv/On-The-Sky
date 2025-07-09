using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using on_the_sky.core.Dto;
using on_the_sky.core.services;
using on_the_sky.models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace on_the_sky.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "manager")]
    public class PlacesController : ControllerBase
    {
        private readonly IplaceService _Placeservice ;
       private readonly IMapper _Mapper ;

        public PlacesController(IplaceService Placeservice, IMapper map )
        {
            _Placeservice = Placeservice;
            _Mapper=map;
        }



        // GET: api/<TravelsController>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Get()
        {
            var placeList = await _Placeservice.GetAll();
            var places=_Mapper.Map<IEnumerable<PlaceDto>>(placeList);
            return Ok(places);
         
        }

        // GET api/<TravelsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var pla = await _Placeservice.GetById(id);
            var place= _Mapper.Map<PlaceDto>(pla);
            if (pla != null)
            {
                return Ok(place);
            }
            return NotFound();
        }


        // POST api/<TravelsController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PlacesPostModel p)
        {
            var newplace = _Mapper.Map<Places>(p);
            await _Placeservice.ADD(newplace);
            return Ok();
        }

        // PUT api/<TravelsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] PlacesPostModel p)
        {
          
            var place = await _Placeservice.Put(id, _Mapper.Map<Places>(p));
            if (place != null)
            {
                return Ok(place);
            }
            return NotFound();                                 // עדכון אובייקט
        }

        // DELETE api/<TravelsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var place = await _Placeservice.Delete(id);
            if (place != null)
            {
                return Ok();
            }
            return NotFound();
            //מחיקת אובייקט
        }
    }
}

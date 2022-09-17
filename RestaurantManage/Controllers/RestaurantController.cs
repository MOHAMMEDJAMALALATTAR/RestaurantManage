using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantManage.Models;
using RestaurantManage.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantManage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly restaurantdbContext _context;
        private readonly IMapper _mapper;
        public RestaurantController(restaurantdbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        // GET: api/<CustomerController>
        [HttpGet]
        public IActionResult Get()
        {
            var GetReuslt = _context.Restaurants.ToList();
            var MapView = _mapper.Map<List<RestaurantModelView>>(GetReuslt);
            return Ok(MapView);
        }

        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var serachId = _context.Restaurants.Any(x => x.Id == id);
            if (!serachId) return BadRequest("Please Register Restaurant");
            var MapView = _mapper.Map<RestaurantModelView>(serachId);
            return Ok(MapView);
        }

        // POST api/<CustomerController>
        [HttpPost]
        public IActionResult AddRestaurant(RestaurantModelView restaurantModelView)
        {
            var Restaurant = new Restaurant()
            {
                Name = restaurantModelView.Name,
                PhoneNumber = restaurantModelView.PhoneNumber,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                Archived = restaurantModelView.Archived,
            };
            _context.Restaurants.Add(Restaurant);
            _context.SaveChanges();
            return Ok("Successfully added!");
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, RestaurantModelView restaurantModelView)
        {
            var serachId = _context.Restaurants.FirstOrDefault(x => x.Id == id);
            if (serachId == null) return BadRequest("Please Register Restaurant");
            serachId.Name = restaurantModelView.Name;
            serachId.PhoneNumber = restaurantModelView.PhoneNumber;
            serachId.Archived = restaurantModelView.Archived;
            serachId.UpdatedDate = DateTime.Now;
            _context.SaveChanges();
            return Ok("Edited successfully");
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var serachId = _context.Restaurants.FirstOrDefault(x => x.Id == id);
            if (serachId == null) return BadRequest("Customer is not registered");
            _context.Restaurants.Remove(serachId);
            _context.SaveChanges();
            return Ok("Deleted successfully");
        }
    }
}

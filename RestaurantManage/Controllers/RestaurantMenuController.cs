using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantManage.Models;
using RestaurantManage.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestaurantManage.Controllers
{
    [Route("api/[controller]")]
    [ApiController] 
    public class RestaurantMenuController : ControllerBase
    {
        private readonly restaurantdbContext _context;
        private readonly IMapper _mapper;
        public RestaurantMenuController(restaurantdbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        // GET: api/<CustomerController>
        [HttpGet]
        public IActionResult Get()
        {
            var GetReuslt = _context.Restaurantmenus.ToList();
            var MapView = _mapper.Map<List<RestaurantmenuModelView>>(GetReuslt);
            return Ok(MapView);
        }

        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var serachId = _context.Restaurantmenus.Any(x => x.RestaurantmenuId == id);
            if (!serachId) return BadRequest("Please Register RestaurantMenu");
            var MapView = _mapper.Map<RestaurantModelView>(serachId);
            return Ok(MapView);
        }

        // POST api/<CustomerController>
        [HttpPost]
        public IActionResult AddRestaurantMenu(RestaurantmenuModelView restaurantmenuModelView)
        {
            var Restaurantmenu = new Restaurantmenu()
            {
                Name = restaurantmenuModelView.Name,
                PriceInNis = restaurantmenuModelView.PriceInNis,
                Quantity = restaurantmenuModelView.Quantity,
                Archived = restaurantmenuModelView.Archived,
                PriceInUsd = (float)(restaurantmenuModelView.PriceInNis/3.5),
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                RestaurantId = restaurantmenuModelView.RestaurantId,
            };
            _context.Restaurantmenus.Add(Restaurantmenu);
            _context.SaveChanges();
            return Ok("Successfully added!");
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, RestaurantmenuModelView restaurantmenuModelView)
        {
            var serachId = _context.Restaurantmenus.FirstOrDefault(x => x.RestaurantmenuId == id);
            if (serachId == null) return BadRequest("Please Register Restaurant");
            serachId.Name = restaurantmenuModelView.Name;
            serachId.PriceInNis = restaurantmenuModelView.PriceInNis;
            serachId.Quantity = restaurantmenuModelView.Quantity;
            serachId.Archived = restaurantmenuModelView.Archived;
            serachId.UpdatedDate = DateTime.Now;
               
            _context.SaveChanges();
            return Ok("Edited successfully");
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var serachId = _context.Restaurantmenus.FirstOrDefault(x => x.RestaurantmenuId == id);
            if (serachId == null) return BadRequest("Customer is not registered");
            _context.Restaurantmenus.Remove(serachId);
            _context.SaveChanges();
            return Ok("Deleted successfully");
        }
    }
}

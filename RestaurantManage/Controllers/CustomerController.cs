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
    public class CustomerController : ControllerBase
    {
        private readonly restaurantdbContext  _context;
        private  readonly IMapper _mapper;
        public CustomerController(restaurantdbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        // GET: api/<CustomerController>
        [HttpGet]
        public IActionResult Get()
        {
            var GetReuslt = _context.Customers.ToList();
            var MapView = _mapper.Map<List<CustomerModelView>>(GetReuslt);
            return Ok(MapView);
        }

        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var serachId = _context.Customers.Any(x => x.CustomerId == id);
            if (!serachId) return BadRequest("Please Register Customer");
            var MapView = _mapper.Map<CustomerModelView>(serachId);
            return Ok(MapView);
        }

        // POST api/<CustomerController>
        [HttpPost]
        public IActionResult AddCustomer(CustomerModelView customerModelView )
        {
            var customer = new Customer()
            {
                FirstName = customerModelView.FirstName,
                LastName = customerModelView.LastName,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                Archived = customerModelView.Archived,
            };
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return Ok("Successfully added!");
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, CustomerModelView customerModelView)
        {
            var serachId = _context.Customers.FirstOrDefault(x => x.CustomerId == id);
            if (serachId == null) return BadRequest("Please Register Customer");
            serachId.FirstName = customerModelView.FirstName;
            serachId.LastName = customerModelView.LastName;
            serachId.Archived = customerModelView.Archived;
            serachId.UpdatedDate = DateTime.Now;
            _context.SaveChanges();
            return Ok("Edited successfully");
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var serachId = _context.Customers.FirstOrDefault(x => x.CustomerId == id);
            if (serachId==null) return BadRequest("Customer is not registered");
            _context.Customers.Remove(serachId);
            _context.SaveChanges();
            return Ok("Deleted successfully");
        }
    }
}

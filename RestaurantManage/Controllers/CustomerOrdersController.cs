using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using RestaurantManage.Extention;
using RestaurantManage.Models;
using RestaurantManage.ModelView;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestaurantManage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerOrdersController : ControllerBase
    {
        private readonly restaurantdbContext _context;
          
        public CustomerOrdersController(restaurantdbContext context)
        {
            _context = context;
        }
        private bool isAvailable(int id)
        {
            var check = _context.Restaurantmenus.FirstOrDefault(x => x.RestaurantmenuId == id);
            if (check.Quantity > 0)
            {
                return true;
            }
            return false;
        }

        // POST api/<OrderController>
        [HttpPost]
        public IActionResult CreateCustomerOrder(OrdersModelView ordersModelView)
        {
            var check = isAvailable(ordersModelView.RestaurantmenuId);
            if (!check) 
            { 
                return BadRequest("Quantity is sold out"); 
            }
            var CustomerOrder = new Restaurantmenucustomer()
            {
                RestaurantmenuId = ordersModelView.RestaurantmenuId,
                CustomerId = ordersModelView.CustomerId,
            };
            _context.Restaurantmenucustomers.Add(CustomerOrder);
            _context.SaveChanges();
            return Ok("Successfully added!");
        }

        // PUT api/<OrderController>/5
        [HttpPut("{id}")]
        
        public IActionResult Update(int id, OrdersModelView ordersModelView)
        {
            var serachId = _context.Restaurantmenucustomers.FirstOrDefault(x => x.Id == id);

            if (serachId == null) return BadRequest("Order not found");

            var check = isAvailable(ordersModelView.RestaurantmenuId);
            if (!check) return BadRequest("Not available now");

            serachId.CustomerId = ordersModelView.CustomerId;
            serachId.RestaurantmenuId = ordersModelView.RestaurantmenuId;
            _context.SaveChanges();

            return Ok("Edited successfully");
        }

        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var serachId = _context.Restaurantmenucustomers.FirstOrDefault(x => x.Id == id);
            if (serachId == null) return BadRequest("Your request does not exist");
            _context.Restaurantmenucustomers.Remove(serachId);
            _context.SaveChanges();
            return Ok("Deleted successfully");
        }

        [HttpGet]
        [Route("CSVReport")]
        public IActionResult Get()
        {
            var Queres = _context.CSVReports.ToList();
            foreach (var item in Queres)
            {
                item.RestaurantName.CapitalizStr();
            }
          var csvpath = Path.Combine(Environment.CurrentDirectory, $" CSVReport.csv");
            using (var writer = new StreamWriter(csvpath))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(Queres);
            }

            return Ok("Done");
        }
    }
}

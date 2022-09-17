using System;
using System.Collections.Generic;

#nullable disable

namespace RestaurantManage.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Restaurantmenucustomers = new HashSet<Restaurantmenucustomer>();
        }

        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }


        public bool Archived { get; set; }

        public virtual ICollection<Restaurantmenucustomer> Restaurantmenucustomers { get; set; }
    }
}

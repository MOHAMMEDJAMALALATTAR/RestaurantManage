using System;
using System.Collections.Generic;

#nullable disable

namespace RestaurantManage.Models
{
    public partial class Restaurantmenucustomer
    {
        public int Id { get; set; }
        public int RestaurantmenuId { get; set; }
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Restaurantmenu Restaurantmenu { get; set; }
    }
}

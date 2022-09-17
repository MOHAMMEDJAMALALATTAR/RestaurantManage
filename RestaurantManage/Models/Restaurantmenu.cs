using System;
using System.Collections.Generic;

#nullable disable

namespace RestaurantManage.Models
{
    public partial class Restaurantmenu
    {
        public Restaurantmenu()
        {
            Restaurantmenucustomers = new HashSet<Restaurantmenucustomer>();
        }

        public int RestaurantmenuId { get; set; }
        public string Name { get; set; }
        public float PriceInNis { get; set; }
        public float PriceInUsd { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Archived { get; set; }
        public int RestaurantId { get; set; }

        public virtual Restaurant Restaurant { get; set; }
        public virtual ICollection<Restaurantmenucustomer> Restaurantmenucustomers { get; set; }
    }
}

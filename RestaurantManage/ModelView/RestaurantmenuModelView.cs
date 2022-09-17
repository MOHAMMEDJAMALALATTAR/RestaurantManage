using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantManage.ModelView
{
    public class RestaurantmenuModelView
    {
        public string Name { get; set; }
        public float PriceInNis { get; set; }
        public int Quantity { get; set; }
        public bool Archived { get; set; }
        public int RestaurantId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantManage.ModelView
{
    public class CSVReport
    {
        public string RestaurantName {get; set;}
        public long NumberOfOrderedCustomer { get; set; }
        public float ProfitInUsd { get; set; }
        public float ProfitInNis { get; set; }

        public long TheBestSellingMeal { get; set; }
        public long MostPurchasedCustomer { get; set; }

    }
}

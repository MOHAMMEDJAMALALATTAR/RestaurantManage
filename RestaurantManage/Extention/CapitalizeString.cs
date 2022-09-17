using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantManage.Extention
{
    public static class CapitalizeString
    {

       public  static void CapitalizStr( this string str)
        {
             

            if (str.Length == 0)
                System.Console.WriteLine("Empty String");
            else if (str.Length == 1)
                System.Console.WriteLine(char.ToUpper(str[0]));
            else
                System.Console.WriteLine(char.ToUpper(str[0]) + str.Substring(1));
        }
    }
}

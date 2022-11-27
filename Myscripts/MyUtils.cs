using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkToDo.Myscripts
{
    internal class MyUtils
    {
        public static void Swap<T>(ref T x, ref T y)
        {
            T temp;
            temp = x;
            x = y;
            y = temp;
        }
        public static string genUUID()
        {
            Guid myUUId = Guid.NewGuid();
            string convertedUUID = myUUId.ToString();
            Console.WriteLine("Current UUID is: " + convertedUUID);
            return convertedUUID;
        }
    }
}

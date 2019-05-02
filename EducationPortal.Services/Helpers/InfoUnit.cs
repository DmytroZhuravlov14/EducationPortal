using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Services.Helpers
{
    public static class InfoUnit
    {
        public static object GetInfoUnit(object obj, Func<object, bool> predicate, string message, string errorMessage)
        {
            Console.WriteLine(message);

            while (true)
            {
                obj = Console.ReadLine();
                if (predicate.Invoke(obj))
                {
                    break;
                }
                else
                {
                    Console.WriteLine(errorMessage);
                }
            }

            return obj;
        }
    }
}

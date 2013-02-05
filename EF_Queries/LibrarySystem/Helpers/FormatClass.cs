using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace LibrarySystem.Helpers
{
    public static class FormatClass<T> where T: class
    {
        public static void ForDisplay(T myClass)
        {
            //  Get the class name
            Type t = typeof(T);

            string className = t.Name;
            PropertyInfo[] props = t.GetProperties();
            foreach (PropertyInfo p in props)
            {
                Console.WriteLine("Name:\t{0}", p.Name);
                Console.WriteLine("Type:\t{0}", p.GetType().ToString());
            }
        }
    }
}

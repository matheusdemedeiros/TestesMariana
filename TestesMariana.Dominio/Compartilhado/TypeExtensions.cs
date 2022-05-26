using System;
using System.Linq;
using System.Reflection;

namespace TestesMariana.Dominio.Compartilhado
{
    public static class TypeExtensions
    {
        public static PropertyInfo[] GetFilteredProperties(this Type type)
        {
            return type.GetProperties().Where(pi => pi.GetCustomAttributes(typeof(SkipPropertyAttribute), true).Length == 0).ToArray();
        }
    }
}

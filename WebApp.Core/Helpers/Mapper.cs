using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace WebApp.Core.Helpers
{
    public static class Mapper
    {
        public static T Map<F, T>(F from) where T : new()
        {
            var to = new T();

            var propertiesFrom = typeof(F).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .ToDictionary(p => p.Name, p => p);
            var propertiesTo = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .ToDictionary(p => p.Name, p => p);

            foreach (var p in propertiesFrom)
            {
               if(propertiesTo.TryGetValue(p.Key, out PropertyInfo property))
                {
                    property.SetValue(to, p.Value.GetValue(from));
                }
            }

            return to;
        }
    }
}

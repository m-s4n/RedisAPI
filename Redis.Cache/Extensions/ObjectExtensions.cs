using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace Redis.Cache.Extensions
{
    public static class ObjectExtensions
    {
        public static HashEntry[] ToHashEntries(this object obj)
        {
            PropertyInfo[] properties = obj.GetType().GetProperties();
            return properties
        .Where(x => x.GetValue(obj) != null)  
        .Select
        (
              property =>
              {
                  object propertyValue = property.GetValue(obj);
                  string hashValue;

                  
                  if (propertyValue is IEnumerable<object>)
                  {
                      
                      hashValue = JsonConvert.SerializeObject(propertyValue);
                  }
                  else
                  {
                      hashValue = propertyValue.ToString();
                  }

                  return new HashEntry(property.Name, hashValue);
              }
        )
        .ToArray();
        }
    }
}

using System;
using System.Reflection;

namespace CSVWriter.Extensions
{
    public static class PropertyExtension
    {
        public static void SetPropertyValue<T>(this object obj, string propertyName, T propertyValue) where T : IConvertible
        {
            var propertyInfo = obj.GetType().GetProperty( propertyName );
        
            if(propertyInfo != null && propertyInfo.CanWrite)
            {
                propertyInfo.SetValue(obj, Convert.ChangeType(propertyValue, propertyInfo.PropertyType),null);
            }
        }
    }
}
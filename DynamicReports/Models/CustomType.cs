using System;
using System.Collections.Generic;
using System.Reflection;

namespace DynamicReports.Models
{
    public class CustomType : ICustomTypeProvider
    {
        readonly CustomTypeHelper<CustomType> _helper = new CustomTypeHelper<CustomType>();

        public static void AddProperty(String name)
        {
            CustomTypeHelper<CustomType>.AddProperty(name);
        }

        public static void AddProperty(String name, Type propertyType)
        {
            CustomTypeHelper<CustomType>.AddProperty(name, propertyType);
        }

        public static void AddProperty(String name, Type propertyType, List<Attribute> attributes)
        {
            CustomTypeHelper<CustomType>.AddProperty(name, propertyType, attributes);
        }


        public void SetPropertyValue(string propertyName, object value)
        {
            _helper.SetPropertyValue(propertyName, value);
        }

        public object GetPropertyValue(string propertyName)
        {
            return _helper.GetPropertyValue(propertyName);
        }

        public PropertyInfo[] GetProperties()
        {
            return _helper.GetProperties();
        }

        public Type GetCustomType()
        {
            return _helper.GetCustomType();
        }
    }
}

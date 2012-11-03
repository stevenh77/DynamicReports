using System;
using System.Collections.Generic;
using System.Json;
using System.Linq;
using System.Reflection;

namespace DynamicReports.Models
{

    public class JsonHelper<T> where T : ICustomTypeProvider
    {
        private readonly IEnumerable<string> _keys = Enumerable.Empty<string>();
        private readonly Dictionary<string, Func<object, object>> _converters = new Dictionary<string, Func<object, object>>();

        public JsonHelper(IDictionary<string, JsonValue> template)
        {
            CustomTypeHelper<T>.ClearProperties();

            _keys = (from k in template.Keys select k).ToArray();

            foreach (var key in template.Keys)
            {
                int integerTest;
                double doubleTest;
                var value = template[key].ToString();
                if (int.TryParse(value, out integerTest))
                {
                    CustomTypeHelper<T>.AddProperty(key, typeof(int));
                    _converters.Add(key, obj => int.Parse(obj.ToString()));
                }
                else if (double.TryParse(value, out doubleTest))
                {
                    CustomTypeHelper<T>.AddProperty(key, typeof(double));
                    _converters.Add(key, obj => double.Parse(obj.ToString()));
                }
                else
                {
                    CustomTypeHelper<T>.AddProperty(key, typeof(string));
                    _converters.Add(key, obj =>
                    {
                        // strip quotes
                        var str = obj.ToString().Substring(1);
                        return str.Substring(0, str.Length - 1);
                    });
                }
            }
        }

        public void MapJsonObject(Action<string, object> setValue, JsonValue item)
        {
            foreach (var key in _keys)
            {
                setValue(key, _converters[key](item[key]));
            }
        }
    }
}

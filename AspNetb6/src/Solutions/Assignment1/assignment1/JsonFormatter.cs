using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace assignment1
{
    public class JsonFormatter
    {
        public static string Convert<T>(T item)
        {
            StringBuilder json = new StringBuilder();

            Type type = item.GetType();

            IEnumerable<PropertyInfo> propertyInfos = type.GetProperties();

            if(!item.GetType().IsPrimitive && item.GetType() != typeof(string))
            {
                var isArray = typeof(IEnumerable).IsAssignableFrom(item.GetType());

                //begins
                if (isArray)
                    json.Append("[");
                else
                    json.Append("{");

                //object properties serialization
                if (!isArray)
                {
                    foreach (PropertyInfo property in propertyInfos)
                    {
                        if (property.PropertyType == typeof(string))
                        {
                            json.Append($"\"{property.Name}\": \"{property.GetValue(item)}\", ");
                        }
                        else if (property.PropertyType == typeof(int) || property.PropertyType == typeof(double) || property.PropertyType == typeof(float))
                        {
                            json.Append($"\"{property.Name}\": {property.GetValue(item)}, ");
                        }
                        else if (property.PropertyType == typeof(DateTime))
                        {
                            json.Append($"\"{property.Name}\": \"{property.GetValue(item).ToString()}\", ");
                        }
                        else
                        {
                            //if property type is object the we call convert method again
                            json.Append($"\"{property.Name}\": {Convert(property.GetValue(item))}, ");
                        }

                        //Console.WriteLine(json.ToString());
                    }
                }
                else
                {
                    foreach (var i in item as IEnumerable)
                    {
                        if (i.GetType() == typeof(string) || i.GetType().IsPrimitive)
                            json.Append($"\"{i}\", ");
                        else
                            json.Append($"{Convert(i)}, ");
                    }
                }

                //remove ", " from the last at each iteration of Convert method
                json.Remove(json.ToString().Length - 2, 2);

                //ends
                if (isArray)
                    json.Append("]");
                else
                    json.Append("}");

                //Console.WriteLine(json.ToString());
            }
            else
            {
                json.Append(item.ToString());
                //Console.WriteLine(json.ToString());
            }

            return json.ToString();
        }
    }
}

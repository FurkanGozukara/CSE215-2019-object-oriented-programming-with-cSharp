using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace lecture_5_access_modifiers
{
    public static class getObjectProperties
    {
        public class PropertyInformation
        {
            public string Name { get; set; }
            public object Value { get; set; }
        }

        public static List<PropertyInformation> ObjectPropertyInformation(object obj)
        {
            var propertyInformations = new List<PropertyInformation>();

            FieldInfo[] fields = obj.GetType().GetFields(BindingFlags.Public |
                                               BindingFlags.NonPublic |
                                               BindingFlags.Instance);
            String str = "";
            foreach (FieldInfo f in fields)
            {
                propertyInformations.Add(new PropertyInformation { Name = f.Name, Value = f.GetValue(obj) });
            }

            foreach (var property in obj.GetType().GetProperties())
            {
                //for value types
                if (property.PropertyType.IsPrimitive || property.PropertyType.IsValueType || property.PropertyType == typeof(string))
                {
                    propertyInformations.Add(new PropertyInformation { Name = property.Name, Value = property.GetValue(obj) });
                }
                //for complex types
                else if (property.PropertyType.IsClass && !typeof(IEnumerable).IsAssignableFrom(property.PropertyType))
                {
                    propertyInformations.AddRange(ObjectPropertyInformation(property.GetValue(obj)));
                }
                //for Enumerables
                else
                {
                    var enumerablePropObj1 = property.GetValue(obj) as IEnumerable;

                    if (enumerablePropObj1 == null) continue;

                    var objList = enumerablePropObj1.GetEnumerator();

                    while (objList.MoveNext())
                    {
                        objList.MoveNext();
                        ObjectPropertyInformation(objList.Current);
                    }
                }
            }

            return propertyInformations;
        }

    }

    }

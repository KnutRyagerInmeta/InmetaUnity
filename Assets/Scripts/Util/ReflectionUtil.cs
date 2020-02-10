using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

/// <summary>
/// Handy reflection methods.
/// </summary>
public static class ReflectionUtil
{
    /// <summary>
    /// Check if a generic type is of a raw generic type.
    /// 
    /// Based on https://stackoverflow.com/questions/457676/check-if-a-class-is-derived-from-a-generic-class
    /// </summary>
    public static bool IsSubclassOfRawGeneric(Type generic, Type toCheck)
    {
        while (toCheck != null && toCheck != typeof(object))
        {
            var cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
            if (generic == cur)
            {
                return true;
            }
            toCheck = toCheck.BaseType;
        }
        return false;
    }

    /// <summary>
    /// Check whether the type is primitive, include Decimal/String.
    /// </summary>
    public static bool IsPrimitive(Type type) => type.IsPrimitive || type == typeof(decimal) || type == typeof(string) || type == typeof(Decimal) || type == typeof(String);

    /// <summary>
    /// Check if a type implements an interface. (Also supports generic interface).
    /// 
    /// Based on https://stackoverflow.com/questions/4963160/how-to-determine-if-a-type-implements-an-interface-with-c-sharp-reflection
    /// </summary>
    public static bool ImplementsInterface(Type type, Type _interface) => type.GetInterfaces().Any(i => i == _interface || i.IsGenericType && i.GetGenericTypeDefinition() == _interface);

    /// <summary>
    /// Check if a type implements List.
    /// </summary>
    public static bool IsList(this Type type) => type.IsGenericType && (type.GetGenericTypeDefinition() == typeof(List<>));

    /// <summary>
    /// Cet the generic type of a collection.
    /// </summary>
    public static Type GetGenericType<T>(ICollection<T> collection) => collection.GetType().GetGenericArguments()[0];

    /// <summary>
    /// Get the first found base interface of given type. Null if none found.
    /// </summary>
    public static Type GetBaseInterface(Type type, Type baseInterface)
    {
        foreach (var _interface in type.GetInterfaces())
        {
            if (_interface.GetGenericTypeDefinition() == baseInterface)
            {
                return _interface;
            }
        }
        return null;
    }

    /// <summary>
    /// Get the first generic argument found for type. ArgumentException if none found.
    /// </summary>
    public static Type GetGenericArgument(Type type)
    {
        var genericArguments = type.GetGenericArguments();
        if (genericArguments.Count() == 0)
        {
            throw new ArgumentException("No generic arguments found for type " + type + ".");
        }
        return genericArguments[0];
    }

    /// <summary>
    /// Get the default value for a type.
    /// </summary>
    public static T GetDefault<T>() => (T)GetDefault(typeof(T));

    /// <summary>
    /// Get the default value for a type.
    /// 
    /// Based on https://stackoverflow.com/questions/325426/programmatic-equivalent-of-defaulttype
    /// </summary>
    public static object GetDefault(Type type) => type.IsValueType ? Activator.CreateInstance(type) : null;

    /// <summary>
    /// Get generic argument found in found Base interface.
    /// </summary>
    public static Type GetGenericArgument(Type type, Type baseInterface) => GetGenericArgument(GetBaseInterface(type, baseInterface));

    /// <summary>
    /// Get the first found property of the given type. If none found, ArgumentException.
    /// </summary>
    public static PropertyInfo GetPropertyOfType<PROPERTY>(object obj) => GetPropertyOfType<PROPERTY>(obj.GetType());

    /// <summary>
    /// Get the first found property of the given type. If none found, ArgumentException.
    /// </summary>
    public static PropertyInfo GetPropertyOfType<OBJECT, PROPERTY>() => GetPropertyOfType<PROPERTY>(typeof(OBJECT));

    /// <summary>
    /// Get the value of the first found property of the given type. If none found, ArgumentException.
    /// </summary>
    public static PROPERTY GetPropertyValueOfType<PROPERTY>(object obj) => (PROPERTY)GetPropertyOfType<PROPERTY>(obj.GetType()).GetValue(obj);

    /// <summary>
    /// Get the first found property of the given type. If none found, ArgumentException.
    /// </summary>
    public static PropertyInfo GetPropertyOfType<PROPERTY>(Type objectType) => GetPropertyOfType(objectType, typeof(PROPERTY));

    /// <summary>
    /// Get the first found property of the given type. If none found, ArgumentException.
    /// </summary>
    public static PropertyInfo GetPropertyOfType(Type objectType, Type propertyType)
    {
        var properties = objectType.GetProperties();
        foreach (var property in properties)
        {
            if (property.PropertyType == propertyType)
            {
                return property;
            }
        }
        throw new ArgumentException("No property of type " + propertyType + " for " + objectType);
    }

    /// <summary>
    /// Get the first found property that has the given attribute. ArgumentException if none found.
    /// </summary>
    public static PropertyInfo GetPropertyOfAttribute<OBJECT, ATTRIBUTE>() where ATTRIBUTE : Attribute
    {
        var objectType = typeof(OBJECT);
        var attributeType = typeof(ATTRIBUTE);
        var properties = objectType.GetProperties();
        foreach (var property in properties)
        {
            if (property.HasAttribute<ATTRIBUTE>())
            {
                return property;
            }
        }
        throw new ArgumentException("No property with attribute of type " + attributeType + " for " + objectType);
    }

    //public static PropertyInfo GetPropertyOfGenericAttribute<OBJECT,ATTRIBUTE,ATTRIBUTE_TYPE>() where ATTRIBUTE : Attribute
    //{
    //    Type objectType = typeof(OBJECT);
    //    Type attributeType = typeof(ATTRIBUTE);
    //    PropertyInfo[] properties = objectType.GetProperties();
    //    foreach(PropertyInfo property in properties)
    //    {
    //        object[] correctAttributes = property.GetCustomAttributes(attributeType, false);
    //        if (correctAttributes.Length > 0)
    //        {
    //            ATTRIBUTE attribute = (ATTRIBUTE)correctAttributes[0];
    //            attribute.

    //            return property;
    //        }
    //    }
    //    throw new ArgumentException("No property with attribute of type " + attributeType + " for " + objectType);
    //}
}

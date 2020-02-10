using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;

public static class CopyUtil
{
    /// <summary>
    /// Based on https://stackoverflow.com/questions/129389/how-do-you-do-a-deep-copy-of-an-object-in-net-c-specifically
    /// </summary>
    public static T DeepClone<T>(T obj) where T : new()
    {
        if (obj == null)
        {
            return default(T);
        }
        using (var ms = new MemoryStream())
        {
            var formatter = new BinaryFormatter();
            formatter.Serialize(ms, obj);
            ms.Position = 0;

            return (T)formatter.Deserialize(ms);
        }
    }

    /// <summary>
    /// Copies non-null values from source to target.
    /// 
    /// Based on https://stackoverflow.com/questions/13212498/loop-through-an-object-and-find-the-not-null-properties
    /// </summary>
    public static void CopyNonNullProperties<T>(T source, T target) where T : new()
    {
        foreach (var prop in source.GetType()
                                   .GetProperties(BindingFlags.Instance |
                                                  BindingFlags.Public)
                                   .Where(p => !p.GetIndexParameters().Any())
                                   .Where(p => p.CanRead && p.CanWrite))
        {
            var value = prop.GetValue(source, null);
            if (value != null)
            {
                prop.SetValue(target, value, null);
            }
        }
    }

    /// <summary>
    /// Copies non-null/0 (default) values from source to target.
    /// 
    /// Based on https://stackoverflow.com/questions/13212498/loop-through-an-object-and-find-the-not-null-properties
    /// </summary>
    public static void CopyNonDefaultProperties<T>(T source, T target) where T : new()
    {
        foreach (var prop in source.GetType()
                                   .GetProperties(BindingFlags.Instance |
                                                  BindingFlags.Public)
                                   .Where(p => !p.GetIndexParameters().Any())
                                   .Where(p => p.CanRead && p.CanWrite))
        {
            var value = prop.GetValue(source, null);
            if (value != ReflectionUtil.GetDefault(prop.PropertyType))
            {
                prop.SetValue(target, value, null);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Linq.Expressions;
using System.Reflection;

public static class Extensions {

    public static void Init<U, T>(this U mono) where T : Component where U : MonoBehaviour
    {
            mono.SetFirst(mono.GetComponentInChildren<T>());
    }
    public static void Copy3<T, U>(this T obj) where T : MonoBehaviour, new() => obj.SetFirst(obj.GetComponentInChildren<T>());

    /// <summary>
    /// Get the value of the first found property of the given type. If none found, ArgumentException.
    /// </summary>
    public static T Get<T>(this object obj) => ReflectionUtil.GetPropertyValueOfType<T>(obj);

    public static T Get<U, T>(this U obj, Expression<Func<U, T>> property)
    {
        var propertyName = Prop<U, T>(property.Body as MemberExpression);
        return (T)typeof(U).GetProperty(propertyName).GetValue(obj);
    }

    /// <summary>
    /// Set the first found property of type T to value.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static void SetFirst<T>(this object obj, T value) => obj.SetFirst(typeof(T), value);

    public static void SetFirst(this object obj, Type type, object value) => ReflectionUtil.GetPropertyOfType(obj.GetType(), type).SetValue(obj, value);

    public static void Run<T>(this T[] objects, Action<T> action)
    {
        foreach (var obj in objects)
        {
            action.Invoke(obj);
        }
    }

    /// <summary>
    /// Get a deep copy of the object (if serializable).
    /// </summary>
    public static T Copy<T>(this T obj) where T : new() => CopyUtil.DeepClone(obj);

    /// <summary>
    /// Copy an object, and set a value to something new.
    /// </summary>
    public static T Change<T, U>(this T obj, Expression<Func<T, U>> property, U newValue) where T : new() => obj.Copy().Set(property, newValue);

    /// <summary>
    /// Set the given property to the default value for it's type.
    /// </summary>
    public static T Clear<T, U>(this T obj, Expression<Func<T, U>> property)
    {
        obj.Set(property, ReflectionUtil.GetDefault<U>());
        return obj;
    }

    /// <summary>
    /// Copy an object, and perform an action on it.
    /// </summary>
    public static T Act<T>(this T obj, Action<T> action) where T : new()
    {
        T copy = Copy(obj);
        action(copy);
        return copy;
    }

    /// <summary>
    /// Set a property value from a lambda reference used as Property reference.
    /// </summary>
    public static U Set<U, T>(this U obj, Expression<Func<U, T>> property, T value)
    {
        ReflectionProp<U, T>(property.Body as MemberExpression).SetValue(obj, value);
        return obj;
    }

    /// <summary>
    /// Convert a lambda expression of a member property to the string name of that property.
    /// 
    /// Use this for property references for safer refactoring.
    /// </summary>
    public static string Prop<U, T>(this U obj, Expression<Func<U, T>> member) => Prop<U, T>(member.Body as MemberExpression);

    /// <summary>
    /// Convert a lambda expression of a member property to the string name of that property.
    /// 
    /// Use this for property references for safer refactoring.
    /// </summary>
    public static string Prop<U, T>(this U obj, Expression<Func<T>> member) => Prop<U, T>(member.Body as MemberExpression);

    public static PropertyInfo ReflectionProp<U, T>(MemberExpression memberExpression)
    {
        var propertyName = Prop<U, T>(memberExpression);
        var result = typeof(U).GetProperty(propertyName);
        return result;
    }

    public static string Prop<U, T>(MemberExpression memberExpression)
    {
        if (memberExpression != null)
        {
            var memberInfo = memberExpression.Member;
            if (memberInfo == null)
            {
                throw new ArgumentException("Not a valid member!");
            }
            return memberInfo.Name;
        }
        else
            throw new ArgumentException("member body must be a MemberExpression");
    }

    public static string Prop(string lambdaExpression)
    {
        var splitted = lambdaExpression.Split(' ');
        return splitted[splitted.Length - 1];
    }

    /// <summary>
    /// Convert a lambda expression of a member property path to the string of that property path.
    /// 
    /// Use this for property path references for safer refactoring.
    /// </summary>
    //public static string Path<U, T>(this U obj, Expression<Func<U, T>> member) => !(member.Body is MemberExpression body) ? Path<U>(member.ToString()) : Path<U, T>(body);

    /// <summary>
    /// Convert a lambda expression of a member property path to the string of that property path.
    /// 
    /// [] will be replaced with [{index}].
    /// 
    /// Use this for property path references for safer refactoring.
    /// </summary>
    //public static string Path<U, T>(this U obj, Expression<Func<U, T>> member, int index) => obj.Path(member).Replace("[]", "[" + index + "]");

    /// <summary>
    /// Convert a lambda expression of a member property path to the string of that property path.
    /// 
    /// Use this for property path references for safer refactoring.
    /// </summary>
    //public static string Path<U, T>(this U obj, Expression<Func<T>> member) => Path<U, T>(member.Body as MemberExpression);

    //public static string Path<U, T>(MemberExpression memberExpression)
    //{
    //    if (memberExpression != null)
    //    {
    //        return Path<U>(memberExpression.ToString());
    //    }
    //    else
    //        throw new ArgumentException("member body must be a MemberExpression");
    //}

    /// <summary>
    /// Calculates the String path of a lamba expression. Works with list syntax. Use -1 for no list index.
    /// </summary>
    //private static string Path<U>(string lambdaExpressionPath)
    //{
    //    var sb = new StringBuilder();
    //    string basePathName;
    //    string[] splittedPath;
    //    if (lambdaExpressionPath.Contains("()") || lambdaExpressionPath.Contains("null As"))
    //    {
    //        splittedPath = lambdaExpressionPath.Split(").");
    //        var spaceSplit = lambdaExpressionPath.Split(" ");
    //        basePathName = spaceSplit[spaceSplit.Length - 1].Split(")")[0].Split("(")[0];
    //    }
    //    else
    //    {
    //        basePathName = typeof(U).Name;
    //        splittedPath = lambdaExpressionPath.Split(".", 2);
    //    }
    //    sb.Append(NonDtoName(basePathName)).Append(".").Append(splittedPath[1]);
    //    for (var i = 2; i < splittedPath.Length; i++)
    //    {
    //        sb.Append(").").Append(splittedPath[i]);
    //    }
    //    sb.Replace(".get_Item(", "[");
    //    sb.Replace(")", "]");
    //    sb.Replace("-1", "");
    //    return sb.ToString();
    //}

    private static string NonDtoName<T>() => NonDtoName(typeof(T).Name);

    private static string NonDtoName(string name) => name.EndsWith("DTO") ? name.Substring(0, name.Length - 3) : name;

    /// <summary>
    /// Check for an attribute.
    /// </summary>
    public static bool HasAttribute<T>(this PropertyInfo propertyInfo) where T : Attribute => propertyInfo.GetCustomAttribute<T>() != null;

    /// <summary>
    /// For case insensitive "LIKE" query.
    /// 
    /// Should be translated to SQL, see: https://forums.devart.com/viewtopic.php?t=14937
    /// </summary>
    public static bool ContainsIgnorecase(this string src, string toCheck) => src != null && src.IndexOf(toCheck, StringComparison.OrdinalIgnoreCase) >= 0;

}

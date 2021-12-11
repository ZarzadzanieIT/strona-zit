using System.Reflection;

namespace ZIT.Infrastructure.Common;

internal static class ReflectionExtensions
{
    internal static IEnumerable<T> GetAllConstStringFieldsWithFlattenedNestedTypes<T>(this Type type)
    {
        var publicStaticFields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);

        var literalFieldsOfType = publicStaticFields.Where(x => x.IsLiteral && x.FieldType == typeof(T));

        var rawValues = literalFieldsOfType.Select(x => (T) x.GetRawConstantValue()!);
        return rawValues.Concat(type.GetNestedTypes().SelectMany(x => x.GetAllConstStringFieldsWithFlattenedNestedTypes<T>()));
    }
}
using ZIT.Infrastructure.Authorization;
using ZIT.Infrastructure.Common;

namespace ZIT.Infrastructure.Services;

public interface IEntitlementsProvider
{
    string[] GetEntitlementsForType<T>() where T : IEntitlementGroup;
}

public class EntitlementsProvider : IEntitlementsProvider
{
    private readonly Dictionary<Type, string[]> _entitlements;
    public EntitlementsProvider()
    {
        _entitlements = new Dictionary<Type, string[]>();
        var entitlementGroupType = typeof(IEntitlementGroup);
        var entitlementsGroups = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).Where(x => entitlementGroupType.IsAssignableFrom(x));
        entitlementsGroups
            .ToList()
            .ForEach(type =>
            {
                _entitlements.Add(type, type.GetAllConstStringFieldsWithFlattenedNestedTypes<string>().ToArray());
            });
    }

    public string[] GetEntitlementsForType<T>() where T : IEntitlementGroup
        => _entitlements.GetValueOrDefault(typeof(T)) ?? Array.Empty<string>();
}
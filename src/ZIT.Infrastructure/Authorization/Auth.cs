namespace ZIT.Infrastructure.Authorization;

public interface IEntitlementGroup
{
}
public class Auth
{
    public static class Claim
    {
        public const string Type = "Entitlement";
    }

    public class Entitlements : IEntitlementGroup
    {
        public const string Default = "ZIT-DefaultEntitlement";
        public const string Panel = "ZIT-PanelEntitlement";

        public class Users : IEntitlementGroup
        {
            public const string All = "ZIT-UsersEntitlement.*";
            public const string Read = "ZIT-UsersEntitlement.Read";
            public const string Write = "ZIT-UsersEntitlement.Write";
        }
    }
}
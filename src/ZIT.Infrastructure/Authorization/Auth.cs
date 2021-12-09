namespace ZIT.Infrastructure.Authorization;

public class Auth
{
    public static class Claim
    {
        public const string Type = "Entitlement";
    }

    public class Entitlements
    {
        public const string Panel = "ZIT-PanelEntitlement";
    }
}
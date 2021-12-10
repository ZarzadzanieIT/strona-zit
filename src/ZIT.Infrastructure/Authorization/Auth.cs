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

        public class Users
        {
            public const string All = "ZIT-UsersEntitlement.*";
            public const string Read = "ZIT-UsersEntitlement.Read";
            public const string Write = "ZIT-UsersEntitlement.Write";

        }
        //public const string Users = "ZIT-UsersEntitlement";
        //public const string UsersWrite = "ZIT-UsersEntitlement.Write";
    }
}
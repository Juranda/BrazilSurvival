public static class AuthorizationPolicies
{
    public const string ADMINISTRATOR = "Administrator";
    public const string PLAYER = "Player";
    public const string ADMINISTRATOR_OR_PLAYER = ADMINISTRATOR + "," + PLAYER;
}
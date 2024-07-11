namespace Restaurants.Infrastructure.Authorizarion;

public static class PolicyName
{
    public const string HasNationality = "HasNationality";
    public const string AtLeast20 = "AtLeast20";
    public const string AtLeast2Restaurants = "AtLeast2Restaurants";
}
public static class AppClaimTypes
{
    public const string Nationality = "Nationality";
    public const string DateOfBirth = "DateOfBirth";

}

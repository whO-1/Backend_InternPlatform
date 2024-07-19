namespace internPlatform.Domain.interfaces
{
    public interface ILocation
    {
        string LocationAddress { get; set; }
        double? Latitude { get; set; }
        double? Longitude { get; set; }
    }
}

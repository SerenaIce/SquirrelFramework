namespace SquirrelFramework.Domain.Model
{
    public abstract class DomainModel
    {
        public string Id { get; set; }
        public Geolocation Geolocation { get; set; }
    }
}

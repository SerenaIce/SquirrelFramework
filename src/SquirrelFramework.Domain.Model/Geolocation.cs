namespace SquirrelFramework.Domain.Model
{
    public class Geolocation
    {
        public Geolocation()
        {
        }

        public Geolocation(double longitude, double latitude)
        {
            this.Longitude = longitude;
            this.Latitude = latitude;
        }

        public Geolocation(double longitude, double latitude, double altitude)
        {
            this.Longitude = longitude;
            this.Latitude = latitude;
            this.Altitude = altitude;
        }

        // 经度
        public double Longitude { get; set; }

        // 纬度
        public double Latitude { get; set; }

        // 海拔
        public double Altitude { get; set; }

        public string ToDisplayString()
        {
            return string.Format($"{this.Longitude}, {this.Latitude}");
        }
    }
}
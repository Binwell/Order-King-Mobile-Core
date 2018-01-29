using System;
using System.Diagnostics;

namespace OrderKingCoreDemo.DAL.DataObjects
{
    [DebuggerDisplay("Position: {Latitude},{Longitude}")]
    public struct PositionObject
    {
        public static PositionObject Empty => new PositionObject();

        public PositionObject(double latitude, double longitude)
		{
			Latitude = Math.Min(Math.Max(latitude, -90.0), 90.0);
			Longitude = Math.Min(Math.Max(longitude, -180.0), 180.0);
		}

		public double Latitude { get; set; }

	    public double Longitude { get; set; }

	    public bool IsZero => (Math.Abs(Latitude*Longitude) < 0.000000001);

	    public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
				return false;
			if (obj.GetType() != GetType())
				return false;
			var other = (PositionObject)obj;
			return Math.Abs(Latitude - other.Latitude) < 0.000000001 && Math.Abs(Longitude - other.Longitude) < 0.000000001;
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = Latitude.GetHashCode();
				hashCode = (hashCode * 397) ^ Longitude.GetHashCode();
				return hashCode;
			}
		}

		public static bool operator ==(PositionObject left, PositionObject right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(PositionObject left, PositionObject right)
		{
			return !Equals(left, right);
		}

        public override string ToString()
        {
            return $"Position: {Latitude},{Longitude}, IsZero: {IsZero}";
        }

        public void Invert()
        {
            var lat = Latitude;
            var lon = Longitude;
            Latitude = lon;
            Longitude = lat;
        }
	}
}
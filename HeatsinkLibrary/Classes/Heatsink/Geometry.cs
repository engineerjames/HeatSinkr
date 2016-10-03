
namespace HeatSinkr.Library
{
	public abstract class Geometry<T>
	{
		public abstract double GetPitch();

		public abstract double GetVolume();

		public abstract T GeometryDetails { get; set; }

		public abstract double GetSurfaceArea();

        public abstract double GetCharacteristicLength();

        public abstract double AspectRatio();
	}
}

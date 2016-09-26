using System;

namespace HeatSinkr.Library
{
	public abstract class Geometry<T>
	{
		public abstract double GetPitch();

		public abstract double GetVolume();

		public abstract T GetGeometryParameters();

		public abstract double GetSurfaceArea();
	}
}

using System;

namespace HeatSinkr.Library
{
	public class PlateFinGeometry : Geometry<PlateFinGeometryParameters>
	{
		PlateFinGeometryParameters Geometry;

		public override double GetPitch()
		{
			var gp = Geometry;

			return (gp.Width - (gp.NumberOfFins * gp.FinThickness)) / (gp.NumberOfFins - 1);
		}

		public PlateFinGeometry(PlateFinGeometryParameters Parameters)
		{
			this.Geometry = Parameters;
		}



		public override double GetVolume()
		{
			return Geometry.FlowLength * Geometry.Height * Geometry.Width;
		}

		public override PlateFinGeometryParameters GetGeometryParameters()
		{
			return Geometry;
		}
	}
}

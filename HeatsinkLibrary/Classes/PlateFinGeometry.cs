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

        public override double GetSurfaceArea()
        {
            var gm = GetGeometryParameters();

            double firstTerm = 2.0 * gm.FinHeight * gm.FlowLength * gm.NumberOfFins;
            double secondTerm = gm.NumberOfFins * (gm.FinThickness * gm.FlowLength);
            double thirdTerm = (gm.NumberOfFins - 1) * GetPitch() * gm.FlowLength;
            double fourthTerm = 2 * gm.BaseThickness * gm.Width;
            double fifthTerm = gm.NumberOfFins * (gm.FinThickness * gm.FinHeight);
            double sixthTerm = 2 * gm.BaseThickness * gm.FlowLength;

            return (firstTerm + secondTerm + thirdTerm + fourthTerm + fifthTerm + sixthTerm);
        }
    }
}

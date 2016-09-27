using System;

namespace HeatSinkr.Library
{
	public class PlateFinGeometry : Geometry<PlateFinGeometryParameters>
	{
		public override PlateFinGeometryParameters GeometryDetails { get; set; }

		public override double GetPitch()
		{
			var gp = GeometryDetails;

			return (gp.Width - (gp.NumberOfFins * gp.FinThickness)) / (gp.NumberOfFins - 1);
		}

		public PlateFinGeometry(PlateFinGeometryParameters Parameters)
		{
			this.GeometryDetails = Parameters;
		}
        
        /// <summary>
        /// Units of mm^3
        /// </summary>
        /// <returns></returns>
		public override double GetVolume()
		{
            var gm = GeometryDetails;

            return CalculateFrontFaceArea(gm) * gm.FlowLength;
		}

        /// <summary>
        /// Units of mm^2
        /// </summary>
        /// <returns></returns>
        public override double GetSurfaceArea()
        {
            var gm = GeometryDetails;

            double frontAndBack = 2 * CalculateFrontFaceArea(gm);
            double bottomAndTop = 2 * gm.FlowLength * gm.Width;
            double finSides = 2 * gm.NumberOfFins * gm.FlowLength * gm.FinHeight;
            double baseSides = 2 * gm.FlowLength * gm.BaseThickness;

            return (frontAndBack + bottomAndTop + finSides + baseSides);
        }

        private double CalculateFrontFaceArea(PlateFinGeometryParameters gm)
        {
            return (gm.BaseThickness * gm.Width + (gm.NumberOfFins * gm.FinThickness * gm.FinHeight));
        }

        /// <summary>
        /// Characteristic Length
        /// </summary>
        /// <returns>Hydraulic diameter [mm]</returns>
        public override double GetCharacteristicLength()
        {
            double finPitch = GetPitch();
            var gm = GeometryDetails;

            double fourTimesTheArea = 4 * finPitch * gm.FinHeight;
            double twoTimesThePerimeter = 2 * finPitch + 2 * gm.FinHeight;

            return fourTimesTheArea / twoTimesThePerimeter;
        }

        public override double AspectRatio()
        {
            return GetPitch() / GeometryDetails.FinHeight;
        }
    }
}

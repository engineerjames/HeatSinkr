﻿
namespace HeatSinkr.Library
{
	public class PlateFinGeometry : Geometry<PlateFinGeometryParameters>
	{
		public override PlateFinGeometryParameters GeometryDetails { get; set; }

        public override double Pitch
        {
            get
            {
                var gp = GeometryDetails;

                return (gp.Width - (gp.NumberOfFins * gp.FinThickness)) / (gp.NumberOfFins - 1);
            }
        }

        public PlateFinGeometry(PlateFinGeometryParameters Parameters)
		{
			this.GeometryDetails = Parameters;
		}

        /// <summary>
        /// Units of mm^3
        /// </summary>
        /// <returns></returns>
        public override double Volume
        {
            get
            {
                var gm = GeometryDetails;

                return CalculateFrontFaceArea(gm) * gm.FlowLength;
            }
        }

        /// <summary>
        /// Units of mm^2
        /// </summary>
        /// <returns></returns>
        public override double SurfaceArea
        {
            get
            {
                var gm = GeometryDetails;

                double frontAndBack = 2 * CalculateFrontFaceArea(gm);
                double bottomAndTop = 2 * gm.FlowLength * gm.Width;
                double finSides = 2 * gm.NumberOfFins * gm.FlowLength * gm.FinHeight;
                double baseSides = 2 * gm.FlowLength * gm.BaseThickness;

                return (frontAndBack + bottomAndTop + finSides + baseSides);
            }
        }

        private double CalculateFrontFaceArea(PlateFinGeometryParameters gm)
        {
            return (gm.BaseThickness * gm.Width + (gm.NumberOfFins * gm.FinThickness * gm.FinHeight));
        }

        /// <summary>
        /// Characteristic Length
        /// </summary>
        /// <returns>Hydraulic diameter [mm]</returns>
        public override double CharacteristicLength
        {
            get
            {
                double finPitch = Pitch;
                var gm = GeometryDetails;

                double fourTimesTheArea = 4 * finPitch * gm.FinHeight;
                double twoTimesThePerimeter = 2 * finPitch + 2 * gm.FinHeight;

                return fourTimesTheArea / twoTimesThePerimeter;
            }
        }

        public override double AspectRatio
        {
            get
            {
                return Pitch / GeometryDetails.FinHeight;
            }
        }
    }

    public struct PlateFinGeometryParameters
    {
        /// <summary>
        /// Units of mm
        /// </summary>
		public double FlowLength { get; set; }

        /// <summary>
        /// Units of mm
        /// </summary>
		public double Width { get; set; }

        public int NumberOfFins { get; set; }

        /// <summary>
        /// Units of mm, Overall height
        /// </summary>
		public double Height
        {
            get
            {
                return FinHeight + BaseThickness;
            }
        }

        /// <summary>
        /// Units of mm
        /// </summary>
		public double FinHeight { get; set; }

        /// <summary>
        /// Units of mm
        /// </summary>
		public double FinThickness { get; set; }

        /// <summary>
        /// Units of mm
        /// </summary>
		public double BaseThickness { get; set; }
    };
}

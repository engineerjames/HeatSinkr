using System;

namespace HeatSinkr.Library
{
	public class PlateFinGeometry : Geometry<PlateFinGeometryParameters>
	{
		public override PlateFinGeometryParameters GeometryDetails { get; set; }

        /// <summary>
        /// [mm]
        /// </summary>
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
        /// [mm^3]
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
        /// [mm^2]
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
        /// [mm]
        /// </summary>
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

        /// <summary>
        /// Unitless
        /// </summary>
        public override double AspectRatio
        {
            get
            {
                return Pitch / GeometryDetails.FinHeight;
            }
        }
    }

    public class PlateFinGeometryParameters
    {
        /// <summary>
        /// [m]
        /// </summary>
		public double FlowLength
        {
            get
            {
                return _FlowLength;
            }
            set
            {
                if (value <= 0)
                    throw new InvalidOperationException("Flow length cannot be less than or equal to 0.");
                else
                    _FlowLength = value;
            }
        }
        private double _FlowLength;

        /// <summary>
        /// [m]
        /// </summary>
		public double Width
        {
            get
            {
                return _Width;
            }
            set
            {
                if (value <= 0)
                    throw new InvalidOperationException("Width of heatsink can not be less than or equal to 0.");
                else
                    _Width = value;
            }
        }
        private double _Width;

        /// <summary>
        /// Number of fins for the heatsink
        /// </summary>
        public int NumberOfFins
        {
            get
            {
                return _NumberOfFins;
            }
            set
            {
                if (value <= 0)
                    throw new InvalidOperationException("Number of fins can not be less than or equal to 0.");
                else if (TooManyFinsOnHeatsink(value))
                    throw new InvalidOperationException("Too many fins for the given base width.");
                else
                    _NumberOfFins = value;
            }
        }

        private bool TooManyFinsOnHeatsink(double PotentialNumberOfFins)
        {
            if (FinThickness <= 0 || Width <= 0)
                throw new InvalidOperationException("Width and Fin Thickness must be initialized prior to the number of fins.");

            return ((PotentialNumberOfFins * FinThickness) >= Width);
        }

        private int _NumberOfFins;

        /// <summary>
        /// [m]
        /// </summary>
		public double Height
        {
            get
            {
                return FinHeight + BaseThickness;
            }
        }

        /// <summary>
        /// [m]
        /// </summary>
		public double FinHeight
        {
            get
            {
                return _FinHeight;
            }
            set
            {
                if (value <= 0)
                    throw new InvalidOperationException("Fin height can't be less than or equal to 0.");
                else
                    _FinHeight = value;
            }
        }
        private double _FinHeight;

        /// <summary>
        /// [m]
        /// </summary>
		public double FinThickness
        {
            get
            {
                return _FinThickness;
            }
            set
            {
                if (value <= 0)
                    throw new InvalidOperationException("Fin thickness can't be less than or equal to 0.");
                else
                    _FinThickness = value;
            }
        }
        private double _FinThickness;

        /// <summary>
        /// [m]
        /// </summary>
		public double BaseThickness
        {
            get
            {
                return _BaseThickness;
            }
            set
            {
                if (value <= 0)
                    throw new InvalidOperationException("Base thickness can't be less than or equal to 0.");
                else
                    _BaseThickness = value;
            }
        }
        private double _BaseThickness;
    };
}

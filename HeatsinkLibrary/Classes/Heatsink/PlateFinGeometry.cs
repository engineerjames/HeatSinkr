using System;

namespace HeatSinkr.Library
{
    public class PlateFinGeometry : Geometry
    {
        public PlateFinGeometry() : base()
        {

        }

        /// <summary>
        /// [mm]
        /// </summary>
        public override double Pitch
        {
            get
            {
                var pitch = (Width - (NumberOfFins * FinThickness)) / (NumberOfFins - 1);

                return pitch;
            }
        }

        /// <summary>
        /// [mm^3]
        /// </summary>
        /// <returns></returns>
        public override double Volume
        {
            get
            {
                return CalculateFrontFaceArea() * FlowLength;
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
                double frontAndBack = 2.0 * CalculateFrontFaceArea();
                double bottomAndTop = 2.0 * FlowLength * Width;
                double finSides = 2.0 * NumberOfFins * FlowLength * FinHeight;
                double baseSides = 2.0 * FlowLength * BaseThickness;

                return (frontAndBack + bottomAndTop + finSides + baseSides);
            }
        }

        private double CalculateFrontFaceArea()
        {
            var Area = BaseThickness * Width + (NumberOfFins * FinThickness * FinHeight);
            return Area;
        }


        /// <summary>
        /// [mm]
        /// </summary>
        public override double CharacteristicLength
        {
            get
            {
                double finPitch = Pitch;
                double fourTimesTheArea = 4 * finPitch * FinHeight;
                double twoTimesThePerimeter = 2 * finPitch + 2 * FinHeight;

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
                return Pitch / FinHeight;
            }
        }

        /// <summary>
        /// [m]
        /// </summary>
        public override double FlowLength
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
		public override double Width
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
        public override int NumberOfFins
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
		public override double Height
        {
            get
            {
                return FinHeight + BaseThickness;
            }
        }

        /// <summary>
        /// [m]
        /// </summary>
		public override double FinHeight
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
		public override double FinThickness
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
		public override double BaseThickness
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
    }
}

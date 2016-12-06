
using System.Reflection;

namespace HeatSinkr.Library
{
    public abstract class Geometry
    {
        /// <summary>
        /// Gap between heatsink fins [m]
        /// </summary>
        public abstract double Pitch { get; }

        /// <summary>
        /// Volume of the entire heatsink [m^3]
        /// </summary>
        public abstract double Volume { get; }

        /// <summary>
        /// Surface area of the entire heatsink [m^2]
        /// </summary>
        public abstract double SurfaceArea { get; }

        /// <summary>
        /// Characteristic Length used in Reynold's number calculation - [m]
        /// </summary>
        public abstract double CharacteristicLength { get; }

        /// <summary>
        /// Fin aspect ratio (gap/fin height) [Unitless]
        /// </summary>
        public abstract double AspectRatio { get; }

        /// <summary>
        /// Size of the heatsink in the flow direction [m]
        /// </summary>
        public abstract double FlowLength { get; set; }

        /// <summary>
        /// Size of the heatsink in the non-flow direction [m]
        /// </summary>
        public abstract double Width { get; set; }

        /// <summary>
        /// Number of fins present on the heatsink [Unitless]
        /// </summary>
        public abstract int NumberOfFins { get; set; }

        /// <summary>
        /// Height of the fins [m]
        /// </summary>
        public abstract double FinHeight { get; set; }

        /// <summary>
        /// Thickness of the fin [m]
        /// </summary>
        public abstract double FinThickness { get; set; }

        /// <summary>
        /// Thickness of the heatsink base [m]
        /// </summary>
        public abstract double BaseThickness { get; set; }

        /// <summary>
        /// Height of the overall heatsink [m]
        /// </summary>
        public abstract double Height { get; }

        public override string ToString()
        {
            var properties = typeof(Geometry).GetRuntimeProperties();
            string geometryString = "";
            int i = 0;

            foreach (PropertyInfo property in properties)
            {
                if (i==0)
                    geometryString += string.Format(property.Name + ", " + property.GetValue(this));
                else
                    geometryString += string.Format(System.Environment.NewLine + property.Name + ", " + property.GetValue(this));

                i++;
            }


            return geometryString;
        }

    }
}

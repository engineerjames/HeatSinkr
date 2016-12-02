
namespace HeatSinkr.Library
{
    public abstract class Geometry
    {
        public abstract double Pitch { get; }

        public abstract double Volume { get; }

        public abstract double SurfaceArea { get; }

        /// <summary>
        /// Characteristic Length used in Reynold's number calculation - [m]
        /// </summary>
        public abstract double CharacteristicLength { get; }

        public abstract double AspectRatio { get; }

        public abstract double FlowLength { get; set; }
        public abstract double Width { get; set; }
        public abstract int NumberOfFins { get; set; }
        public abstract double FinHeight { get; set; }
        public abstract double FinThickness { get; set; }
        public abstract double BaseThickness { get; set; }
        public abstract double Height { get; }

    }
}

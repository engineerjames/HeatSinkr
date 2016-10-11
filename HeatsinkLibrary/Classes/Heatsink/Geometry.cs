﻿
namespace HeatSinkr.Library
{
	public abstract class Geometry<T>
	{
        public abstract double Pitch { get; }

        public abstract double Volume { get; }

        public abstract T GeometryDetails { get; set; }

        public abstract double SurfaceArea { get; }

        public abstract double CharacteristicLength { get; }

        public abstract double AspectRatio { get; }
    }
}
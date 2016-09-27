using System;

namespace HeatSinkr.Library
{
	public abstract class Heatsink<T>
	{
		Geometry<T> HeatSinkGeometry { get; set; }
        Material HeatSinkMaterial { get; set; }

        public Heatsink(Material HeatSinkMaterial, Geometry<T> HeatSinkGeometry)
        {
            this.HeatSinkMaterial = HeatSinkMaterial;
            this.HeatSinkGeometry = HeatSinkGeometry;
        }
	}
}

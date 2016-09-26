using System;

namespace HeatSinkr.Library
{
	public abstract class Heatsink<T>
	{

		Geometry<T> HeatSinkGeometry { get; set; }
	}
}

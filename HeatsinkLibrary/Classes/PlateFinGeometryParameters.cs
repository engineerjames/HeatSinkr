using System;
using System.Diagnostics.Contracts;

namespace HeatSinkr.Library
{
	public struct PlateFinGeometryParameters
	{
		public double FlowLength;
		public double Width;
		public double NumberOfFins;
		public double Height
		{
			get
			{
				return FinHeight + BaseThickness;
			}
		}

		public double FinHeight;
		public double FinThickness;
		public double BaseThickness;
	};


}
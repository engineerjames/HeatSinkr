using System;
namespace HeatsinkLibrary
{
	public struct GeometryParameters
	{
		public double FlowLength;
		public double Width;

		private double _Height;

		public double Height
		{
			get
			{
				return FinHeight + BaseThickness;
			}
			private set
			{
				_Height = value;
			}
		}

		public double FinHeight;
		public double FinThickness;
		public double BaseThickness;
	};

	public class Geometry
	{
		GeometryParameters GeometryParameters;

		public Geometry()
		{

		}

		public Geometry(GeometryParameters Parameters)
		{
			this.GeometryParameters = Parameters;
		}

		internal GeometryParameters GetGeometryParameters()
		{
			return GeometryParameters;
		}

		internal double GetVolume()
		{
			return GeometryParameters.FlowLength * GeometryParameters.Height * GeometryParameters.Width;
		}
	}
}

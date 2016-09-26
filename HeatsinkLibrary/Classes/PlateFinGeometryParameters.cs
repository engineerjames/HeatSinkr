using System;
using System.Diagnostics.Contracts;

namespace HeatSinkr.Library
{
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
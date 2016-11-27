using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatSinkr.Library
{ 
    public class HeatSource
    {
        /// <summary>
        /// Heat source applied centered underneath the heatsink
        /// </summary>
        /// <param name="Power">Power dissipated [W]</param>
        public HeatSource(double Power)
        {
            this.Power = Power;
        }

		/// <summary>
		/// Heat source applied centered underneath the heatsink
		/// </summary>
		public HeatSource()
		{
			
		}

        /// <summary>
        /// Power dissipated [W]
        /// </summary>
        public double Power { get; set; }

        /// <summary>
		/// Width (non-flow direction) [m]
        /// </summary>
        public double Width { get; set; }

		/// <summary>
		/// Length (flow direction) [m]
		/// </summary>
		public double Length { get; set; }

    }
}

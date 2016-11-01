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
        /// Power dissipated [W]
        /// </summary>
        public double Power { get; set; }

        /// <summary>
        /// Width [m]
        /// </summary>
        public double Width { get; set; }

        /// <summary>
        /// Height [m]
        /// </summary>
        public double Height { get; set; }
    }
}

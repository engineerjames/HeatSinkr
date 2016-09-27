using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatSinkr.Library
{
    public class Air : Material
    {
        public double Temperature { get; set; }

        /// <summary>
        /// Air
        /// </summary>
        /// <param name="AirTemperatureInC">Units of °C</param>
        /// <param name="ThermalConductivity">Units of W/m-K</param>
        /// <param name="Density">Units of kg/m^3</param>
        public Air(double AirTemperatureInC) 
        {
            Temperature = AirTemperatureInC;
        }
    }
}

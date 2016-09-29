using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatSinkr.Library
{
    public class Air : Material
    {

        /// <summary>
        /// Calculated using information from EngineeringToolbox.com, assumes dry air
        /// http://www.engineeringtoolbox.com/dry-air-properties-d_973.html
        /// </summary>
        public override double Density
        {
            get
            {
                return CalculateAirDensity(this.Temperature);
            }
        }

        /// <summary>
        /// Calculated using information from EngineeringToolbox.com, assumes dry air
        /// http://www.engineeringtoolbox.com/dry-air-properties-d_973.html
        /// </summary>
        public override double ThermalConductivity
        {
            get
            {
                return CalculateThermalConductivity(this.Temperature);
            }

            set
            {
                base.ThermalConductivity = value;
            }
        }

        private double CalculateThermalConductivity(double Temperature)
        {
            double y = (-0.00000004 * Temperature * Temperature) + (0.00008 * Temperature) + 0.0241;
            return y;
        }

        private double CalculateAirDensity(double Temperature)
        {
            double y = (-0.000000063793629 * Temperature * Temperature * Temperature) + (0.000020279649044 * Temperature * Temperature) - (0.004773608915610 * Temperature) + 1.288377511307610;
            return y;
        }

        /// <summary>
        /// Calculated using information from EngineeringToolbox.com, assumes dry air
        /// http://www.engineeringtoolbox.com/dry-air-properties-d_973.html
        /// </summary>
        public override double DynamicViscosity
        {
            get
            {
                return CalculateDynamicViscosity(this.Temperature);
            }

            set
            {
                base.DynamicViscosity = value;
            }
        }

        private double CalculateDynamicViscosity(double Temperature)
        {
            double y = (-0.000000000041273 * Temperature * Temperature) + (0.000000050033012 * Temperature) + 0.000017155337759;
            return y;
        }

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

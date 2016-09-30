using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatSinkr.Library
{
    // Calculated using information from EngineeringToolbox.com, assumes dry air
    // http://www.engineeringtoolbox.com/dry-air-properties-d_973.html
    public class Air : Material
    {

        public override double Density
        {
            get
            {
                return CalculateAirDensity(Temperature);
            }
        }

        public override double ThermalConductivity
        {
            get
            {
                return CalculateThermalConductivity(Temperature);
            }
        }

        /// <summary>
        /// C_p
        /// </summary>
        public override double SpecificHeat
        {
            get
            {
                return CalculateSpecificHeat(Temperature);
            }
        }

        public override double Prandtl
        {
            get
            {
                return CalculatePrandtlNumber(Temperature);
            }
        }

        public override double Diffusivity
        {
            get
            {
                return CalculateDiffusivity(Temperature);
            }
        }

        private double CalculateDiffusivity(double Temperature)
        {
            double y = 0.000149428571429 * Temperature * Temperature + 0.126075685714285 * Temperature + 18.565998562143000;

            y *= 0.000010;

            return y;
        }

        private double CalculatePrandtlNumber(double Temperature)
        {
            double y = (0.000000000124320 * Temperature * Temperature * Temperature) + (0.000000479496503 * Temperature * Temperature) -
                (0.000264673626417 * Temperature) + 0.713798960055835;

            return y;
        }

        private double CalculateSpecificHeat(double Temperature)
        {
            double y = (0.000000000647708 * Temperature * Temperature * Temperature) + (0.000000257570723 * Temperature * Temperature) +
                (0.000033719436666 * Temperature) + 1.003779297507820;

            y *= 1000;

            return y;
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

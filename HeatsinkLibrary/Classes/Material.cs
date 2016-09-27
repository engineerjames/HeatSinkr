using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatSinkr.Library
{
    public abstract class Material
    {
        /// <summary>
        /// Units of W/m-K
        /// </summary>
        public double ThermalConductivity { get; set; }

        /// <summary>
        /// Units of kg/m^3
        /// </summary>
        public double Density { get; set; }
        
        /// <summary>
        /// Units of J/(kg*K)
        /// </summary>
        public double SpecificHeat { get; set; } 

        /// <summary>
        /// Units of Pa-s, also known as Absolute Viscosity
        /// </summary>
        public double DynamicViscosity { get; set; }

        /// <summary>
        /// Units of m^2/s
        /// </summary>
        public double KinematicViscosity
        {
            get
            {
                return DynamicViscosity / Density;
            }
        }

    }
}

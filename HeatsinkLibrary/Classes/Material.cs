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
        public virtual double ThermalConductivity { get; set; }

        /// <summary>
        /// Units of kg/m^3
        /// </summary>
        public virtual double Density { get; set; }
        
        /// <summary>
        /// Units of J/(kg*K)
        /// </summary>
        public virtual double SpecificHeat { get; set; } 

        /// <summary>
        /// Units of Pa-s, also known as Absolute Viscosity
        /// </summary>
        public virtual double DynamicViscosity { get; set; }

        /// <summary>
        /// Units of m^2/s
        /// </summary>
        public virtual double KinematicViscosity
        {
            get
            {
                return DynamicViscosity / Density;
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatSinkr.Library
{
    public class Material
    {
        /// <summary>
        /// Units of W/m-K
        /// </summary>
        public double ThermalConductivity { get; set; }

        /// <summary>
        /// Units of kg/m^3
        /// </summary>
        public double Density { get; set; }

        public Material(double ThermalConductivity, double Density)
        {
            this.ThermalConductivity = ThermalConductivity;
            this.Density = Density;
        }
    }
}

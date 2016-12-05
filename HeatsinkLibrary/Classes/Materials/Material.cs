
namespace HeatSinkr.Library
{
    public abstract class Material
    {
        /// <summary>
        /// Thermal conductivity [W/m^2-K]
        /// </summary>
        public virtual double ThermalConductivity { get; set; }

        /// <summary>
        /// Density [kg/m^3]
        /// </summary>
        public virtual double Density { get; set; }
        
        /// <summary>
        /// Specific Heat (Constant Pressure) [J/kg-K]
        /// </summary>
        public virtual double SpecificHeat { get; set; } 

        /// <summary>
        /// Dynamic viscosity [N-s/m^2 or kg/m-s]
        /// </summary>
        public virtual double DynamicViscosity { get; set; }

        /// <summary>
        /// Kinematic Viscosity [m^2/s]
        /// </summary>
        public double KinematicViscosity
        {
            get
            {
                return DynamicViscosity / Density;
            }
        }

        public virtual double Prandtl { get; }

        public virtual double Diffusivity { get; }

        /// <summary>
        /// Temperature [°C] - DEFAULT IS 35°C
        /// </summary>
        public double Temperature { get; set; } = 35.0;

        public MaterialType Type { get; set; }
    }
}

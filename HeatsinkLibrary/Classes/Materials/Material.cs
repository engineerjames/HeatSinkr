
namespace HeatSinkr.Library
{
    public abstract class Material
    {
        public virtual double ThermalConductivity { get; set; }

        public virtual double Density { get; set; }
        
        public virtual double SpecificHeat { get; set; } 

        public virtual double DynamicViscosity { get; set; }

        public double KinematicViscosity
        {
            get
            {
                return DynamicViscosity / Density;
            }
        }

        public virtual double Prandtl { get; }

        public virtual double Diffusivity { get; }

        public double Temperature { get; set; } = 35.0;
    }
}

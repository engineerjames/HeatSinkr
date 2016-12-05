

namespace HeatSinkr.Library
{
    public class Aluminum : Material
    {
        private const double _ThermalConductivity = 204.0;
        private const double _Density = 2700;

        [System.Obsolete("Aluminum doesn't utilize temperature dependent properties yet. Use blank constructor instead.")]
        public Aluminum(double Temperature)
        {
            this.Temperature = Temperature;
            this.Type = MaterialType.Aluminum;
        }

        public Aluminum() { }

        public override double ThermalConductivity
        {
            get
            {
                return _ThermalConductivity;
            }
        }

        public override double Density
        {
            get
            {
                return _Density;
            }
        }
    }
}
using HeatSinkr.Library;

namespace UnitTests
{
    public class Aluminum : Material
    {
        private const double _ThermalConductivity = 204.0;
        private const double _Density = 2700;

        public Aluminum(double Temperature)
        {
            this.Temperature = Temperature;
        }

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
namespace HeatSinkr.Library
{
    public class Copper : Material
    {
        private const double _ThermalConductivity = 400.0;
        private const double _Density = 8940;

        [System.Obsolete("Copper doesn't utilize temperature dependent properties yet. Use blank constructor instead.")]
        public Copper(double Temperature)
        {
            this.Temperature = Temperature;
        }

        public Copper() { }

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

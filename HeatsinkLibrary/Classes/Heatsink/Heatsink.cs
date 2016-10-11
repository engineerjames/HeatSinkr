

namespace HeatSinkr.Library
{
	public abstract class Heatsink<T>
	{
		public virtual Geometry<T> HeatSinkGeometry { get; set; }
        public virtual Material HeatSinkMaterial { get; set; }
        public Air InletAir;
        public abstract double CFM { get; set; }
        
        public Heatsink(Material HeatSinkMaterial, Geometry<T> HeatSinkGeometry, double InletAirTemperature = 35)
        {
            this.HeatSinkMaterial = HeatSinkMaterial;
            this.HeatSinkGeometry = HeatSinkGeometry;
            InletAir = new Air(InletAirTemperature);
        }

        public abstract double IncidentFlowVelocity { get; }
        public abstract double ChannelVelocity { get;}
        public abstract double HydraulicDiameter { get; }
        public abstract double FlowArea { get; }
        public abstract double ReynoldsNumber { get; }
        public abstract double PressureDrop { get; }
        
	}

}

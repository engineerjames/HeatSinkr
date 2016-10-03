

namespace HeatSinkr.Library
{
	public abstract class Heatsink<T>
	{
		public virtual Geometry<T> HeatSinkGeometry { get; set; }
        public virtual Material HeatSinkMaterial { get; set; }

        public abstract double CFM { get; set; }

        public Heatsink(Material HeatSinkMaterial, Geometry<T> HeatSinkGeometry)
        {
            this.HeatSinkMaterial = HeatSinkMaterial;
            this.HeatSinkGeometry = HeatSinkGeometry;
        }

        public abstract double IncidentFlowVelocity { get; }
        public abstract double ChannelVelocity { get;}
        public abstract double HydraulicDiameter { get; }


        public abstract double FlowArea { get; }
      
        
	}

}

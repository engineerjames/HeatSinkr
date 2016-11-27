namespace HeatSinkr.Library
{
    public abstract class Heatsink<T>
    {
        public Heatsink(Material HeatSinkMaterial, Geometry<T> HeatSinkGeometry, double InletAirTemperature = 35)
        {
            this.HeatSinkMaterial = HeatSinkMaterial;
            this.HeatSinkGeometry = HeatSinkGeometry;
            InletAir = new Air(InletAirTemperature);
			Source = new HeatSource();
        }

        public virtual Geometry<T> HeatSinkGeometry { get; set; }
        public virtual Material HeatSinkMaterial { get; set; }

        public Air InletAir;
        public HeatSource Source;

        public virtual FlowCondition FlowCondition
        {
            get
            {
                if (ReynoldsNumber < 2300.0)
                    return FlowCondition.Laminar;
                else if (ReynoldsNumber > 2300.0 && ReynoldsNumber < 4000.0)
                    return FlowCondition.Transient;
                else
                    return FlowCondition.Turbulent;
            }
        }

        public abstract double EntranceLength { get; }
        public abstract double CFM { get; set; }
        public abstract double IncidentFlowVelocity { get; }
        public abstract double ChannelVelocity { get; }
        public abstract double HydraulicDiameter { get; }
        public abstract double FlowArea { get; }
        public abstract double ReynoldsNumber { get; }
        public abstract double PressureDrop { get; }
        public abstract double Nu { get; }
        public abstract double FinEfficiency { get; }
        public abstract double HeatTransferCoefficient { get; }
        public abstract double ThermalResistance_Convection { get; }
        public abstract double ThermalResistance_Conduction { get; }
        public abstract double ThermalResistance_Caloric { get; }
		public abstract double ThermalResistance_Spreading { get; }

        /// <summary>
        /// Total thermal resistance [K/W]
        /// </summary>
        public virtual double ThermalResistance_Total
        {
            get
            {
                return ThermalResistance_Caloric + ThermalResistance_Conduction + ThermalResistance_Convection;
            }
        }

       

    }

    public enum FlowCondition { Laminar, Transient, Turbulent };
}

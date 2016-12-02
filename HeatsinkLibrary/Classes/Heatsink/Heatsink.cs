namespace HeatSinkr.Library
{
    public abstract class Heatsink
    {
        public Heatsink(Material Material, Geometry Geometry, double InletAirTemperature = 35)
        {
            this.Material = Material;
            this.Geometry = Geometry;
            InletAir = new Air(InletAirTemperature);
            Source = new HeatSource();
        }

        public Geometry Geometry { get; set; }
        public Material Material { get; set; }
        public Air InletAir;
        public HeatSource Source;

        public double CFM { get; set; }
        
        public FlowCondition FlowCondition
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
        public abstract double IncidentFlowVelocity { get; }
        public abstract double ChannelVelocity { get; }
        public abstract double HydraulicDiameter { get; }
        public abstract double FlowArea { get; }
        public abstract double FinArea { get; }
        public abstract double BaseArea { get; }
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
                return ThermalResistance_Caloric + ThermalResistance_Conduction + ThermalResistance_Convection + ThermalResistance_Spreading;
            }
        }

    }

    public enum FlowCondition { Laminar, Transient, Turbulent };
}

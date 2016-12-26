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
                    return FlowCondition.Transition;
                else
                    return FlowCondition.Turbulent;
            }
        }

        /// <summary>
        /// Entrance length [m]
        /// </summary>
        public abstract double EntranceLength { get; }

        /// <summary>
        /// Incident flow velocity (Duct velocity) [m/s]
        /// </summary>
        public abstract double IncidentFlowVelocity { get; }

        /// <summary>
        /// Channel flow velocity [m/s]
        /// </summary>
        public abstract double ChannelVelocity { get; }

        /// <summary>
        /// Hydraulic diameter [m]
        /// </summary>
        public abstract double HydraulicDiameter { get; }

        /// <summary>
        /// Flow area incident to the heatsink including the base [m^2]
        /// </summary>
        public abstract double FlowArea { get; }

        /// <summary>
        /// Exposed fin area to the flow (on a per fin basis) [m^2]
        /// </summary>
        public abstract double FinArea { get; }

        /// <summary>
        /// Total exposed base area to the inlet flow [m^2]
        /// </summary>
        public abstract double BaseArea { get; }

        /// <summary>
        /// Reynold's number [Dimensionless]
        /// </summary>
        public abstract double ReynoldsNumber { get; }

        /// <summary>
        /// Pressure drop across the heatsink [Pa]
        /// </summary>
        public abstract double PressureDrop { get; }

        /// <summary>
        /// Nusselt number [Dimensionless]
        /// </summary>
        public abstract double Nu { get; }

        /// <summary>
        /// Fin efficiency [Dimensionless]
        /// </summary>
        public abstract double FinEfficiency { get; }

        /// <summary>
        /// Heat transfer coefficient [W/m-K]
        /// </summary>
        public abstract double HeatTransferCoefficient { get; }

        /// <summary>
        /// Thermal convective resistance [K/W]
        /// </summary>
        public abstract double ThermalResistance_Convection { get; }

        /// <summary>
        /// Conductive thermal resistance [K/W]
        /// </summary>
        public abstract double ThermalResistance_Conduction { get; }

        /// <summary>
        /// Caloric thermal reistance [K/W]
        /// </summary>
        public abstract double ThermalResistance_Caloric { get; }

        /// <summary>
        /// Spreading resistance [K/W]
        /// </summary>
        public abstract double ThermalResistance_Spreading { get; }

        /// <summary>
        /// Total thermal resistance [K/W]
        /// </summary>
        public virtual double ThermalResistance_Total
        {
            get
            {
                return ThermalResistance_Caloric + ThermalResistance_Convection + ThermalResistance_Spreading;
            }
        }

    }

    public enum FlowCondition { Laminar, Transition, Turbulent };

}

using System;

namespace HeatSinkr.Library
{

    public class PlateFinHeatsink : Heatsink<PlateFinGeometryParameters>
    {
        public PlateFinHeatsink(Material HeatSinkMaterial, Geometry<PlateFinGeometryParameters> HeatSinkGeometry) : base(HeatSinkMaterial, HeatSinkGeometry)
        {
            CFM = 0;
        }

        /// <summary>
        /// Volumetric flow-rate in ft^3/min - CFM
        /// </summary>
        public override double CFM { get; set; }

        /// <summary>
        /// Flow velocity through each fin [m/s]
        /// </summary>
        public override double ChannelVelocity
        {
            get
            {
                return (IncidentFlowVelocity / Sigma);
            }
        }


        /// <summary>
        /// Hydraulic diameter of heatsink fin channel - 4*A/P [m]
        /// </summary>
        public override double HydraulicDiameter
        {
            get
            {
                var hs = HeatSinkGeometry.GeometryDetails;
                var p = HeatSinkGeometry.Pitch;
                var h = hs.FinHeight;

                var area = h * p;
                var perimeter = (2 * h) + p;

                return ((4 * area) / (perimeter));
            }
        }

        /// <summary>
        /// Flow velocity (avg.) incident to the heatsink: CFM/Area [m/s]
        /// </summary>
        public override double IncidentFlowVelocity
        {
            get
            {
                return Units.ConvertCFMToMetersCubedPerSecond(CFM) / FlowArea;
            }
        }

        /// <summary>
        /// Flow area incident to the heatsink including the base [m^2]
        /// </summary>
        public override double FlowArea
        {
            get
            {
                var hs = HeatSinkGeometry.GeometryDetails;
                var area = hs.Height * hs.Width;

                return area;
            }
        }

        /// <summary>
        /// Reynold's number for the heatsink channel [Unitless]
        /// </summary>
        public override double ReynoldsNumber
        {
            get
            {
                var mu = this.InletAir.DynamicViscosity;
                var rho = this.InletAir.Density;
                var V_ch = this.ChannelVelocity;
                var D_h = this.HydraulicDiameter;

                var Re = (rho * V_ch * D_h) / mu;

                return Re; 
            }
        }

        private double Sigma
        {
            get
            {
                var p = HeatSinkGeometry.Pitch;
                var t = HeatSinkGeometry.GeometryDetails.FinThickness;

                return (p / (p + t));
            }
        }

        /// <summary>
        /// Pressure drop across the heatsink [Pa], currently this assumes the following:
        /// 1.  Hydrodynamically developing flow (good likely only for 'shallow' heatsinks)
        /// This section will need to be expanded to check the entry length compared to the 
        /// heatsink flow length
        /// </summary>
        public override double PressureDrop
        {
            get
            {
                if (!FlowIsDeveloping()) 
                    throw new InvalidProgramException("Equation is only valid for developing flow condition!");

                var Pc = CalculateContractionLoss();
                var Pe = CalculateExpansionLoss();
                var Pf = CalculateFrictionalLoss();

                return (Pc + Pe + Pf);
            }
        }

        private bool FlowIsDeveloping()
        {
            return (EntranceLength > HeatSinkGeometry.GeometryDetails.FlowLength);
        }

        /// <summary>
        /// Nusselt number - Nu_Dh = h*Dh/k_air
        /// Turbulent correlation - Pg. 499 Incropera & Dewitt, "Introduction to Heat Transfer"
        /// 0.023Re^(0.8)*Pr^(0.4), assumed always heating and L/Dh >= 10
        /// 
        /// Laminar correlation - Currently using constant Nu_Dh of 3.66
        /// </summary>
        public override double Nu
        {
            get
            {
                if (this.FlowCondition == FlowCondition.Laminar)
                {
                    return 3.66;
                }
                else
                {
                    return (0.023 * Math.Pow(ReynoldsNumber, 0.8) * Math.Pow(InletAir.Prandtl, 0.4));
                }
            }
        }

        /// <summary>
        /// Calculates the entrance length [m]
        /// </summary>
        public override double EntranceLength
        {
            get
            {
                if (FlowCondition == FlowCondition.Laminar || FlowCondition == FlowCondition.Transient)
                    return (0.05 * ReynoldsNumber * HydraulicDiameter);
                else
                    return (1.359 * HydraulicDiameter * Math.Pow(ReynoldsNumber, 0.25));
            }
        }

        /// <summary>
        /// Fin efficiency
        /// </summary>
        public override double FinEfficiency
        {
            get
            {
                var h = HeatTransferCoefficient;
                var P = 2 * HeatSinkGeometry.GeometryDetails.FinThickness + 2 * HeatSinkGeometry.GeometryDetails.FlowLength;
                var k = HeatSinkMaterial.ThermalConductivity;
                var Ac = HeatSinkGeometry.GeometryDetails.FinThickness * HeatSinkGeometry.GeometryDetails.FlowLength;
                var Lc = HeatSinkGeometry.GeometryDetails.FinThickness / 2 + HeatSinkGeometry.GeometryDetails.FinHeight;

                var m = Math.Sqrt((h * P) / (k * Ac));
                return Math.Tanh(m * Lc) / (m*Lc);
            }
        }

        /// <summary>
        /// Heat transfer coefficient [W/m-K]
        /// </summary>
        public override double HeatTransferCoefficient
        {
            get
            {
                return (Nu * InletAir.ThermalConductivity) / HydraulicDiameter;
            }
        }

        private double CalculateFrictionalLoss()
        {
            var L = this.HeatSinkGeometry.GeometryDetails.FlowLength;
            var f = CalculateFrictionFactor(L);
            var Pf = (2 * f * L * InletAir.Density * ChannelVelocity * ChannelVelocity) / HydraulicDiameter;
            return Pf;
        }

        private double CalculateFrictionFactor(double flowLengthInM)
        {
            var Lch = flowLengthInM / (ReynoldsNumber * HydraulicDiameter);
            var firstTerm = (3.44 / Math.Sqrt(Lch));
            var secondTerm = 24 / (1 + this.HeatSinkGeometry.AspectRatio);

            var fAppRe = Math.Sqrt(firstTerm * firstTerm + secondTerm * secondTerm);

            return fAppRe / ReynoldsNumber;
        }

        private double CalculateExpansionLoss()
        {
            var Ke = CalculateExpansionFactor();
            var Pe = 0.5 * InletAir.Density * ChannelVelocity * ChannelVelocity * Ke;
            return Pe;
        }

        private double CalculateContractionLoss()
        {
            var Kc = CalculateContractionFactor();
            var Pc = 0.5 * InletAir.Density * IncidentFlowVelocity * IncidentFlowVelocity * Kc;
            return Pc;
        }

        private double CalculateContractionFactor()
        {
            var Kc = 1.18 + (0.0015 * Sigma) - (0.395 * Sigma * Sigma);
            return Kc;
        }

        private double CalculateExpansionFactor()
        {
            var Ke = 1 - (2.76 * Sigma) + (Sigma * Sigma);
            return Ke;
        }
    }
}

using System;

namespace HeatSinkr.Library
{

    public class PlateFinHeatsink : Heatsink
    {
        public PlateFinHeatsink(Material HeatSinkMaterial, PlateFinGeometry HeatSinkGeometry) : base(HeatSinkMaterial, HeatSinkGeometry)
        {
			CFM = 0;
        }

        /// <summary>
        /// Flow velocity through each fin [m/s]
        /// </summary>
        public override double ChannelVelocity
        {
            get
            {
                var hs = HeatsinkGeometry;

                var gapThickness = hs.Pitch;
                var A_duct = gapThickness * hs.FinHeight;
                var N_ducts = hs.NumberOfFins - 1;
                var Q = Convert.CFMToMCubedPerSecond(CFM) / N_ducts;
                var result = Q / A_duct;
                return result;
            }
        }

		/// <summary>
		/// Spreading thermal resistance [K/W]
		/// </summary>
		public override double ThermalResistance_Spreading
		{
			get
			{
				var pi = Math.PI;
				var h = HeatTransferCoefficient;
				var gm = HeatsinkGeometry;

				var r1 = Math.Sqrt((Source.Length * Source.Width) / pi);
				var r2 = Math.Sqrt((gm.FlowLength * gm.Width) / pi);

				var sigma = r1 / r2;
				var tau = gm.BaseThickness / r2;
				var Bi = (h * r2) / HeatsinkMaterial.ThermalConductivity;
				var lambda = Math.PI + (1.0 / (sigma * Math.Sqrt(pi)));

				var phi = (Math.Tanh(lambda * tau) + (lambda / Bi)) 
					/ (1.0 + (lambda / Bi) * Math.Tanh(lambda * tau));

				var Tr = ((1.0 - sigma) * phi) / (pi * HeatsinkMaterial.ThermalConductivity * r1);

				return Tr;
			}
		}

        /// <summary>
        /// Hydraulic diameter of heatsink fin channel - 4*A/P [m]
        /// </summary>
        public override double HydraulicDiameter
        {
            get
            {
                var hs = HeatsinkGeometry;
                var p = hs.Pitch;
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
                return Convert.CFMToMCubedPerSecond(CFM) / FlowArea;
            }
        }

        /// <summary>
        /// Flow area incident to the heatsink including the base [m^2]
        /// </summary>
        public override double FlowArea
        {
            get
            {
                var hs = HeatsinkGeometry;
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
                var p = HeatsinkGeometry.Pitch;
                var t = HeatsinkGeometry.FinThickness;

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
                System.Diagnostics.Debug.WriteLine("Flow is developing: " + FlowIsDeveloping().ToString());

                var Pc = CalculateContractionLoss();
                var Pe = CalculateExpansionLoss();
                var Pf = CalculateFrictionalLoss();

                return (Pc + Pe + Pf);
            }
        }

        private bool FlowIsDeveloping()
        {
            return (EntranceLength > HeatsinkGeometry.FlowLength);
        }

        /// <summary>
        /// Nusselt number - Nu_Dh = h*Dh/k_air
        /// Turbulent correlation - Pg. 499 Incropera & Dewitt, "Introduction to Heat Transfer"
        /// 0.023Re^(0.8)*Pr^(0.4), assumed always heating and L/Dh >= 10
        /// 
        /// Laminar correlation - Eqn. 8.56 Incropera & Dewitt, "Introduction to Heat Transfer"
        /// Assumes thermal entry, laminar flow, and uniform Ts
        /// </summary>
        public override double Nu
        {
            get
            {
                if (this.FlowCondition == FlowCondition.Laminar)
                {
                    var dL = HydraulicDiameter / HeatsinkGeometry.FlowLength;
                    var Re = ReynoldsNumber;
                    var Pr = InletAir.Prandtl;
                    var dLRePr = dL * Re * Pr;
                    var NuD = 3.66 + (0.0668 * dLRePr) / (1.0 + 0.04 * Math.Pow((dLRePr), 2.0 / 3.0));

                    return NuD;
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
				var k = HeatsinkMaterial.ThermalConductivity;
				var t = HeatsinkGeometry.FinThickness;
				var m = Math.Sqrt((2.0 * h) / (k * t));
				var Lc = HeatsinkGeometry.FinHeight + (0.5 * t);
				var efficiency = Math.Tanh(m * Lc) / (m * Lc);
				return efficiency;
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

        /// <summary>
        /// Thermal Resistance - Convection term [K/W]
        /// </summary>
        public override double ThermalResistance_Convection
        {
            get
            {
                
                var gm = HeatsinkGeometry;
                var At = gm.NumberOfFins * FinArea + BaseArea;

                var eta_nought = 1.0 - ((gm.NumberOfFins * FinArea / At) * (1.0 - FinEfficiency));
                var R = 1.0 / (eta_nought * HeatTransferCoefficient * At);

                return R;
            }
        }

        /// <summary>
        /// Exposed fin area to the flow (on a per fin basis) [m^2]
        /// </summary>
        public override double FinArea
        {
            get
            {
                var gm = HeatsinkGeometry;
                return (2.0 * gm.FlowLength * gm.FinHeight);
            }
        }

        /// <summary>
        /// Total exposed base area to the inlet flow [m^2]
        /// </summary>
        public override double BaseArea
        {
            get
            {
                var gm = HeatsinkGeometry;
                return (gm.NumberOfFins - 1.0) * HeatsinkGeometry.Pitch * gm.FlowLength;
            }
        }

        /// <summary>
        /// Thermal Resistance - Conduction term [K/W]
        /// </summary>
        public override double ThermalResistance_Conduction
        {
            get
            {
                var L = HeatsinkGeometry.BaseThickness;
                var k = HeatsinkMaterial.ThermalConductivity;
                var Ac = HeatsinkGeometry.FlowLength * HeatsinkGeometry.Width;

                return L / (k * Ac);
            }
        }

        public override double ThermalResistance_Caloric
        {
            get
            {
                var DT = (1.8 * Source.Power) / CFM;
                var Tfinal = InletAir.Temperature + DT;
                var Tavg = (InletAir.Temperature + Tfinal) / 2;

                return (Tavg - InletAir.Temperature) / Source.Power;
            }
        }

        private double CalculateFrictionalLoss()
        {
            var L = HeatsinkGeometry.FlowLength;
            var f = CalculateFrictionFactor(L);
            var Pf = (2 * f * L * InletAir.Density * ChannelVelocity * ChannelVelocity) / HydraulicDiameter;
            return Pf;
        }

        private double CalculateFrictionFactor(double flowLengthInM)
        {
            var Lch = flowLengthInM / (ReynoldsNumber * HydraulicDiameter);
            var firstTerm = (3.44 / Math.Sqrt(Lch));
            var secondTerm = 24.0 / (1.0 + HeatsinkGeometry.AspectRatio);

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

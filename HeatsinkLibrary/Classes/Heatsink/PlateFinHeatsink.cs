
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
        /// Hydraulic diameter of heatsink fin channel - 4*A/P
        /// </summary>
        public override double HydraulicDiameter
        {
            get
            {
                var hs = HeatSinkGeometry.GeometryDetails;
                var p = Units.ConvertMMtoM(HeatSinkGeometry.GetPitch());
                var h = Units.ConvertMMtoM(hs.FinHeight);

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
                var area = (Units.ConvertMMtoM(hs.Height)) * (Units.ConvertMMtoM(hs.Width));

                return area;
            }
        }

        private double Sigma
        {
            get
            {
                var p = HeatSinkGeometry.GetPitch();
                var t = HeatSinkGeometry.GeometryDetails.FinThickness;

                return (p / (p + t));
            }
        }
    }
}

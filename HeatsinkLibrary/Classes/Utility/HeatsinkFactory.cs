using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatSinkr.Library
{
    public enum HeatsinkType { PlateFin, FoldedFin, StaggeredPinFin, InlinePinFin};

    public static class HeatsinkFactory
    {
        public static Heatsink GetDefaultHeatsink(HeatsinkType type)
        {
            if (type == HeatsinkType.PlateFin)
                return GetDefaultPlateFinHeatsink();
            else
                throw new NotImplementedException("Heatsink type of " + type.ToString() + " has not been implemented!");
        }

        private static Heatsink GetDefaultPlateFinHeatsink()
        {
            PlateFinGeometry testGeom = new PlateFinGeometry();
            testGeom.FinThickness = .001;
            testGeom.FlowLength = .010;
            testGeom.Width = .040;
            testGeom.FinHeight = .035;
            testGeom.BaseThickness = .005;
            testGeom.NumberOfFins = 11;

            var hs = new PlateFinHeatsink(new Aluminum(), testGeom);
            hs.CFM = 5;

            var heat = new HeatSource(4);
            heat.Length = 0.010;
            heat.Width = 0.010;
            hs.Source = heat;

            return hs;
        }
    }
}

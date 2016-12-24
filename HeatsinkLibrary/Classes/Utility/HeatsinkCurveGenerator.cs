using System;
using System.Collections.Generic;

namespace HeatSinkr.Library
{
    public struct DataPoint
    {
        /// <summary>
        /// X coordinate
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Y coordinate
        /// </summary>
        public double Y { get; set; }

        public DataPoint(double X, double Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public override string ToString()
        {
            var dispX = Math.Round(X, 3);
            var dispY = Math.Round(Y, 3);
            return "{" + string.Format("x:{0}, y:{1}", dispX, dispY) + "}";
        }
    }

    public class CurveGenerator
    {
        private const int InterpolationPoints = 15;
        public List<List<DataPoint>> DataPoints = new List<List<DataPoint>>();
        private List<Heatsink> Heatsinks = new List<Heatsink>();

        private static CurveGenerator _Instance;

        /// <summary>
        /// Generated thermal resistance curve based on the current heatsinks in the container - use AddHeatsink method
        /// to add heatsinks to be analyzed.
        /// </summary>
        /// <param name="LowCFM">Lowest CFM to curve</param>
        /// <param name="HighCFM">Highest CFM on the curve</param>
        /// <returns>List of plottable X/Y coordinates for each heatsink in the collection</returns>
        public List<List<DataPoint>> GetThermalResistanceCurves(double LowCFM, double HighCFM)
        {            
            if (!CFMIsValid(HighCFM) || !CFMIsValid(LowCFM))
                throw new InvalidOperationException("Invalid CFM value passed in to GetThermalResistanceCurves");
            if (Heatsinks.Count <= 0)
                throw new InvalidOperationException("Can't generate thermal resistance curve for no heatsinks.  Try adding heatsink to the list (AddHeatSink)");

            DataPoints.Clear();
            
            foreach (Heatsink hs in Heatsinks)
            {
                var OriginalCFM = hs.CFM;
                var hsCurve = GenerateThermalResistancecurve(hs, LowCFM, HighCFM);
                DataPoints.Add(hsCurve);
                hs.CFM = OriginalCFM;
            }

            return DataPoints;
        }

        public List<List<DataPoint>> GetPressureDropCurves(double LowCFM, double HighCFM)
        {
            if (!CFMIsValid(HighCFM) || !CFMIsValid(LowCFM))
                throw new InvalidOperationException("Invalid CFM value passed in to GetPressureDropCurves.");
            if (Heatsinks.Count <= 0)
                throw new InvalidOperationException("Can't generate pressure drop curve for no heatsinks.  Try adding heatsink to the list (AddHeatSink)");

            DataPoints.Clear();

            foreach (Heatsink hs in Heatsinks)
            {
                var OriginalCFM = hs.CFM;
                var hsCurve = GeneratePressureDropCurve(hs, LowCFM, HighCFM);
                DataPoints.Add(hsCurve);
                hs.CFM = OriginalCFM;
            }

            return DataPoints;
        }

        private bool CFMIsValid(double CFMValue)
        {
            bool isCFMGreaterThanZero = (CFMValue >= 0);
            return isCFMGreaterThanZero;
        }

        private List<DataPoint> GenerateThermalResistancecurve(Heatsink hs, double LowCFM, double HighCFM)
        {
            var delta = (HighCFM - LowCFM) / (InterpolationPoints - 1);
            var TrCurve = new List<DataPoint>();

            for (int i = 0; i < InterpolationPoints; i++)
            {
                var cfm = LowCFM + delta * i;
                hs.CFM = cfm;
                TrCurve.Add(new DataPoint(cfm, hs.ThermalResistance_Total));
            }

            return TrCurve;
        }

        private List<DataPoint> GeneratePressureDropCurve(Heatsink hs, double LowCFM, double HighCFM)
        {
            var delta = (HighCFM - LowCFM) / (InterpolationPoints - 1);
            var DPCurve = new List<DataPoint>();

            for (int i = 0; i < InterpolationPoints; i++)
            {
                var cfm = LowCFM + delta * i;
                hs.CFM = cfm;
                DPCurve.Add(new DataPoint(cfm, Convert.PaToInH2O(hs.PressureDrop)));
            }

            return DPCurve;
        }

        public void AddHeatsink(Heatsink heatsinkToAdd)
        {
            Heatsinks.Add(heatsinkToAdd);
        }

        public void RemoveHeatsink(Heatsink heatsinkToRemove)
        {
            Heatsinks.Remove(heatsinkToRemove);
        }

        public void ClearHeatsinks()
        {
            Heatsinks.Clear();
        }

        public static CurveGenerator Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new CurveGenerator();

                return _Instance;
            }
        }

       
    }
}

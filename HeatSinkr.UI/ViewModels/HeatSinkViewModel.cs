﻿using HeatSinkr.Library;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace HeatSinkr.UI.ViewModels
{
    public class HeatSinkViewModel : ViewModelBase
    {
        private Heatsink hs { get; set; }

        private const int ThermalResistanceDecimalPlaces = 2;

        public double Pitch
        {
            get
            {
                return Convert.MtoMM(hs.Geometry.Pitch);
            }
        }

        public double Volume
        {
            get
            {
                return hs.Geometry.Volume;
            }
        }

        public double SurfaceArea
        {
            get
            {
                return hs.Geometry.SurfaceArea;
            }
        }

        private double _FlowLength;
        public double FlowLength
        {
            get
            {
                return Convert.MtoMM(_FlowLength);
            }
            set
            {
                value = Convert.MMtoM(value);
                hs.Geometry.FlowLength = value;
                SetField<double>(ref _FlowLength, value);
            }
        }

        private double _Width;
        public double Width
        {
            get
            {
                return Convert.MtoMM(_Width);
            }
            set
            {
                value = Convert.MMtoM(value);
                hs.Geometry.Width = value;
                SetField<double>(ref _Width, value);
            }
        }

        private double _HeatSourceWidth;
        public double HeatSourceWidth
        {
            get
            {
                return Convert.MtoMM(_HeatSourceWidth);
            }
            set
            {
                value = Convert.MMtoM(value);
                hs.Source.Width = value;
                SetField<double>(ref _HeatSourceWidth, value);
            }
        }

        private double _HeatSourceLength;
        public double HeatSourceLength
        {
            get
            {
                return Convert.MtoMM(_HeatSourceLength);
            }
            set
            {
                value = Convert.MMtoM(value);
                hs.Source.Length = value;
                SetField<double>(ref _HeatSourceLength, value);
            }
        }

        private double _HeatSourcePower;
        public double HeatSourcePower
        {
            get
            {
                return _HeatSourcePower;
            }
            set
            {
                hs.Source.Power = value;
                SetField<double>(ref _HeatSourcePower, value);
            }
        }

        private int _NumberOfFins;
        public int NumberOfFins
        {
            get
            {
                return _NumberOfFins;
            }
            set
            {
                hs.Geometry.NumberOfFins = value;
                SetField<int>(ref _NumberOfFins, value);
            }
        }

        private double _FinHeight;
        public double FinHeight
        {
            get
            {
                return Convert.MtoMM(_FinHeight);
            }
            set
            {
                value = Convert.MMtoM(value);
                hs.Geometry.FinHeight = value;
                SetField<double>(ref _FinHeight, value);
            }
        }

        private double _FinThickness;
        public double FinThickness
        {
            get
            {
                return Convert.MtoMM(_FinThickness);
            }
            set
            {
                value = Convert.MMtoM(value);
                hs.Geometry.FinThickness = value;
                SetField<double>(ref _FinThickness, value);
            }
        }

        private double _BaseThickness;
        public double BaseThickness
        {
            get
            {
                return Convert.MtoMM(_BaseThickness);
            }
            set
            {
                value = Convert.MMtoM(value);
                hs.Geometry.BaseThickness = value;
                SetField<double>(ref _BaseThickness, value);
            }
        }

        public double ThermalResistance_Convection
        {
            get
            {
                var Tr = System.Math.Round(hs.ThermalResistance_Convection, ThermalResistanceDecimalPlaces);
                return Tr;
            }
        }

        public double ThermalResistance_Spreading
        {
            get
            {
                var Tr = System.Math.Round(hs.ThermalResistance_Spreading, ThermalResistanceDecimalPlaces);
                return Tr;
            }
        }

        public double ThermalResistance_Caloric
        {
            get
            {
                var Tr = System.Math.Round(hs.ThermalResistance_Caloric, ThermalResistanceDecimalPlaces);
                return Tr;
            }
        }

        public double ThermalResistance_Total
        {
            get
            {
                var Tr = System.Math.Round(hs.ThermalResistance_Total, ThermalResistanceDecimalPlaces);
                return Tr;
            }
        }

        public double PressureDrop
        {
            get
            {
                var dp = Convert.PaToInH2O(hs.PressureDrop);
                dp = System.Math.Round(dp, 4);
                return dp;
            }
        }

        private double _CFM;
        public double CFM
        {
            get
            {
                return _CFM;
            }
            set
            {
                hs.CFM = value;
                SetField<double>(ref _CFM, value);
            }
        }

        private Material _Material;
        public Material Material
        {
            get
            {
                return hs.Material;
            }
            set
            {
                hs.Material = value;
                SetField(ref _Material, value);
            }
        }

        private double _InletAirTemperature;
        public double InletAirTemperature
        {
            get
            {
                return hs.InletAir.Temperature;
            }
            set
            {
                hs.InletAir.Temperature = value;
                SetField(ref _InletAirTemperature, value);
            }
        }

        public double FinEfficiency
        {
            get
            {
                var eta = hs.FinEfficiency * 100;
                eta = System.Math.Round(eta, 2);
                return eta;
            }
        }

        public double Temperature_Case
        {
            get
            {
                var Tcase = hs.Temperature_Case;
                return System.Math.Round(Tcase, 2);
            }
        }

        public string ThermalResistanceCurve
        {
            get
            {
                return GetThermalResistanceCurveData();
            }
        }

        public string PressureCurve
        {
            get
            {
                return GetPressureCurveData();
            }
        }

   
        public FlowCondition CurrentFlowCondition
        {
            get
            {
                return hs.FlowCondition;
            }
        }

        public double ReynoldsNumber
        {
            get
            {
                var Re = hs.ReynoldsNumber;
                return System.Math.Round(Re);
            }
        }

       

        public HeatSinkViewModel()
        {
            hs = HeatsinkFactory.GetDefaultHeatsink(HeatsinkType.PlateFin);
            CurveGenerator.Instance.AddHeatsink(hs);

            _Width = hs.Geometry.Width;
            _FlowLength = hs.Geometry.FlowLength;
            _FinThickness = hs.Geometry.FinThickness;
            _NumberOfFins = hs.Geometry.NumberOfFins;
            _FinHeight = hs.Geometry.FinHeight;
            _BaseThickness = hs.Geometry.BaseThickness;
            _CFM = hs.CFM;
            _InletAirTemperature = hs.InletAir.Temperature;
            _Material = hs.Material;
            _HeatSourceLength = hs.Source.Length;
            _HeatSourceWidth = hs.Source.Width;
            _HeatSourcePower = hs.Source.Power;

            // Initialize Model Outputs - things that need to get updated whenever changes are made to the input parameters
            ModelOutputs.Add(nameof(CurrentFlowCondition));
            ModelOutputs.Add(nameof(ThermalResistance_Convection));
            ModelOutputs.Add(nameof(ThermalResistance_Spreading));
            ModelOutputs.Add(nameof(ThermalResistance_Caloric));
            ModelOutputs.Add(nameof(ThermalResistance_Total));
            ModelOutputs.Add(nameof(PressureDrop));
            ModelOutputs.Add(nameof(FinEfficiency));
            ModelOutputs.Add(nameof(ReynoldsNumber));
            ModelOutputs.Add(nameof(Temperature_Case));        
        }
   

        public async Task<string> WriteHeatsinkDataAsync(HeatsinkWriters Writer)
        {
            return await Task.Run(() =>
             {
                 IHeatsinkExporter writer = HeatsinkWriterFactory.GetWriter(Writer);
                 return writer.GetWriteableData(hs);
             });
        }


        private string GetThermalResistanceCurveData()
        {
            string jChartDataPoints = "var chartData = [";

            var datapoints = CurveGenerator.Instance.GetThermalResistanceCurves(CalculateLowCFM(CFM), CalculateHighCFM(CFM));

            jChartDataPoints += GenerateChartDataFromDataPoints(datapoints);

            jChartDataPoints += "];";

            return jChartDataPoints;
        }

        private string GetPressureCurveData()
        {
            string jChartDataPoints = "var chartData2 = [";
            var datapoints = CurveGenerator.Instance.GetPressureDropCurves(CalculateLowCFM(CFM), CalculateHighCFM(CFM));

            jChartDataPoints += GenerateChartDataFromDataPoints(datapoints);

            jChartDataPoints += "];";

            return jChartDataPoints;    
        }


        private string GenerateChartDataFromDataPoints(List<List<DataPoint>> datapoints)
        {
            var dataString = "";
            for (int i = 0; i < datapoints[0].Count; i++)
            {
                if (i!= datapoints[0].Count-1)
                {
                    dataString += datapoints[0][i].ToString() + ",";
                }
                else
                {
                    dataString += datapoints[0][i];
                }
            }

            return dataString;
        }

        private double CalculateHighCFM(double currentCFM)
        {
            return (CFM + CFM * 0.9);
        }

        private double CalculateLowCFM(double currentCFM)
        {
            var lowCFM = CFM - CFM * 0.9;

            if (lowCFM < 0.5)
                lowCFM = 0.5;

            return lowCFM;
        }
    }
}

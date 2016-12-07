using HeatSinkr.Library;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HeatSinkr.UI.ViewModels
{
    public class HeatSinkViewModel : ViewModelBase
    {
        private Heatsink hs { get; set; }

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
                return hs.ThermalResistance_Convection;
            }
        }

        public double ThermalResistance_Spreading
        {
            get
            {
                return hs.ThermalResistance_Spreading;
            }
        }


        public double ThermalResistance_Total
        {
            get
            {
                return hs.ThermalResistance_Total;
            }
        }

        public double PressureDrop
        {
            get
            {
                return Convert.PaToInH2O(hs.PressureDrop);
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
                SetField<Material>(ref _Material, value);
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
                return hs.FinEfficiency;
            }
        }

        public HeatSinkViewModel()
        {
            hs = HeatsinkFactory.GetDefaultHeatsink(HeatsinkType.PlateFin);
            _Width = hs.Geometry.Width;
            _FlowLength = hs.Geometry.FlowLength;
            _FinThickness = hs.Geometry.FinThickness;
            _NumberOfFins = hs.Geometry.NumberOfFins;
            _FinHeight = hs.Geometry.FinHeight;
            _BaseThickness = hs.Geometry.BaseThickness;
            _CFM = hs.CFM;
            _InletAirTemperature = hs.InletAir.Temperature;
            

            // Initialize Model Outputs - things that need to get updated whenever changes are made to the input parameters
            ModelOutputs.Add(nameof(ThermalResistance_Convection));
            ModelOutputs.Add(nameof(ThermalResistance_Spreading));
            ModelOutputs.Add(nameof(ThermalResistance_Total));
            ModelOutputs.Add(nameof(PressureDrop));
            ModelOutputs.Add(nameof(FinEfficiency));
        }

        public void WriteHeatsinkData(HeatsinkWriters Writer, string directory)
        {
            IHeatsinkWriter writer = HeatsinkWriterFactory.GetWriter(Writer);
            writer.Write(this.hs, directory);
        }

    }
}

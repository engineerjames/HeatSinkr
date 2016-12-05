using HeatSinkr.Library;
using System.Collections.Generic;

namespace HeatSinkr.UI.ViewModels
{
    public class HeatSinkViewModel : ViewModelBase
    {
        public Heatsink hs { get; set; }

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
                SetField<int>(ref _NumberOfFins, value);
                hs.Geometry.NumberOfFins = value;
            }
        }

        private double _FinThickness;
        public double FinThickness
        {
            get
            {
                return _FinThickness;
            }
            set
            {
                SetField<double>(ref _FinThickness, value);
                hs.Geometry.FinThickness = value;
            }
        }

        private double _BaseThickness;
        public double BaseThickness
        {
            get
            {
                return _BaseThickness;
            }
            set
            {
                SetField<double>(ref _BaseThickness, value);
                hs.Geometry.BaseThickness = value;
            }
        }

        public double ThermalResistance_Convection
        {
            get
            {
                return hs.ThermalResistance_Convection;
            }
        }

        public HeatSinkViewModel()
        {
            hs = PlateFinHeatsink.DefaultHeatsink;
            _Width = hs.Geometry.Width;
            _FlowLength = hs.Geometry.FlowLength;

            // Initialize Model Outputs
            base.ModelOutputs.Add(nameof(ThermalResistance_Convection));
        }


    }
}

using HeatSinkr.Library;

namespace HeatSinkr.UI.ViewModels
{
    public class HeatSinkViewModel : ViewModelBase
    {
        private Heatsink hs;

        public double Pitch
        {
            get
            {
                return hs.Geometry.Pitch;
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

        
        public HeatSinkViewModel()
        {
                       
        }
    }
}

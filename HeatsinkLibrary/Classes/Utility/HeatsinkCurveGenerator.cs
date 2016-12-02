using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatSinkr.Library
{
    public class HeatsinkCurveGenerator
    {
        private static HeatsinkCurveGenerator _Instance;

        public void SetHeatsink(Heatsink heatsink) 
        {

        }
        
        private HeatsinkCurveGenerator()
        {

        }

        public static HeatsinkCurveGenerator Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new HeatsinkCurveGenerator();
                
                return _Instance;
            }
        }



    }
}

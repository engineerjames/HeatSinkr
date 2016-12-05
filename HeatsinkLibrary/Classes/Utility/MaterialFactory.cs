using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatSinkr.Library
{
    public enum MaterialType { Aluminum, Copper, Air };

    public static class MaterialFactory
    {
        public static Material GetMaterial(MaterialType type, double Temperature = 35.0)
        {
            switch (type)
            {
                case MaterialType.Aluminum:
                    return new Aluminum();
                case MaterialType.Copper:
                    return new Copper();
                case MaterialType.Air:
                    return new Air(Temperature);
                default:
                    throw new NotImplementedException(String.Format("Material type {0} hasn't been implemented yet!", type.ToString()));                    
            }
        }
    }
}

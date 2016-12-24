using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeatSinkr.Library
{
    public enum MaterialType { Aluminum, Copper, Air };

    /// <summary>
    /// Factory for generating material instances
    /// </summary>
    public static class MaterialFactory
    {
        /// <summary>
        /// Get material of 'type', evaluated at 'Temperature'
        /// </summary>
        /// <param name="type">Type of material to return</param>
        /// <param name="Temperature">Temperature of material (estimate)</param>
        /// <returns></returns>
        public static Material GetMaterial(MaterialType type, double Temperature = 35.0)
        {
            System.Diagnostics.Debug.WriteLine("Getting material of type: " + type);

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

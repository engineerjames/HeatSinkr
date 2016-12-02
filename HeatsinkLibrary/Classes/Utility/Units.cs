using System;

namespace HeatSinkr.Library
{
	public enum UnitType
	{
		SI,
		Imperial,
	};

	public static class Convert
	{
		private static double _PaToH2O = 1.0 / 248.84;
		private static double _H2OToPa = 248.84;
        private static double _CFMToMCubedPerSecond = 0.0004719474;
        private static double _MCubedPerSecondToCFM = 2118.880003;

		public static double MMtoM(double millimeters)
		{
			return millimeters / 1000.0;
		}

		public static double MtoMM(double meters)
		{
			return meters * 1000.0;
		}

		public static double PaToInH2O(double Pa)
		{
			return Pa * _PaToH2O;
		}

		public static double InH2OToPa(double InH2O)
		{
			return InH2O * _H2OToPa;
		}

        public static double CFMToMCubedPerSecond(double valueToConvert)
        {
            return valueToConvert * _CFMToMCubedPerSecond;
        }

        public static double MCubedPerSecondToCFM(double valueToConvert)
        {
            return valueToConvert * _MCubedPerSecondToCFM;
        }
    }
}

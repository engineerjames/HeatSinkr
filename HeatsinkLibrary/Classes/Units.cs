using System;

namespace HeatSinkr.Library
{
	public enum UnitType
	{
		SI,
		Imperial,
	};

	public static class Units
	{
		private static double PaToH2O = 1.0 / 248.84;
		private static double H2OToPa = 248.84;
        private static double CFMToMCubedPerSecond = 0.0004719474;
        private static double MCubedPerSecondToCFM = 2118.880003;

		public static double ConvertMMtoM(double millimeters)
		{
			return millimeters / 1000.0;
		}

		public static double ConvertMtoMM(double meters)
		{
			return meters * 1000.0;
		}

		public static double ConvertFromPaToH2O(double Pa)
		{
			return Pa * PaToH2O;
		}

		public static double ConvertFromH2OToPa(double InH2O)
		{
			return InH2O * H2OToPa;
		}

        public static double ConvertCFMToMetersCubedPerSecond(double valueToConvert)
        {
            return valueToConvert * CFMToMCubedPerSecond;
        }

        public static double ConvertFromMetersCubedPerSecondToCFM(double valueToConvert)
        {
            return valueToConvert * MCubedPerSecondToCFM;
        }
    }
}

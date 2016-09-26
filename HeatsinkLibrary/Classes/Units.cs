using System;


namespace HeatSinkr.Library
{
	public enum UnitType
	{
		mm,
		inches,
	};

	public static class Units
	{
		private static double PaToH2O = 1.0 / 248.84;
		private static double H2OToPa = 248.84;

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
	}
}

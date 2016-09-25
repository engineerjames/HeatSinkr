using System;


namespace HeatsinkLibrary
{
	enum UnitType
	{
		mm,
		inches,
	};

	public static class Units
	{
		internal static double ConvertMMtoM(double millimeters)
		{
			return millimeters / 1000.0;
		}

		internal static double ConvertMtoMM(double meters)
		{
			return meters * 1000.0;
		}

		internal static double ConvertFromPaToH2O(double Pa)
		{
			return 0.0;
		}
	}
}

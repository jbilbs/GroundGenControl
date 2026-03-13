using System;

namespace MccDaq
{
	[Flags]
	public enum SetpointFlag
	{
		EqualLimitA = 0,
		LessThanLimitA = 1,
		InsideLimits = 2,
		GreaterThanLimitB = 3,
		OutsideLimits = 4,
		Hysteresis = 5,
		UpdateOnTrueOnly = 0,
		UpdateOnTrueAndFalse = 8
	}
}

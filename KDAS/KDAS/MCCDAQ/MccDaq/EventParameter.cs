using System;

namespace MccDaq
{
	[Flags]
	public enum EventParameter
	{
		Default = 0,
		LatchDI = 1,
		LatchDO = 2
	}
}

using System;

namespace MccDaq
{
	[Flags]
	public enum StatusBits
	{
		UnderFlow = 1,
		OverFlow = 2,
		Compare = 4,
		Sign = 8,
		Error = 0x10,
		Updown = 0x20,
		Index = 0x40
	}
}

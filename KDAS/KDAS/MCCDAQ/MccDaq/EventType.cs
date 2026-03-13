using System;

namespace MccDaq
{
	[Flags]
	public enum EventType
	{
		OnScanError = 1,
		OnExternalInterrupt = 2,
		OnPretrigger = 4,
		OnDataAvailable = 8,
		OnEndOfAiScan = 0x10,
		OnEndOfAoScan = 0x20,
		OnChangeOfDigInput = 0x40,
		AllEventTypes = 0xFFFF
	}
}

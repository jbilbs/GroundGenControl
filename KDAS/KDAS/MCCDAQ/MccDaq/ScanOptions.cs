using System;

namespace MccDaq
{
	[Flags]
	public enum ScanOptions
	{
		Default = 0,
		Background = 1,
		Continuous = 2,
		ExtClock = 4,
		ConvertData = 8,
		ScaleData = 0x10,
		DtConnect = 0x10,
		SingleIo = 0x20,
		DmaIo = 0x40,
		BlockIo = 0x60,
		BurstIo = 0x10000,
		WordXfer = 0x100,
		Simultaneous = 0x200,
		NoFilter = 0x400,
		ExtMemory = 0x800,
		BurstMode = 0x1000,
		NoTodInts = 0x2000,
		ExtTrigger = 0x4000,
		NoCalibrateData = 0x8000,
		RetrigMode = 0x20000,
		NonStreamedIO = 0x40000,
		ADCClockTrig = 0x80000,
		ADCClock = 0x100000,
		HighResRate = 0x200000,
		ShuntCal = 0x400000,
		Ctr16Bit = 0,
		Ctr32Bit = 0x100,
		Ctr48Bit = 0x200
	}
}

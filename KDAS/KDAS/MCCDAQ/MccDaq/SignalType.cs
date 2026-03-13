namespace MccDaq
{
	public enum SignalType
	{
		AdcConvert = 1,
		AdcGate,
		AdcStartTrig = 4,
		AdcStopTrig = 8,
		AdcTbSrc = 0x10,
		AdcScanClk = 0x20,
		AdcSsh = 0x40,
		AdcStartScan = 0x80,
		AdcScanStop = 0x100,
		DacUpdate = 0x200,
		DacTbSrc = 0x400,
		DacStartTrig = 0x800,
		SyncClk = 0x1000,
		Ctr1Clk = 0x2000,
		Ctr2Clk = 0x4000,
		DGnd = 0x8000
	}
}

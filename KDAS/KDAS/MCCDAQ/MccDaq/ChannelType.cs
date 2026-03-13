namespace MccDaq
{
	public enum ChannelType
	{
		Analog,
		Digital8,
		Digital16,
		Ctr16,
		Ctr32Low,
		Ctr32High,
		CJC,
		TC,
		AnalogSE,
		AnalogDiff,
		SetpointStatus,
		SetpointEnable = 0x100
	}
}

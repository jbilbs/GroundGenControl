namespace MccDaq
{
	public class cDioConfig
	{
		internal enum DigitalInfo
		{
			BaseAdr,
			Initialized,
			DevType,
			Mask,
			ReadWrite,
			Config,
			NumBits,
			CurVal,
			InMask,
			OutMask,
			AlarmMask = 230
		}

		private MccBoard f_MccBoard;

		internal cDioConfig(MccBoard mccBoard)
		{
			this.f_MccBoard = mccBoard;
		}

		public ErrorInfo GetDevType(int devNum, out int configVal)
		{
			return this.f_MccBoard.GetConfig(InfoType.DigitalInfo, devNum, 2, out configVal);
		}

		public ErrorInfo GetConfig(int devNum, out int configVal)
		{
			return this.f_MccBoard.GetConfig(InfoType.DigitalInfo, devNum, 5, out configVal);
		}

		public ErrorInfo GetNumBits(int devNum, out int configVal)
		{
			return this.f_MccBoard.GetConfig(InfoType.DigitalInfo, devNum, 6, out configVal);
		}

		public ErrorInfo GetCurVal(int devNum, out int configVal)
		{
			return this.f_MccBoard.GetConfig(InfoType.DigitalInfo, devNum, 7, out configVal);
		}

		public ErrorInfo GetDInMask(int devNum, out int configVal)
		{
			return this.f_MccBoard.GetConfig(InfoType.DigitalInfo, devNum, 8, out configVal);
		}

		public ErrorInfo GetDOutMask(int devNum, out int configVal)
		{
			return this.f_MccBoard.GetConfig(InfoType.DigitalInfo, devNum, 9, out configVal);
		}

		public ErrorInfo GetAlarmMask(int devNum, out int configVal)
		{
			return this.f_MccBoard.GetConfig(InfoType.BoardInfo, devNum, 230, out configVal);
		}

		public ErrorInfo SetAlarmMask(int devNum, int configVal)
		{
			return this.f_MccBoard.SetConfig(InfoType.BoardInfo, devNum, 230, configVal);
		}
	}
}

namespace MccDaq
{
	public class cCtrConfig
	{
		internal enum CtrInfo
		{
			BaseAdr,
			Initialized,
			CtrType,
			CtrNum,
			ConfigByte
		}

		private MccBoard f_MccBoard;

		internal cCtrConfig(MccBoard mccBoard)
		{
			this.f_MccBoard = mccBoard;
		}

		public ErrorInfo GetCtrType(int devNum, out int configVal)
		{
			return this.f_MccBoard.GetConfig(InfoType.CounterInfo, devNum, 2, out configVal);
		}

		public ErrorInfo GetCtrNum(int devNum, out int configVal)
		{
			return this.f_MccBoard.GetConfig(InfoType.CounterInfo, devNum, 3, out configVal);
		}
	}
}

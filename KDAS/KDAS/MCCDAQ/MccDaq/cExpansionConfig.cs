namespace MccDaq
{
	public class cExpansionConfig
	{
		internal enum ExpansionInfo
		{
			BoardType,
			Mux_Ad_Chan1,
			Mux_Ad_Chan2,
			Range1,
			Range2,
			CjcChan,
			ThermType,
			NumExpChans,
			ParentBoard,
			Spare0
		}

		private MccBoard f_MccBoard;

		internal cExpansionConfig(MccBoard mccBoard)
		{
			this.f_MccBoard = mccBoard;
		}

		public ErrorInfo GetBoardType(int devNum, out int configVal)
		{
			return this.f_MccBoard.GetConfig(InfoType.ExpansionInfo, devNum, 0, out configVal);
		}

		public ErrorInfo SetMuxAdChan1(int devNum, int configVal)
		{
			return this.f_MccBoard.SetConfig(InfoType.ExpansionInfo, devNum, 1, configVal);
		}

		public ErrorInfo GetMuxAdChan1(int devNum, out int configVal)
		{
			return this.f_MccBoard.GetConfig(InfoType.ExpansionInfo, devNum, 1, out configVal);
		}

		public ErrorInfo SetMuxAdChan2(int devNum, int configVal)
		{
			return this.f_MccBoard.SetConfig(InfoType.ExpansionInfo, devNum, 2, configVal);
		}

		public ErrorInfo GetMuxAdChan2(int devNum, out int configVal)
		{
			return this.f_MccBoard.GetConfig(InfoType.ExpansionInfo, devNum, 2, out configVal);
		}

		public ErrorInfo SetRange1(int devNum, int configVal)
		{
			return this.f_MccBoard.SetConfig(InfoType.ExpansionInfo, devNum, 3, configVal);
		}

		public ErrorInfo GetRange1(int devNum, out int configVal)
		{
			return this.f_MccBoard.GetConfig(InfoType.ExpansionInfo, devNum, 3, out configVal);
		}

		public ErrorInfo GetRange2(int devNum, out int configVal)
		{
			return this.f_MccBoard.GetConfig(InfoType.ExpansionInfo, devNum, 4, out configVal);
		}

		public ErrorInfo SetRange2(int devNum, int configVal)
		{
			return this.f_MccBoard.SetConfig(InfoType.ExpansionInfo, devNum, 4, configVal);
		}

		public ErrorInfo GetCjcChan(int devNum, out int configVal)
		{
			return this.f_MccBoard.GetConfig(InfoType.ExpansionInfo, devNum, 5, out configVal);
		}

		public ErrorInfo SetCjcChan(int devNum, int configVal)
		{
			return this.f_MccBoard.SetConfig(InfoType.ExpansionInfo, devNum, 5, configVal);
		}

		public ErrorInfo GetThermType(int devNum, out int configVal)
		{
			return this.f_MccBoard.GetConfig(InfoType.ExpansionInfo, devNum, 6, out configVal);
		}

		public ErrorInfo SetThermType(int devNum, int configVal)
		{
			return this.f_MccBoard.SetConfig(InfoType.ExpansionInfo, devNum, 6, configVal);
		}

		public ErrorInfo GetNumExpChans(int devNum, out int configVal)
		{
			return this.f_MccBoard.GetConfig(InfoType.ExpansionInfo, devNum, 7, out configVal);
		}
	}
}

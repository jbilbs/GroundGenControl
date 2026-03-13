using System;

namespace MccDaq
{
	public class cBoardConfig
	{
		internal enum BoardInfo
		{
			BaseAdr,
			BoardType,
			IntLevel,
			DmaChan,
			Initialized,
			Clock,
			Range,
			NumAdChans,
			UsesExps,
			DiNumDevs,
			DiDevNum,
			CiNumDevs,
			CiDevNum,
			NumDaChans,
			WaitState,
			NumIoPorts,
			ParentBoard,
			DtBoard,
			NumExps,
			NumTempChans = 208,
			DacUpdateMode = 215,
			DacUpdateCmd,
			DacStartup,
			AdTrigCount = 219,
			PanId = 258,
			RfChannel,
			RSS = 261,
			DeviceId,
			DevNotes,
			HideLoginDlg = 274,
			DacTrigCount = 284,
			AdRes = 291
		}

		private MccBoard f_MccBoard;

		internal cBoardConfig(MccBoard mccBoard)
		{
			this.f_MccBoard = mccBoard;
		}

		public ErrorInfo GetDACUpdateMode(out int configVal)
		{
			return this.f_MccBoard.GetConfig(InfoType.BoardInfo, 0, 215, out configVal);
		}

		public ErrorInfo GetPANID(out int configVal)
		{
			return this.f_MccBoard.GetConfig(InfoType.BoardInfo, 0, 258, out configVal);
		}

		public ErrorInfo SetPANID(int configVal)
		{
			ErrorInfo errorInfo = this.f_MccBoard.SetConfig(InfoType.BoardInfo, 0, 258, configVal);
			if (errorInfo.Value == ErrorInfo.ErrorCode.NoErrors)
			{
				MccService.SaveConfig("CB.CFG");
			}
			return errorInfo;
		}

		public ErrorInfo GetRFChannel(out int configVal)
		{
			return this.f_MccBoard.GetConfig(InfoType.BoardInfo, 0, 259, out configVal);
		}

		public ErrorInfo SetRFChannel(int configVal)
		{
			ErrorInfo errorInfo = this.f_MccBoard.SetConfig(InfoType.BoardInfo, 0, 259, configVal);
			if (errorInfo.Value == ErrorInfo.ErrorCode.NoErrors)
			{
				MccService.SaveConfig("CB.CFG");
			}
			return errorInfo;
		}

		public ErrorInfo GetRSS(out int configVal)
		{
			return this.f_MccBoard.GetConfig(InfoType.BoardInfo, 0, 261, out configVal);
		}

		public ErrorInfo GetDeviceNotes(int start, out string configVal, ref int maxLen)
		{
			return this.f_MccBoard.GetConfig(InfoType.BoardInfo, start, 263, out configVal, ref maxLen);
		}

		public ErrorInfo SetDeviceNotes(int start, string configVal, ref int maxLen)
		{
			if (maxLen > configVal.Length)
			{
				maxLen = configVal.Length;
			}
			return this.f_MccBoard.SetConfig(InfoType.BoardInfo, start, 263, configVal, ref maxLen);
		}

		public ErrorInfo GetDeviceId(out string configVal, ref int maxLen)
		{
			return this.f_MccBoard.GetConfig(InfoType.BoardInfo, 0, 262, out configVal, ref maxLen);
		}

		public ErrorInfo SetDeviceId(string configVal, ref int maxLen)
		{
			if (maxLen > configVal.Length)
			{
				maxLen = configVal.Length;
			}
			ErrorInfo errorInfo = this.f_MccBoard.SetConfig(InfoType.BoardInfo, 0, 262, configVal, ref maxLen);
			if (errorInfo.Value == ErrorInfo.ErrorCode.NoErrors)
			{
				MccService.SaveConfig("CB.CFG");
			}
			return errorInfo;
		}

		public ErrorInfo GetDACStartup(int devNum, out int configVal)
		{
			return this.f_MccBoard.GetConfig(InfoType.BoardInfo, devNum, 217, out configVal);
		}

		public ErrorInfo GetBaseAdr(int devNum, out int configVal)
		{
			return this.f_MccBoard.GetConfig(InfoType.BoardInfo, devNum, 0, out configVal);
		}

		public ErrorInfo SetDACUpdateMode(int devNum, int configVal)
		{
			return this.f_MccBoard.SetConfig(InfoType.BoardInfo, devNum, 215, configVal);
		}

		public ErrorInfo DACUpdate()
		{
			return this.f_MccBoard.SetConfig(InfoType.BoardInfo, 0, 216, 0);
		}

		public ErrorInfo SetDACStartup(int configVal)
		{
			return this.f_MccBoard.SetConfig(InfoType.BoardInfo, 0, 217, configVal);
		}

		public ErrorInfo SetBaseAdr(int devNum, int configVal)
		{
			return this.f_MccBoard.SetConfig(InfoType.BoardInfo, devNum, 0, configVal);
		}

		public ErrorInfo GetBoardType(out int configVal)
		{
			return this.f_MccBoard.GetConfig(InfoType.BoardInfo, 0, 1, out configVal);
		}

		public ErrorInfo GetIntLevel(out int configVal)
		{
			return this.f_MccBoard.GetConfig(InfoType.BoardInfo, 0, 2, out configVal);
		}

		public ErrorInfo SetIntLevel(int configVal)
		{
			return this.f_MccBoard.SetConfig(InfoType.BoardInfo, 0, 2, configVal);
		}

		public ErrorInfo GetDmaChan(out int configVal)
		{
			return this.f_MccBoard.GetConfig(InfoType.BoardInfo, 0, 3, out configVal);
		}

		public ErrorInfo SetDmaChan(int configVal)
		{
			return this.f_MccBoard.SetConfig(InfoType.BoardInfo, 0, 3, configVal);
		}

		public ErrorInfo GetClock(out int configVal)
		{
			return this.f_MccBoard.GetConfig(InfoType.BoardInfo, 0, 5, out configVal);
		}

		public ErrorInfo SetClock(int configVal)
		{
			return this.f_MccBoard.SetConfig(InfoType.BoardInfo, 0, 5, configVal);
		}

		public ErrorInfo GetRange(out Range configVal)
		{
			int value;
			ErrorInfo config = this.f_MccBoard.GetConfig(InfoType.BoardInfo, 0, 6, out value);
			configVal = (Range)Enum.ToObject(typeof(Range), value);
			return config;
		}

		public ErrorInfo SetRange(Range configVal)
		{
			return this.f_MccBoard.SetConfig(InfoType.BoardInfo, 0, 6, (int)configVal);
		}

		public ErrorInfo GetNumAdChans(out int configVal)
		{
			return this.f_MccBoard.GetConfig(InfoType.BoardInfo, 0, 7, out configVal);
		}

		public ErrorInfo GetAdRetrigCount(out int configVal)
		{
			return this.f_MccBoard.GetConfig(InfoType.BoardInfo, 0, 219, out configVal);
		}

		public ErrorInfo SetAdRetrigCount(int configVal)
		{
			return this.f_MccBoard.SetConfig(InfoType.BoardInfo, 0, 219, configVal);
		}

		public ErrorInfo GetDACRetrigCount(out int configVal)
		{
			return this.f_MccBoard.GetConfig(InfoType.BoardInfo, 0, 284, out configVal);
		}

		public ErrorInfo SetDACRetrigCount(int configVal)
		{
			return this.f_MccBoard.SetConfig(InfoType.BoardInfo, 0, 284, configVal);
		}

		public ErrorInfo GetNumTempChans(out int configVal)
		{
			return this.f_MccBoard.GetConfig(InfoType.BoardInfo, 0, 208, out configVal);
		}

		public ErrorInfo SetNumAdChans(int configVal)
		{
			return this.f_MccBoard.SetConfig(InfoType.BoardInfo, 0, 7, configVal);
		}

		public ErrorInfo GetUsesExps(out int configVal)
		{
			return this.f_MccBoard.GetConfig(InfoType.BoardInfo, 0, 8, out configVal);
		}

		public ErrorInfo GetDiNumDevs(out int configVal)
		{
			return this.f_MccBoard.GetConfig(InfoType.BoardInfo, 0, 9, out configVal);
		}

		public ErrorInfo GetCiNumDevs(out int configVal)
		{
			return this.f_MccBoard.GetConfig(InfoType.BoardInfo, 0, 11, out configVal);
		}

		public ErrorInfo GetNumDaChans(out int configVal)
		{
			return this.f_MccBoard.GetConfig(InfoType.BoardInfo, 0, 13, out configVal);
		}

		public ErrorInfo GetWaitState(out int configVal)
		{
			return this.f_MccBoard.GetConfig(InfoType.BoardInfo, 0, 14, out configVal);
		}

		public ErrorInfo SetWaitState(int configVal)
		{
			return this.f_MccBoard.SetConfig(InfoType.BoardInfo, 0, 14, configVal);
		}

		public ErrorInfo GetNumIoPorts(out int configVal)
		{
			return this.f_MccBoard.GetConfig(InfoType.BoardInfo, 0, 15, out configVal);
		}

		public ErrorInfo GetDtBoard(out int configVal)
		{
			return this.f_MccBoard.GetConfig(InfoType.BoardInfo, 0, 17, out configVal);
		}

		public ErrorInfo GetNumExps(out int configVal)
		{
			return this.f_MccBoard.GetConfig(InfoType.BoardInfo, 0, 18, out configVal);
		}

		public ErrorInfo GetAdResolution(out int configVal)
		{
			return this.f_MccBoard.GetConfig(InfoType.BoardInfo, 0, 291, out configVal);
		}
	}
}

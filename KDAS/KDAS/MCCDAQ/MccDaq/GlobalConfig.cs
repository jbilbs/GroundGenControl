namespace MccDaq
{
	public class GlobalConfig
	{
		internal enum GlobalInfo
		{
			eVersion = 36,
			eNumBoards = 38,
			eNumExpBoards = 40
		}

		public static int NumBoards
		{
			get
			{
				CbwApi.Instance.cbGetConfig(1, 0, 0, 38, out int result);
				return result;
			}
		}

		public static int Version
		{
			get
			{
				CbwApi.Instance.cbGetConfig(1, 0, 0, 36, out int result);
				return result;
			}
		}

		public static int NumExpBoards
		{
			get
			{
				CbwApi.Instance.cbGetConfig(1, 0, 0, 40, out int result);
				return result;
			}
		}

		private GlobalConfig()
		{
		}
	}
}

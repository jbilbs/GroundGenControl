namespace MccDaq
{
	public class DataLogger
	{
		private string m_fileName;

		private ErrorInfo m_errorInfo;

		public string FileName => this.m_fileName;

		public DataLogger(string fileName)
		{
			this.m_fileName = fileName;
			this.m_errorInfo = new ErrorInfo(0);
		}

		public static ErrorInfo GetFileName(int fileNumber, ref string path, ref string filename)
		{
			ErrorInfo errorInfo = new ErrorInfo(0);
			errorInfo.ErrNumber = CbwApi.Instance.cbLogGetFileName(fileNumber, ref path, ref filename);
			return errorInfo;
		}

		public ErrorInfo SetPreferences(TimeFormat timeFormat, TimeZone timeZone, TempScale units)
		{
			ErrorInfo errorInfo = new ErrorInfo(0);
			errorInfo.ErrNumber = CbwApi.Instance.cbLogSetPreferences(timeFormat, timeZone, units);
			return errorInfo;
		}

		public ErrorInfo GetPreferences(ref TimeFormat timeFormat, ref TimeZone timeZone, ref TempScale units)
		{
			ErrorInfo errorInfo = new ErrorInfo(0);
			errorInfo.ErrNumber = CbwApi.Instance.cbLogGetPreferences(ref timeFormat, ref timeZone, ref units);
			return errorInfo;
		}

		public ErrorInfo GetFileInfo(ref int fileVersion, ref int fileSize)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbLogGetFileInfo(this.m_fileName, ref fileVersion, ref fileSize);
			return this.m_errorInfo;
		}

		public ErrorInfo GetSampleInfo(ref int sampleInterval, ref int sampleCount, ref int startDate, ref int startTime)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbLogGetSampleInfo(this.m_fileName, ref sampleInterval, ref sampleCount, ref startDate, ref startTime);
			return this.m_errorInfo;
		}

		public ErrorInfo GetAIChannelCount(ref int aiCount)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbLogGetAIChannelCount(this.m_fileName, ref aiCount);
			return this.m_errorInfo;
		}

		public ErrorInfo GetAIInfo(ref int[] channelNumbers, ref int[] units)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbLogGetAIInfo(this.m_fileName, channelNumbers, units);
			return this.m_errorInfo;
		}

		public ErrorInfo GetCJCInfo(ref int cjcCount)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbLogGetCJCInfo(this.m_fileName, ref cjcCount);
			return this.m_errorInfo;
		}

		public ErrorInfo GetDIOInfo(ref int dioCount)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbLogGetDIOInfo(this.m_fileName, ref dioCount);
			return this.m_errorInfo;
		}

		public ErrorInfo ReadTimeTags(int startSample, int count, ref int[] dateTags, ref int[] timeTags)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbLogReadTimeTags(this.m_fileName, startSample, count, dateTags, timeTags);
			return this.m_errorInfo;
		}

		public ErrorInfo ReadAIChannels(int startSample, int count, ref float[] aiChannels)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbLogReadAIChannels(this.m_fileName, startSample, count, aiChannels);
			return this.m_errorInfo;
		}

		public ErrorInfo ReadCJCChannels(int startSample, int count, ref float[] cjcChannels)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbLogReadCJCChannels(this.m_fileName, startSample, count, cjcChannels);
			return this.m_errorInfo;
		}

		public ErrorInfo ReadDIOChannels(int startSample, int count, ref int[] dioChannels)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbLogReadDIOChannels(this.m_fileName, startSample, count, dioChannels);
			return this.m_errorInfo;
		}

		public ErrorInfo ConvertFile(string destFileName, int startSample, int count, FieldDelimiter delimiter)
		{
			this.m_errorInfo.ErrNumber = CbwApi.Instance.cbLogConvertFile(ref this.m_fileName, ref destFileName, startSample, count, delimiter);
			return this.m_errorInfo;
		}
	}
}

using System;
using System.Threading;

namespace GroundGenControl.Logging
{
	/// <summary>
	/// Summary description for NamedEvents.
	/// </summary>
	public class LogSemaphore
	{

		System.Threading.Semaphore _logAccess = null;


		//	Maximum number of resources that can write to the log at once
		const int		LOG_ACCESS_COUNT=1;
		const String	WMI_GGC_LOGGER_SEMAPHORE =	"WMI_GGC_LOGGER";

		public LogSemaphore()
		{
			//
			// TODO: Add constructor logic here
			//
		}


		public void OpenOrCreate()
		{

			try
			{
				_logAccess = Semaphore.OpenExisting(WMI_GGC_LOGGER_SEMAPHORE);
			}
			catch (WaitHandleCannotBeOpenedException)
			{
				//Handle does not exist, create it.
				_logAccess = new Semaphore(LOG_ACCESS_COUNT,LOG_ACCESS_COUNT,WMI_GGC_LOGGER_SEMAPHORE);
			}


		}

		public void Close()
		{

			if ( _logAccess != null )
			{
				_logAccess.Close();
			}
		}

		public void getAccess()
		{

			_logAccess.WaitOne();

		}

		public void releaseAccess()
		{

			_logAccess.Release();

		}


	}
}

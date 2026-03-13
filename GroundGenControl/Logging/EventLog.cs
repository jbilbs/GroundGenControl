using System;
using System.IO;
using System.Collections;
using System.Collections.Specialized;
using System.Windows.Forms;

namespace GroundGenControl.Logging
{
	/// <summary>
	/// Summary description for EventLog.
	/// </summary>
	public class EventLog
	{

		ArrayList _eventList = new ArrayList();

		private  System.DateTime	_logStartTime		=	 System.DateTime.Now;
		private String				_logFileName		= "";
		private String				_logDirectoryPath	= "";
		private StreamWriter		_logWriter			= null;
		private FileStream			_logFileStream		= null;

        //LogSemaphore _logSemaphore = new LogSemaphore();

		public EventLog()
		{
            //  Create access semaphore for access to log file
            //_logSemaphore.OpenOrCreate();
			//	Initialize a new log if necessary
			//initNewLog();
		}


		public String	getLogFileName(String strSite)
		{
            return "GGC_Log_" + strSite + "_" + _logStartTime.ToString("MMMMddyyyy") + ".txt";
		}


		private void	writeLogHeader()
		{

            if (_logWriter != null)
            {
                //	Write header
                _logWriter.WriteLine("Log Time\tSite\tCommand\tDescription");
                _logWriter.Flush();
            }

		}

		private bool	openLogFile(String strSite)
		{
			bool	bLogExists = false;
            try
            {
                //  Ensure we are the only one trying to open
                //_logSemaphore.getAccess();

                //	Build filename
                _logFileName = getLogFileName(strSite);
                //	Build log path
                _logDirectoryPath = Directory.GetCurrentDirectory();
                _logDirectoryPath += @"\logs";
                if (!Directory.Exists(_logDirectoryPath))
                {
                    Directory.CreateDirectory(_logDirectoryPath);
                    //	Indicate that the log did not exist
                }

                bLogExists = File.Exists(_logDirectoryPath + @"\" + _logFileName);

                //	open log file, allow appending

                _logFileStream = File.Open(_logDirectoryPath + @"\" + _logFileName,
                                            FileMode.Append,
                                            FileAccess.Write,
                                            FileShare.ReadWrite);

                if (_logFileStream == null)
                {
                    throw new Exception("Unable to create log file stream");
                }
                _logWriter = new StreamWriter(_logFileStream);

                //_logWriter = new StreamWriter(_logDirectoryPath + @"\" + _logFileName, true);

                if (!bLogExists)
                {
                    //	Write a log header if this is a new log file
                    writeLogHeader();
                }



            }
            catch (Exception ex)
            {
                _logWriter = null;
                MessageBox.Show("Unable to create log file (" + ex.Message + ").  Logging to disk is disabled");
            }
            finally
            {
                //  We are done, access no longe required
                //_logSemaphore.releaseAccess();

            }

			return bLogExists;

		}

		private void	closeLogFile()
		{

			if ( _logWriter != null )
			{
				_logWriter.Close();
				_logWriter = null;
				_logFileStream = null;
			}


		}


		private void writeLogEntry(ref LogEntry entry)
		{
            try
            {

                //_logSemaphore.getAccess();

                if (_logWriter == null)
                {
                    return;
                }

                //	 If the current log file name is nolonger valid (date/changed), create a new one
                if (_logFileName != getLogFileName(entry.logSite))
                {
                    initNewLog(entry.logSite);
                }

                String logString = entry.logTime.ToString("MM/dd/yyyy HH:mm:ss") + "\t" +
                                   entry.logSite + "\t" +
                                   entry.logCommand + "\t" +
                                   entry.logDescription;


                _logWriter.WriteLine(logString);
                _logWriter.Flush();
            }
            catch (Exception)
            {

            }
            finally
            {
                //_logSemaphore.releaseAccess();

            }

		}

		//	Clears log and adds default log entry
		public void initNewLog(String strSite)
		{

            //  Access control managed in open log file

			_eventList.Clear();

			_logStartTime = System.DateTime.Now;
			//	Ensure log file is closed
			closeLogFile();
			//	Open new one
            openLogFile(strSite);

		}

		public void addEntry(ref LogEntry entry )
		{

			//	If the date is not the same as when the log was started, we need to 
			//	create a new log file

			System.DateTime entryTime = entry.logTime;

			if ( entryTime.Date.CompareTo(_logStartTime.Date) != 0 )
			{
				//	The date changed, initialize a new log
				initNewLog(entry.logSite);
			}

			_eventList.Add(entry);
			writeLogEntry(ref entry);

		}




		public string logFileName			{ get { return _logFileName;			} set { _logFileName = value;	} }
		public string logDirectoryPath		{ get { return _logDirectoryPath;		} set { _logDirectoryPath = value;	} }
		public ArrayList eventList			{ get { return _eventList;			} set { _eventList = value;	} }


	}
}

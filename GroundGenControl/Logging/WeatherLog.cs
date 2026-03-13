using System;
using System.IO;
using System.Collections;
using System.Collections.Specialized;
using System.Windows.Forms;
using GroundGenControl.Messages;

namespace GroundGenControl.Logging
{
    class WeatherLog
    {


		ArrayList _weatherList        = new ArrayList();

		private  System.DateTime	_logStartTime		=	 System.DateTime.Now;
		private String				_logFileName		= "";
		private String				_logDirectoryPath	= "";
		private StreamWriter		_logWriter			= null;
		private FileStream			_logFileStream		= null;


        public WeatherLog()
		{
		}


		public String	getLogFileName(String strSite)
		{
            return "GGC_WxLog_" + strSite + "_" + _logStartTime.ToString("MMMMddyyyy") + ".txt";
		}


		private void	writeLogHeader()
		{

            if (_logWriter != null)
            {
                //	Write header
                _logWriter.WriteLine("Observation Time\tWind Direction (Degrees)\tWind Speed (m/s)\tTemperature (°C)\tPressure (mmHg)\tRelative Humidity (%)\tHeater State");
                _logWriter.Flush();
            }

		}

		private bool	openLogFile(String strSite)
		{
			bool	bLogExists = false;
            try
            {

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


                if (!bLogExists)
                {
                    //	Write a log header if this is a new log file
                    writeLogHeader();
                }



            }
            catch (Exception ex)
            {
                _logWriter = null;
                MessageBox.Show("Unable to create weather log file (" + ex.Message + ").  Logging to disk is disabled");
            }
            finally
            {


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


		private void writeLogEntry(WeatherMessage entry)
		{
            try
            {

                if (_logWriter == null)
                {
                    return;
                }

                //	 If the current log file name is nolonger valid (date/changed), create a new one
                if (_logFileName != getLogFileName(entry.wxSite))
                {
                    initNewLog(entry.wxSite);
                }

                //_logWriter.WriteLine("Observation Time\tWind Direction\tWind Speed\tTemperature\tPressure\tRelative Humidity\tHeater State");


                String logString = entry.observationTimeString      + "\t" +
                                   entry.windDirectionString        + "\t" +
                                   entry.windSpeedString            + "\t" +
                                   entry.airTempString              + "\t" +
                                   entry.airPressureString          + "\t" +
                                   entry.relativeHumidityString     + "\t" +
                                   entry.heaterStateString;


                _logWriter.WriteLine(logString);
                _logWriter.Flush();
            }
            catch (Exception)
            {

            }
            finally
            {
            }

		}

		//	Clears log and adds default log entry
		public void initNewLog(String strSite)
		{

            //  Access control managed in open log file

            _weatherList.Clear();

			_logStartTime = System.DateTime.Now;
			//	Ensure log file is closed
			closeLogFile();
			//	Open new one
            openLogFile(strSite);

		}

        /// <summary>
        /// Adds a single weather entry to the weather list and weather log 
        /// </summary>
        /// <param name="entry">WeatherMessage individual weather messsage</param>
		public void addEntry(ref WeatherMessage entry )
		{

			//	If the date is not the same as when the log was started, we need to 
			//	create a new log file

			System.DateTime entryTime = entry.observationTime;

			if ( entryTime.Date.CompareTo(_logStartTime.Date) != 0 )
			{
				//	The date changed, initialize a new log
				initNewLog(entry.wxSite);
			}

            //  If the weather event hasn't already been placed in the log, add it
            if ( !_weatherList.Contains(entry) )
            {
    			_weatherList.Add(entry);
                writeLogEntry(entry);
            }

		}


        private bool isWxMessageInArray(WeatherMessage wxMessage)
        {

            foreach (WeatherMessage tempMessage in _weatherList)
            {

                if (tempMessage.observationTime.CompareTo(wxMessage.observationTime) == 0)
                {
                    return true;
                }
            }

            return false;


        }

        /// <summary>
        /// Adds multiple weather messages to the weather list and weather log
        /// </summary>
        /// <param name="entry">entry - Weather History Message</param>
        public void addEntries(ref WeatherHistoryMessage entry )
		{

			//	If the date is not the same as when the log was started, we need to 
			//	create a new log file

            if ( entry.WeatherMessageArray.Count == 0 )
            {
                return;
            }

            System.DateTime entryTime = ((WeatherMessage)entry.WeatherMessageArray[0]).observationTime;

			if ( entryTime.Date.CompareTo(_logStartTime.Date) != 0 )
			{
				//	The date changed, initialize a new log
				initNewLog(((WeatherMessage)entry.WeatherMessageArray[0]).wxSite);
			}

            foreach( WeatherMessage message in entry.WeatherMessageArray )
            {
                //  If the weather event hasn't already been placed in the log, add it
                if (!isWxMessageInArray(message))
                {
    			    _weatherList.Add(message);
                    writeLogEntry(message);
                }
            }

		}

		public string logFileName			{ get { return _logFileName;			} set { _logFileName = value;	} }
		public string logDirectoryPath		{ get { return _logDirectoryPath;		} set { _logDirectoryPath = value;	} }
        public ArrayList weatherList        { get { return _weatherList; } set { _weatherList = value; } }



    }
}

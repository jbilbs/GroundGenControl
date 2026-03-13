using System;

namespace GroundGenControl.Messages
{
	/// <summary>
	/// Summary description for WeatherMessages.
	/// </summary>
    public class WeatherMessage : IComparable
	{


        public class MessageSchema 
		{
        
			public const char	WIND_DIRECTION_TERMINATOR		= 'D';
			public const char	WIND_SPEED_TERMINATOR			= 'S';
			public const char	AIR_TEMP_TERMINATOR				= 'C';
			public const char	RELATIVE_HUMIDITY_TERMINATOR	= 'H';
			public const char	PRESSURE_TERMINATOR				= 'P';

			//	Message Code
			public const int MESSAGE_TYPE_INDEX			= 0;
			public const int MESSAGE_TYPE_LENGTH		= 1;

            //  Time of observation
            public const int OBSERVATION_TIME_INDEX     = MESSAGE_TYPE_INDEX + MESSAGE_TYPE_LENGTH;
            public const int OBSERVATION_TIME_LENGTH    = 14;


			//	Wind Direction
            public const int WIND_DIRECTION_INDEX = OBSERVATION_TIME_INDEX + OBSERVATION_TIME_LENGTH;


			/*
			public const int WIND_DIRECTION_LENGTH		= 4;
			//	Wind Speed m/s
			public const int WIND_SPEED_INDEX			= WIND_DIRECTION_INDEX + WIND_DIRECTION_LENGTH;
			public const int WIND_SPEED_LENGTH			= 5;	
			//	Air Temp Degrees C
			public const int AIR_TEMP_INDEX				= WIND_SPEED_INDEX + WIND_SPEED_LENGTH;
			public const int AIR_TEMP_LENGTH			= 6;	
			//	Relative humidity
			public const int RELATIVE_HUMIDITY_INDEX	= AIR_TEMP_INDEX + AIR_TEMP_LENGTH;
			public const int RELATIVE_HUMIDITY_LENGTH	= 6;
			//	Air presure in mmHg
			public const int AIR_PRESSURE_INDEX			= RELATIVE_HUMIDITY_INDEX + RELATIVE_HUMIDITY_LENGTH;
			public const int AIR_PRESSURE_LENGTH		= 7;
			//	Heater status
			public const int HEATER_STATUS_INDEX		= AIR_PRESSURE_INDEX + AIR_PRESSURE_LENGTH;
			public const int HEATER_STATUS_LENGTH		= 1;

			public const int TOTAL_MESSAGE_LENGTH		= HEATER_STATUS_INDEX + HEATER_STATUS_LENGTH;
			*/

		}

		private System.DateTime	_observationTime					=	System.DateTime.Now;
		private	int				_windDirection						=	0;
		private double			_windSpeed							=	0;
		private double			_airTemp							=   0;
		private	double			_relativeHumidity					=	0;
		private double			_airPressure						=	0;
		private	int				_heaterState						=	0;
        private String          _wxSite                             =   "";

		public WeatherMessage(String wxSite)
		{

            _wxSite = wxSite;
		}


		public String stateToString( bool state, String trueString, String falseString )
		{
			if ( state )
			{
				return trueString;
			}

			return falseString;

		}


		public int getHeaterPercent( char heaterChar )
		{

			if ( heaterChar == 'V'  || heaterChar == 'F' )
			{
				return 50;
			}

			if ( heaterChar == 'W'  )
			{
				return 100;
			}

			return 0;

		}
        

        /// <summary>
        /// This method checks a weather message to indicate if weather information is available
        /// </summary>
        /// <param name="strMessage"></param>
        /// <returns></returns>
        public static bool isWeatherAvailable(String strMessage)
        {


            if ( strMessage.Substring(0,2) == "W-" )
            {
                return false;
            }
            
            return true;
        }

		public void parse( String strMessage )
		{

			/*
			if ( strMessage.Length != MessageSchema.TOTAL_MESSAGE_LENGTH )
			{
				throw new Exception("Invalid weather message received.");
			}
			*/

			try
			{

  

				//
				//	Extract the wind direction minus the trailing D
				//
				
                //  Find the observation time

                //  Build time string
                String strTempTime = strMessage.Substring(MessageSchema.OBSERVATION_TIME_INDEX, 2) + "/" +
                                     strMessage.Substring(MessageSchema.OBSERVATION_TIME_INDEX + 2, 2) + "/" +
                                     strMessage.Substring(MessageSchema.OBSERVATION_TIME_INDEX + 4, 4) + " " +
                                     strMessage.Substring(MessageSchema.OBSERVATION_TIME_INDEX + 8, 2) + ":" +
                                     strMessage.Substring(MessageSchema.OBSERVATION_TIME_INDEX + 10, 2) + ":" +
                                     strMessage.Substring(MessageSchema.OBSERVATION_TIME_INDEX + 12, 2);

                _observationTime = System.DateTime.Parse(strTempTime);

				//	Find the Wind direction
				int	tempEndIndex = strMessage.IndexOf(MessageSchema.WIND_DIRECTION_TERMINATOR);
				String	temp = strMessage.Substring(MessageSchema.WIND_DIRECTION_INDEX,tempEndIndex-MessageSchema.WIND_DIRECTION_INDEX);
				_windDirection = Int32.Parse(temp);
				//	Extract the wind speed minus the trailing S
				int tempStartIndex = tempEndIndex +1;
				tempEndIndex = strMessage.IndexOf(MessageSchema.WIND_SPEED_TERMINATOR);
				temp = strMessage.Substring(tempStartIndex,tempEndIndex-tempStartIndex);
				_windSpeed = Double.Parse(temp);
				//	Extract the air temperature minus the trailing C
				tempStartIndex = tempEndIndex +1;
				tempEndIndex = strMessage.IndexOf(MessageSchema.AIR_TEMP_TERMINATOR);
				temp = strMessage.Substring(tempStartIndex,tempEndIndex-tempStartIndex);
				_airTemp = Double.Parse(temp);
				//	Extract the relative humidity minus the trailing 'H'
				tempStartIndex = tempEndIndex +1;
				tempEndIndex = strMessage.IndexOf(MessageSchema.RELATIVE_HUMIDITY_TERMINATOR);
				temp = strMessage.Substring(tempStartIndex,tempEndIndex-tempStartIndex);
				_relativeHumidity = Double.Parse(temp);
				//	Extract the air pressure minus the trailing P
				tempStartIndex = tempEndIndex +1;
				tempEndIndex = strMessage.IndexOf(MessageSchema.PRESSURE_TERMINATOR);
				temp = strMessage.Substring(tempStartIndex,tempEndIndex-tempStartIndex);
				_airPressure = Double.Parse(temp);
				//	Extract the heater state
				tempStartIndex = tempEndIndex +1;
				_heaterState		= getHeaterPercent(strMessage[tempStartIndex]);			
			}
			catch(Exception)
			{
				throw new Exception("Invalid weather message received.");
			}



		}



        #region Standard Comparer
        /// <summary>
        /// Compares on weather message to another, for comparrison purposes
        /// the time is used
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>

        public int CompareTo(object compareObject)
        {
            //  Use time as a comparission
            return observationTime.CompareTo(((WeatherMessage)compareObject).observationTime);
        }
        #endregion


        public string           wxSite                  { get { return _wxSite; } set { _wxSite = value; } }

        public System.DateTime  observationTime         { get { return _observationTime; } }
        public String           observationTimeString   { get { return _observationTime.ToString("MM/dd/yyyy HH:mm:ss"); } } 

		public int				windDirection			{ get { return _windDirection;									}  }
		public String			windDirectionString		{ get { return _windDirection.ToString();						}  }																												  

		public double			windSpeed				{ get { return _windSpeed;										}  }
		public String			windSpeedString			{ get { return String.Format("{0:0.0}",_windSpeed); }  }					
																							  
		public double			airTemp					{ get { return _airTemp;										}  }
		public String			airTempString			{ get { return String.Format("{0:0.0}", _airTemp);						}  }																												  

		public double			relativeHumidity		{ get { return _relativeHumidity;								}  }
		public String			relativeHumidityString	{ get { return String.Format("{0:0.0}", _relativeHumidity);			}  }																												  

		public double			airPressure				{ get { return _airPressure;									}  }
		public String			airPressureString		{ get { return String.Format("{0:0.0}",_airPressure);				}  }																												  

		public int				heaterState				{ get { return _heaterState;									}  }
		public String			heaterStateString		{ get { return _heaterState.ToString();							}  }


	}
}

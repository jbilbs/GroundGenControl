using System;

namespace GroundGenControl.Messages
{
	/// <summary>
	/// Summary description for StatusMessage.
	/// </summary>
	public class StatusMessage
	{

		public const string SEND_COMMAND = "7";


		public class MessageSchema
		{
        
			//	Message Code
			public const int MESSAGE_TYPE_INDEX			= 0;
			public const int MESSAGE_TYPE_LENGTH		= 1;
			//	Temp sensor
			public const int BURNER_STATE_INDEX			= MESSAGE_TYPE_INDEX + MESSAGE_TYPE_LENGTH; // 1
			public const int BURNER_STATE_LENGTH		= 1;
			//	Nozzle sensor
			public const int NOZZLE_STATE_INDEX			= BURNER_STATE_INDEX + BURNER_STATE_LENGTH; // 2 // also, should probably be BURNER_STATE_LENGTH, yes?
			public const int NOZZLE_STATE_LENGTH		= 1;	
			//	Nozzle sensor
			public const int SOLUTION_STATE_INDEX		= NOZZLE_STATE_INDEX + NOZZLE_STATE_LENGTH; // 3
			public const int SOLUTION_STATE_LENGTH		= 1;	
			//	Temp Sensor sensor
			public const int TEMPSENSOR_STATE_INDEX		= SOLUTION_STATE_INDEX + SOLUTION_STATE_LENGTH; // 4
			public const int TEMPSENSOR_STATE_LENGTH	= 1;
			//	Flame Temp (Farenheight)
			public const int FLAME_TEMP_INDEX			= TEMPSENSOR_STATE_INDEX + TEMPSENSOR_STATE_LENGTH; // 5
			public const int FLAME_TEMP_LENGTH			= 5;
			//	Batter voltage (Volts)1
			public const int BATTERY_VOLTS_INDEX		= FLAME_TEMP_INDEX + FLAME_TEMP_LENGTH; // 10
			public const int BATTERY_VOLTS_LENGTH		= 4;
			//	Flow (GPH)
			public const int FLOW_INDEX					= BATTERY_VOLTS_INDEX + BATTERY_VOLTS_LENGTH; // 14
			public const int FLOW_LENGTH				= 3;
			//	Purge Status
			public const int PURGE_STATE_INDEX			= FLOW_INDEX + FLOW_LENGTH; // 17
			public const int PURGE_STATE_PURGE_LENGTH	= 1;
			//	Igniter State
			public const int IGNITER_STATE_INDEX		= PURGE_STATE_INDEX + PURGE_STATE_PURGE_LENGTH; // 18
			public const int IGNITER_STATE_LENGTH		= 1;
			//	Seeding Status
			public const int SEEDING_STATUS_INDEX		= IGNITER_STATE_INDEX + IGNITER_STATE_LENGTH; // 19
			public const int SEEDING_STATUS_LENGTH		= 1;
			//	Date/Time
			public const int DATETIME_INDEX				= SEEDING_STATUS_INDEX + SEEDING_STATUS_LENGTH; // 20
			public const int DATETIME_LENGTH			= 15;
			//	Firmware version
			public const int FIRMWARE_INDEX				= DATETIME_INDEX + DATETIME_LENGTH; // 35
			public const int FIRMWARE_LENGTH			= 5;
			//	Pressure
			public const int PRESSURE_INDEX				= FIRMWARE_INDEX + FIRMWARE_LENGTH; // 40
			public const int PRESSURE_LENGTH			= 4;


			//	Ending CR			
			//public const int ENDING_CR_INDEX			= FLOW_INDEX + FLOW_LENGTH;
			//public const int ENDING_CR_LENGTH			= 1;

			public const int TOTAL_MESSAGE_LENGTH		= PRESSURE_INDEX + PRESSURE_LENGTH;
			//	Total message length including leading and trailing CR
//			public const int TOTAL_MESSAGE_LENGTH_CR	= SEEDING_STATUS_INDEX + SEEDING_STATUS_LENGTH+;

		}

		private System.DateTime	_parseTime			=	System.DateTime.Now;
		private	bool	_burnerState				=	false;
		private	bool	_nozzleState				=	false;
		private bool	_solutionState				=	false;
		private bool	_tempsensorState			=	false;
		private bool	_purgeState					=	false;
		private bool	_igniterState				=	false;
		private	bool	_seedingStatus				=	false;
		private System.DateTime	_remoteDateTime		=	System.DateTime.MinValue;


		private	double	_flameTemp			=	0;
		private double	_batteryVolts		=	0;
		private double	_flowMeter			=	0;
		private double  _firmwareVersion	=	0;
		private double	_pressure			=	0;

		public StatusMessage()
		{
		}

		public bool stringToBoolean( char convertChar )
		{
			if ( convertChar == '1' )
			{
				return true;
			}

			if ( convertChar == '0' )
			{
				return false;
			}

			throw new Exception("Invalid conversion from char to boolean.");

		}

		public String stateToString( bool state, String trueString, String falseString )
		{
			if ( state )
			{
				return trueString;
			}

			return falseString;

		}

		public System.DateTime parseDateTime( String strMessage )
		{

			try
			{
				String	formattedDateTime = strMessage.Substring(0,2) + "/" + 
					strMessage.Substring(2,2) + "/" +
					strMessage.Substring(4,4) + " " +
					strMessage.Substring(8,2) + ":" + 
					strMessage.Substring(10,2) + ":" + 
					strMessage.Substring(12,2);

				return System.DateTime.Parse(formattedDateTime);

			}
			catch(Exception)
			{
				return System.DateTime.MinValue;
			}

		}



		public void parse( String strMessage )
		{

            //strMessage = strMessage.Replace("\0","");
            //strMessage = strMessage.Replace("\r","");
            //strMessage = strMessage.Replace("\n","");
            strMessage = strMessage.Trim('\r');
			if ( strMessage.Length != MessageSchema.TOTAL_MESSAGE_LENGTH )
			{
				throw new Exception("Invalid status message received.");
			}
			
			_parseTime			= System.DateTime.Now;
			_burnerState		= stringToBoolean(strMessage[MessageSchema.BURNER_STATE_INDEX]); // 1
			_nozzleState		= stringToBoolean(strMessage[MessageSchema.NOZZLE_STATE_INDEX]); // 2
			_solutionState		= stringToBoolean(strMessage[MessageSchema.SOLUTION_STATE_INDEX]); // 3
			_tempsensorState	= stringToBoolean(strMessage[MessageSchema.TEMPSENSOR_STATE_INDEX]); // 4
			_purgeState			= stringToBoolean(strMessage[MessageSchema.PURGE_STATE_INDEX]); // 17
			_igniterState		= stringToBoolean(strMessage[MessageSchema.IGNITER_STATE_INDEX]); // 18
			_seedingStatus		= stringToBoolean(strMessage[MessageSchema.SEEDING_STATUS_INDEX]); // 19
			_remoteDateTime		= parseDateTime(strMessage.Substring(MessageSchema.DATETIME_INDEX,MessageSchema.DATETIME_LENGTH)); // 20

			//Extract flame temperature minus the trailing F
			String	temp = strMessage.Substring(MessageSchema.FLAME_TEMP_INDEX,MessageSchema.FLAME_TEMP_LENGTH-1);
			//	Convert it to an int, then/10 for final flame temp number
			_flameTemp = Double.Parse(temp)/(double)10;
			//	Extract battery volts minus ending V
			temp = strMessage.Substring(MessageSchema.BATTERY_VOLTS_INDEX,MessageSchema.BATTERY_VOLTS_LENGTH-1);
			_batteryVolts	=	Double.Parse(temp)/(double)10;
			//	Extract flow minus ending G
			temp = strMessage.Substring(MessageSchema.FLOW_INDEX,MessageSchema.FLOW_LENGTH-1);
			_flowMeter	=	Double.Parse(temp)/(double)100;
			//	Extract firmware version minus ending F
			temp = strMessage.Substring(MessageSchema.FIRMWARE_INDEX,MessageSchema.FIRMWARE_LENGTH-1);
			_firmwareVersion	=	Double.Parse(temp);
			//	Extract pressure minus ending P
			temp = strMessage.Substring(MessageSchema.PRESSURE_INDEX,MessageSchema.PRESSURE_LENGTH-1);
			_pressure	=	Double.Parse(temp)/(double)10;



		}

		public System.DateTime	parseTime			{ get { return _parseTime;										}  } 
		public String			parseTimeString		{ get { return _parseTime.ToString("MM/dd/yyyy HH:mm:ss");		}  } 
		public bool				burnerState			{ get { return _burnerState;									}  }
		public String			burnerStateString	{ get { return stateToString(_burnerState,"Open","Closed");			}  }
		public bool				nozzleState			{ get { return _nozzleState;									}  }
		public String			nozzleStateString	{ get { return stateToString(_nozzleState,"Open","Closed");		}  }
		public bool				solutionState		{ get { return _solutionState;									}  }
		public String			solutionStateString	{ get { return stateToString(_solutionState,"Open","Closed");	}  }
		public bool				tempsensorState		{ get { return _tempsensorState;								}  }
		public String			purgeStateString	{ get { return stateToString(_purgeState,"Open","Closed");		}  }
		public bool				purgeState			{ get { return _purgeState;										}  }
		public String			igniterStateString	{ get { return stateToString(_igniterState,"On","Off");			}  }
		public bool				igniterState		{ get { return _igniterState;									}  }
		public String			seedingStatusString	{ get { return stateToString(_seedingStatus,"Yes","No");		}  }
		public bool				seedingStatus		{ get { return _seedingStatus;									}  }



		public String			tempsensorStateString	{ get { return stateToString(_tempsensorState,"On","Off");	}  }
		public double			flameTemp				{ get { return _flameTemp;										}  }
		public String			flameTempString			{ get { return _flameTemp.ToString("{0:0.0}");					}  }																												  
		public double			batteryVolts			{ get { return _batteryVolts;									}  }
		public String			batteryVoltsString		{ get { return _batteryVolts.ToString("{0:0.0}");				}  }																												  
		public double			flowMeter				{ get { return _flowMeter;										}  }
		public String			flowMeterString			{ get { return _flowMeter.ToString("{0:0.0}");					}  }																												  
		public System.DateTime	remoteDateTime			{ get { return _remoteDateTime;									}  }
		public double			firmwareVersion			{ get { return _firmwareVersion;								}  }
		public String			firmwareVersionString	{ get { return _firmwareVersion.ToString("{0:0.0}");			}  }																												  
		public double			pressurePSI				{ get { return _pressure;									}  }
		public String			pressurePSIString		{ get { return _pressure.ToString("{0:0.0}");				}  }																												  



	}
}

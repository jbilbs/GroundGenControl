using System;
using System.Collections;
using GroundGenControl.Configuration;

namespace GroundGenControl.Communications
{
	/// <summary>
	/// Summary description for Command.
	/// </summary>
	public class RemoteCommand
	{



		public class Commands
		{

			public static string TOGGLE_PROPANE		    =	"1";
			public static string TOGGLE_NOZZLE		    =	"2";
			public static string TOGGLE_SOLUTION	    =	"3";
			public static string TOGGLE_PURGE		    =	"4";
			public static string IGNITER_ON			    =	"5";
			public static string CLOSE_ALL			    =	"6";
			public static string GET_STATUS			    =	"7";
			public static string START_SEEDING		    =	"A";
			public static string END_SEEDING		    =	"B";
			public static string GET_WEATHER		    =	"w";
            public static string GET_WEATHER_HISTORY    =   "h";
			public static string KEEP_ALIVE			    =	"k";

		}

		Hashtable commandMapper = new System.Collections.Hashtable();
		

        /*
         * This copy is prior to adding padding to the timeouts for SkyWave
         * 
		public RemoteCommand()
		{
			//	Populate hash table with command and result counts
			commandMapper.Add(Commands.TOGGLE_PROPANE	    , new CommandInfo(1,30,		"Toggle Propane"	));
			commandMapper.Add(Commands.TOGGLE_NOZZLE	    , new CommandInfo(1,30,		"Toggle Nozzle"		));
			commandMapper.Add(Commands.TOGGLE_SOLUTION	    , new CommandInfo(1,30,		"Toggle Solution"	));
			commandMapper.Add(Commands.TOGGLE_PURGE		    , new CommandInfo(2,420,	"Toggle Purge"		));
			commandMapper.Add(Commands.IGNITER_ON		    , new CommandInfo(2,60,		"Toggle Ignition"	));
			commandMapper.Add(Commands.CLOSE_ALL		    , new CommandInfo(1,60,		"Close"				));
			commandMapper.Add(Commands.GET_STATUS		    , new CommandInfo(1,30,		"Status Request"	));
			commandMapper.Add(Commands.START_SEEDING	    , new CommandInfo(3,240,	"Start Seeding"		));
			commandMapper.Add(Commands.END_SEEDING		    , new CommandInfo(2,180,	"End Seeding"		));
			commandMapper.Add(Commands.GET_WEATHER		    , new CommandInfo(1,90,		"Weather Request"	));
            commandMapper.Add(Commands.GET_WEATHER_HISTORY  , new CommandInfo(1,180,    "Weather Request"   ));
			commandMapper.Add(Commands.KEEP_ALIVE		    , new CommandInfo(0,15,		"Keep Alive"		));

		}
        */

		public RemoteCommand()
		{
			//	Populate hash table with command and result counts
			commandMapper.Add(Commands.TOGGLE_PROPANE	    , new CommandInfo(1,60,		"Toggle Propane"	));
			commandMapper.Add(Commands.TOGGLE_NOZZLE	    , new CommandInfo(1,60,		"Toggle Nozzle"		));
			commandMapper.Add(Commands.TOGGLE_SOLUTION	    , new CommandInfo(1,60,		"Toggle Solution"	));
			commandMapper.Add(Commands.TOGGLE_PURGE		    , new CommandInfo(2,450,	"Toggle Purge"		));
			commandMapper.Add(Commands.IGNITER_ON		    , new CommandInfo(2,90,		"Toggle Ignition"	));
			commandMapper.Add(Commands.CLOSE_ALL		    , new CommandInfo(1,90,		"Close"				));
			commandMapper.Add(Commands.GET_STATUS		    , new CommandInfo(1,60,		"Status Request"	));
			commandMapper.Add(Commands.START_SEEDING	    , new CommandInfo(3,270,	"Start Seeding"		));
			commandMapper.Add(Commands.END_SEEDING		    , new CommandInfo(2,210,	"End Seeding"		));
			commandMapper.Add(Commands.GET_WEATHER		    , new CommandInfo(1,120,	"Weather Request"	));
            commandMapper.Add(Commands.GET_WEATHER_HISTORY  , new CommandInfo(1,210,    "Weather Request"   ));
            // Note, keep alive not needed by Skywave, its left here in case we need to reserrect it for another service
            commandMapper.Add(Commands.KEEP_ALIVE		    , new CommandInfo(0,15,		"Keep Alive"		));

		}



		public int getStatusMessageCount(String command)
		{
			
			
			if ( !commandMapper.ContainsKey(command) )
			{
				throw new Exception("Unrecognized remote site command");
			}

			return ((CommandInfo) commandMapper[command]).numberOfResponses;


		}

		public int getStatusMessageTimeoutMS(String command)
		{
			
			if ( !commandMapper.ContainsKey(command) )
			{
				throw new Exception("Unrecognized remote site command");
			}

			return ((CommandInfo) commandMapper[command]).timeoutMS;

		}

		public int getStatusMessageTimeout(String command)
		{
			
			if ( !commandMapper.ContainsKey(command) )
			{
				throw new Exception("Unrecognized remote site command");
			}

            //  For the timeout, allow for the command time along with the satellite gateway polling interval time
			return ((CommandInfo) commandMapper[command]).timeoutSeconds + (GlobalConfiguration.instance.configuration.SkyWaveInfo.gatewayPollingIntervalMS/1000);

		}

		public String getStatusMessageDescription(String command)
		{
			
			if ( !commandMapper.ContainsKey(command) )
			{
				throw new Exception("Unrecognized remote site command");
			}

			return ((CommandInfo) commandMapper[command]).description;

		}

	}
}

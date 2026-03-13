using System;

namespace GroundGenControl.Messages
{
	/// <summary>
	/// Summary description for MessageType.
	/// </summary>
	public class MessageTypes
	{
		public class	MessageType
		{
			public const int	UNKNOWN			= 0;
			public const int	ERROR			= -1;
			public const int	STATUS			= 1;
			public const int	WEATHER			= 2;
            public const int    WEATHER_HISTORY = 3;
			public const int	INFORMATIONAL	= 4;

		}

		private class	MessageIdentifiers
		{
			public const char	ERROR			= 'E';
			public const char	STATUS			= 'S';
			public const char	WEATHER			= 'W';
			public const char	INFORMATIONAL	= 'I';
            public const char   WEATHER_HISTORY = 'H';

		}

	
		public MessageTypes()
		{
		}

		public static int getMessageType(String strMessage)
		{

			if ( strMessage.Length < 1 )
			{
				return MessageType.UNKNOWN;
			}

			if ( strMessage[0] == MessageIdentifiers.ERROR )
			{
				return MessageType.ERROR;
			}

			if ( strMessage[0] == MessageIdentifiers.STATUS )
			{
				return MessageType.STATUS;
			}

			if ( strMessage[0] == MessageIdentifiers.INFORMATIONAL )
			{
				return MessageType.INFORMATIONAL;
			}


			if ( strMessage[0] == MessageIdentifiers.WEATHER )
			{
				return MessageType.WEATHER;
			}

            if (strMessage[0] == MessageIdentifiers.WEATHER_HISTORY)
            {
                return MessageType.WEATHER_HISTORY;
            }


			//	Unknown message 
			return MessageType.UNKNOWN;

		}

	}
}

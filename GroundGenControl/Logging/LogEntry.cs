using System;
using System.Xml;
using System.Xml.Serialization;

namespace GroundGenControl.Logging
{
	/// <summary>
	/// Summary description for LogEntry.
	/// </summary>
	public class LogEntry
	{

		private System.DateTime	_logTime		= System.DateTime.Now;
		private String			_logSite		= "";
		private String			_logCommand		= "";
		private String			_logDescription	= "";

		public LogEntry()
		{

		}

		public LogEntry(System.DateTime logTime, String logSite, String logCommand, String logDescription)
		{
			_logTime		=	logTime;
			_logSite		=	logSite;
			_logCommand		=	logCommand;
			_logDescription	=	logDescription;

		}

		public LogEntry( String logSite, String logCommand, String logDescription)
		{
			_logTime		=	System.DateTime.Now;
			_logSite		=	logSite;
			_logCommand		=	logCommand;
			_logDescription	=	logDescription;

		}

	//	[XmlElementAttribute("logTime")]
		public System.DateTime logTime			{ get { return _logTime;				} set { _logTime = value;		} }
	//	[XmlElementAttribute("logSite")]
		public string logSite					{ get { return _logSite;				} set { _logSite = value;		} }
	//	[XmlElementAttribute("logCommand")]
		public string logCommand				{ get { return _logCommand;				} set { _logCommand = value;	} }
	//	[XmlElementAttribute("logDescription")]
		public string logDescription			{ get { return _logDescription;			} set { _logDescription = value;	} }


	}
}

using System;

namespace GroundGenControl.Communications
{
	/// <summary>
	/// Summary description for CommandInfo.
	/// </summary>
	public class CommandInfo
	{

		int		_numberOfResponses = 0;
		int		_timeoutSeconds	   = 0;
		String	_description	 = "";


		public CommandInfo(int numberOfResponses, int timeoutSeconds, String description)
		{
			_numberOfResponses	= numberOfResponses;
			_timeoutSeconds		= timeoutSeconds;
			_description		= description;

		}

		public int			timeoutSeconds				{ get { return _timeoutSeconds;			} }																   
		public int			timeoutMS					{ get { return _timeoutSeconds*1000;	} }																   
		public int			numberOfResponses			{ get { return _numberOfResponses;		} }																   
		public String		description					{ get { return _description;			} }
	

	}
}

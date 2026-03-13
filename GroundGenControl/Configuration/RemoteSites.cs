using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;


namespace GroundGenControl.Configuration
{
	/// <summary>
	/// Summary description for Configuration.
	/// </summary>
	/// 

	public class RemoteSites
	{

		[XmlArrayAttribute("Sites")]
		public Site[] _sites = {}; 

		public RemoteSites()
		{
		}
		


	}
}



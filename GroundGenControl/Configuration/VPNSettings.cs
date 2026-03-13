using System;
using System.Xml;
using System.Xml.Serialization;

namespace GroundGenControl.Configuration
{
	/// <summary>
	/// Summary description for VPNSettings.
	/// </summary>
	public class VPNSettings
	{
		private string  _connectionName;
		private string  _userName;
		private string  _password;

		public VPNSettings()
		{
		}

		public VPNSettings(String connectionName, String userName, String password)
		{
			_connectionName	= connectionName;
			_userName		= userName;
			_password		= password;

		}

		[XmlElementAttribute("connectionName")]
		public string connectionName	{ get { return _connectionName;			} set { _connectionName = value; } }
		[XmlElementAttribute("userName")]
		public string userName			{ get { return _userName;				} set { _userName = value;		 } }
		[XmlElementAttribute("password")]
		public string password			{ get { return _password;				} set { _password = value;       } }






	}
}

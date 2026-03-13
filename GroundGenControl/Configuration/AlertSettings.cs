using System;
using System.Xml;
using System.Xml.Serialization;

namespace GroundGenControl.Configuration
{
	/// <summary>
	/// Summary description for AlertSettings.
	/// </summary>
	public class AlertSettings
	{
		private double  _batteryVoltage;

		public AlertSettings()
		{
		}

		public AlertSettings(float batteryVoltage)
		{
			_batteryVoltage	= batteryVoltage;

		}

		[XmlElementAttribute("batteryVoltage")]
		public double batteryVoltage	{ get { return _batteryVoltage;			} set { _batteryVoltage = value; } }






	}
}

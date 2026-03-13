using System;
using System.Xml;
using System.Xml.Serialization;

namespace GroundGenControl.Weather
{
    class WeatherEntry
    {


        private System.DateTime _wxTime = System.DateTime.Now;
        private String _wxWindDirection         = "";
        private String _wxTemperature           = "";
        private String _wxPressure              = "";
        private String _wxRelativeHumidity      = "";
        private String _wxHeaterState           = "";


        public WeatherEntry()
		{

		}



	//	[XmlElementAttribute("wxTime")]
		public System.DateTime wxTime			{ get { return _wxTime;				    } set { _wxTime = value;		        } }
	//	[XmlElementAttribute("wxWindDirection")]
		public string wxWindDirection			{ get { return _wxWindDirection;		} set { _wxWindDirection = value;		} }
	//	[XmlElementAttribute("wxTemperature")]
		public string wxTemperature				{ get { return _wxTemperature;			} set { _wxTemperature = value;	        } }
	//	[XmlElementAttribute("wxPressure")]
		public string wxPressure			    { get { return _wxPressure;			    } set { _wxPressure = value;	        } }
	//	[XmlElementAttribute("wxRelativeHumidity")]
		public string wxRelativeHumidity	    { get { return _wxRelativeHumidity;	    } set { _wxRelativeHumidity = value;    } }
	//	[XmlElementAttribute("wxHeaterState")]
		public string wxHeaterState	            { get { return _wxHeaterState;	        } set { _wxHeaterState = value;         } }


    }
}

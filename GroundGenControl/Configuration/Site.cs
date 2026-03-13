using System;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;

namespace GroundGenControl.Configuration
{

	/// <summary>
	/// Summary description for Site.
	/// </summary>
	public class Site : IComparable, IComparer
	{

		private string  _name;
		private string  _mobileID;
        private bool    _checkWeather;

		public Site()
		{

		}

		public Site(String name, String mobileID, bool checkWeather)
		{
			_name	        = name;
            _mobileID       = mobileID;
            _checkWeather   = checkWeather;

		}

		/*
		 *	Compare interface used to allow sorting by name
		 * 
		 * 		Value Condition 
		 *			Less than zero x is less than y. 
		 *			Zero x equals y. 
		 *			Greater than zero x is greater than y.  
		 */

		public int Compare( object x, object y )
		{		
			//	Use standard string comparison to determine order
			return String.Compare(((Site) x).name,((Site) y).name);
		}
		
		public int CompareTo( object obj )
		{
			return String.Compare(this.name,((Site) obj).name);

		}

		[XmlElementAttribute("name")]
		public string name			{ get { return _name;			} set { _name = value; } }
		[XmlElementAttribute("mobileID")]
		public string mobileID { get { return _mobileID;				} set { _mobileID = value;		  } }
        [XmlElementAttribute("checkWeather")]
        public bool checkWeather { get { return _checkWeather; } set { _checkWeather = value; } }



	}
}

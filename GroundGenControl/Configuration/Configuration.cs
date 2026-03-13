using System;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;


//
//http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpref/html/frlrfsystemxmlserializationxmlserializerclasstopic.asp

namespace GroundGenControl.Configuration
{
	/// <summary>
	/// Summary description for Configuration.
	/// </summary>
	/// 

	[XmlRootAttribute("Configuration", Namespace="http://www.weathermod.com", IsNullable = false)]
	public class Configuration
	{

        //		[XmlAttribute("Sites")]
        //		public RemoteSites	_remoteSites = new RemoteSites();

        private SkyWaveSettings _skywaveInfo = new SkyWaveSettings();
		private Site[]			_sites			= {};
	//	private VPNSettings		_vpnConnectInfo	= new VPNSettings();
		private AlertSettings	_alertSettings = new AlertSettings();

        //  Create default site
        private Site _connectedSite = new Site("DISCONNECTED", "DISCONNECTED", false);


        public Configuration()
		{
	
		}

		public bool isNameInSites(String strName)
		{
			//	Loop through sites looking for name
			foreach ( Site siteInfo in _sites )
			{
				if ( siteInfo.name == strName )
				{
					//	Matching site found
					return true;
				}
			}

			//	No matching sites found
			return false;
		}

		public void addSite(Site siteToAdd)
		{

			//	Use arraylist to handle addition
			ArrayList arrayTemp = new ArrayList(_sites);
			arrayTemp.Add(siteToAdd);

			//	Clear old site reference
			_sites = null;
			//	Initialize a new array
			_sites = new Site[arrayTemp.Count];

			for ( int offset=0; offset<arrayTemp.Count; offset++ )
			{
				_sites[offset] = (Site) arrayTemp[offset];
			}

		}

		public void removeSite(String namedSiteToRemove)
		{

			//	Use arraylist to handle addition
			ArrayList arrayTemp = new ArrayList(_sites);

			for ( int offset=0; offset<arrayTemp.Count; offset++ )
			{
				if ( ((Site) arrayTemp[offset]).name == namedSiteToRemove )
				{
					arrayTemp.RemoveAt(offset);
				}
			}

			//	Clear old site reference
			_sites = null;
			//	Initialize a new array
			_sites = new Site[arrayTemp.Count];

			for ( int offset=0; offset<arrayTemp.Count; offset++ )
			{
				_sites[offset] = (Site) arrayTemp[offset];
			}

		}

		public void replaceSite(String oldName, Site siteToReplace)
		{

			//	Loop through sites looking for name
			foreach ( Site siteInfo in _sites )
			{
				if ( siteInfo.name == oldName )
				{
					//	Matching site found, replace site information
					siteInfo.name           = siteToReplace.name;
					siteInfo.mobileID       = siteToReplace.mobileID;
                    siteInfo.checkWeather   = siteToReplace.checkWeather;
				}
			}

		}


        public Site ConnectedSite { get { return _connectedSite; } set { _connectedSite = value; } }

        //  Skywave account
        public SkyWaveSettings SkyWaveInfo  { get { return _skywaveInfo;	} set { _skywaveInfo = value; } }
		[XmlArrayAttribute("Sites")]
        public Site[] sites				    { get { return _sites;			} set { _sites = value; } }
//		[XmlAttribute("VPNSettings")]
//		public  VPNSettings VPNConnectInfo	{ get { return _vpnConnectInfo;	} set { _vpnConnectInfo = value; } }
		public  AlertSettings AlertInfo		{ get { return _alertSettings;	} set { _alertSettings = value; } }


	}
}

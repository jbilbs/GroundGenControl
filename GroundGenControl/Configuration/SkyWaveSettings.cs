using System;
using System.Xml;
using System.Xml.Serialization;

namespace GroundGenControl.Configuration
{
    //  General skywave configuration
    public class SkyWaveSettings
    {

        //  Default gateway polling interval in seconds
        private const int GATEWAY_POLLING_INTERVAL_MS = 10000;

        //Skywave Access ID
        private string _accessID;
        //Skywave Pasword
        private string _password;
        // Mobile ID
        private string _mobileID;
         
        public SkyWaveSettings()
        {
        }

        public SkyWaveSettings(String accessID, String password, String mobileID)
        {

            _accessID = accessID;
            _password = password;
            _mobileID = mobileID;

        }


        public String getDecryptedPassword()
        {

            String strPassword = "";

            if (_password != null && _password != "")
            {
                strPassword = Encryption.Encryption.DecryptString(_password, Encryption.Encryption.DEFAULT_PASSPHRASE);
            }

            return strPassword;
        }


        //Returns the polling delay between each message get request
        public int gatewayPollingIntervalMS { get { return GATEWAY_POLLING_INTERVAL_MS; }  }


        [XmlElementAttribute("accessID")]
        public string accessID { get { return _accessID; } set { _accessID = value; } }
        [XmlElementAttribute("password")]
        public string password { get { return _password; } set { _password = value; } }



    }
}

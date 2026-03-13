using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace GroundGenControl.Messages
{


    class WeatherHistoryMessage
    {


        public const char HISTORY_SEPARATOR_CHARACTER = '|';
        ArrayList _WeatherMessageArray = new ArrayList();
        private String    _wxSite  = "";


        public WeatherHistoryMessage(String wxSite)
		{

            _wxSite = wxSite;
		}




        /// <summary>
        /// Parses a ground generator control Weather History Message
        /// </summary>
        /// <param name="strMessage">String message to parse</param>
        public void parse(String strMessage)
        {
            //  The weather string is in the format
            //  DATA|DATA|DATA, separting by the data string allows us to
            //  parse each individual data string

            //  Strip off the first command character from the message string
            String strModifiedMessage = strMessage.Substring(1);



            string[] messageArray = strModifiedMessage.Split(HISTORY_SEPARATOR_CHARACTER);


            //  Parse each individual weather string
            foreach (String historyMessage in messageArray)
            {
                WeatherMessage wxMessage = new WeatherMessage(_wxSite);
                wxMessage.parse(historyMessage);
                AddWeatherMessage(wxMessage);

            }


        }

        /// <summary>
        /// Adds a weather message if it is not already present in the weather array
        /// </summary>
        /// <param name="wxMessage"></param>
        public void AddWeatherMessage(WeatherMessage wxMessage)
        {
            foreach (WeatherMessage tempMessage in _WeatherMessageArray)
            {

                if (tempMessage.observationTime.CompareTo(wxMessage.observationTime) == 0)
                {
                    return;
                }
            }

            _WeatherMessageArray.Add(wxMessage);

            //  Added parsed message to weather array if it isn't already present
//            if (!_WeatherMessageArray.Contains(wxMessage))
  //          {
    //            _WeatherMessageArray.Add(wxMessage);
      //      }

        }

        public ArrayList WeatherMessageArray { get { return _WeatherMessageArray; } } 



    }


 

}

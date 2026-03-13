using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GroundGenControl.SkyWave
{
    /// <summary>
    /// This class encapsulates a raw payload with MIN and SIN
    /// </summary>
    public class PayloadContainer
    {

        int         _SIN = 0;
        int         _MIN = 0;
        byte[]       _rawPayload = null;
        String      _stringPayload = "";

        public PayloadContainer(byte[] rawPayload)
        {
            _rawPayload = rawPayload;
            ParseRawPayload();
        }


        private void ParseRawPayload()
        {

            if (_rawPayload.Length <2)
            {

                throw new Exception("Unable to read SkyWave message, invalid message format.");
            }


            //  Parse MIN ahnd SIN
            _SIN = _rawPayload[0];
            _MIN = _rawPayload[1];

            //  Extract bytes from rest of string ignoring the MIN and SIN bytes
            _stringPayload = Encoding.UTF8.GetString(_rawPayload, 2, _rawPayload.Length - 2);



        }



        public int SIN
        {
            get
            {
                return _SIN;
            }

            set
            {
                _SIN = value;
            }

        }

        public int MIN
        {
            get
            {
                return _MIN;
            }

            set
            {
                _MIN = value;
            }

        }

        public String stringPayload
        {
            get
            {
                return _stringPayload;
            }

            set
            {
                _stringPayload = value;
            }

        }

    }
}

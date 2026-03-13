using System;
using System.Windows.Forms;
using System.Text;
using System.Net;
using System.Net.Sockets;
using GroundGenControl.Configuration;
using GroundGenControl.SkyWave;

namespace GroundGenControl.Communications
{
	/// <summary>
	/// Summary description for RemoteConnectio, which is used to manage communications with the remote site
	/// </summary>
	public class RemoteConnection
	{

        //  Configuraiton class which holds program settings
        GlobalConfiguration _globalConfiguration = GlobalConfiguration.instance;
         //  Last command send to the ground generator unit
        String _lastCommand = "";


        delegate void delegateSubmit(string strMobileID, string strParamMessage);

        private static String _strGatewayTimeAtLastCommand = "";


        public RemoteConnection()
		{
		}

        public void updateGatewayTime(String gatewayTime)
        {
            _strGatewayTimeAtLastCommand = gatewayTime;
        }


        /// <summary>
        /// Sends a command string to the remote site
        /// </summary>
        /// <param name="strString"></param>
        /// <param name="callback"></param>
		public void startSendString( string strCommand, AsyncCallback callback )
		{

            //  This time will be used to filter any new messages
            //  We start the time prior to sending the command just in case there is a fast
            //  Response from the satellite the time we set if we set this later may not
            //  include the message
            _strGatewayTimeAtLastCommand = MessageTransceiver.instance.InfoUTC();


            MessageSender.activateThread(_globalConfiguration.configuration.ConnectedSite.mobileID,
                                         strCommand,
                                         callback);


            //	Keep alive is not considered a normal command type and should not be added
            //	to the last command variable
            if (strCommand != RemoteCommand.Commands.KEEP_ALIVE)
            {
                _lastCommand = strCommand;
            }




        }


		public void startReceiveString(AsyncCallback callback, int timeoutMS)
		{

            //	Set timeout for socket
            //_sock.SetSocketOption (SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, timeoutMS);
            //	Activate the receive message thread
            //MessageReceiver.activateThread(_sock, callback);
            //			startReceiveString(callback, _receiveBuffer.Length );

            MessageReceiver.activateThread(_globalConfiguration.configuration.ConnectedSite.mobileID,
                                            _strGatewayTimeAtLastCommand,
                                            timeoutMS, 
                                            callback);
               

		}
/*
		public string receiveString
		{
			get
			{
				System.Text.ASCIIEncoding encoder = new System.Text.ASCIIEncoding();
				return	encoder.GetString(_receiveBuffer);
			}
		}
*/
		public string lastCommand
		{
			get
			{
				return	_lastCommand;
			}
		}

        public string gatewayTimeAtLastCommand
        {
            get
            {
                return _strGatewayTimeAtLastCommand;
            }
        }
    }
}

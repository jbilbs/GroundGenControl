using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.ComponentModel;
using System.Text;
using GroundGenControl.Configuration;
using GroundGenControl.SkyWave;

namespace GroundGenControl.Communications
{
    /// <summary>
    /// Summary description for ReceiveMessageThread.
    /// </summary>
    public class MessageSender
    {

        //  Synronization conect for calling the UI thread
        private String _wxStationCommand            = "";
        private String _mobileID = "";
        private AsyncCallback _callback             = null;




        //
        //  Construction with synronization context
        //


        /*
		 *	 Module		 :	activateThread
		 * 
		 *	 Purpose	 :	This method returns a thread instance of the
		 *					ReceiveMessageThread class.
		 * 
		 *   Author		 :	Scott Brause
		 *					Cosairus LLC
		 * 
		 *	 Create Date :  August 31, 2017
		 * 
		 *	 Notes		 :	None
		 * 
		 *
		 */

        public static Thread activateThread(String strMobileID, String strCommand, AsyncCallback callback)
        {
            //	Create an instance of the MessageReceiver class
            MessageSender senderInstance = new MessageSender();

            //  Command to send
            senderInstance.wxStationCommand = strCommand;
            // Moible ID to send to
            senderInstance.mobileID = strMobileID;
            //  Callback function to call when complete
            senderInstance.callback = callback;
            //	Bind this instance to a new thread
            Thread senderThread = new Thread(new ThreadStart(senderInstance.StartThread));
            //	Start it
            senderThread.Start();

            return senderThread;

        }

        public void StartThread()
        {

            try
            {

                //  Configuraiton class which holds program settings
                GlobalConfiguration globalConfiguration = GlobalConfiguration.instance;

                MessageTransceiver.instance.SubmitForwardMessages(globalConfiguration.configuration.ConnectedSite.mobileID,
                                                       wxStationCommand);

                SendResult result = new SendResult();
                result.exceptionState = false;
                //	Call the caller
                _callback(result);

            }
            catch (Exception ex)
            {

                ReceiveResult result = new ReceiveResult();
                result.errorString = ex.Message;
                result.exceptionState = true;

                //	Call the caller
                _callback(result);
                return;


            }




        }



        public String wxStationCommand
        {
            get
            {
                return _wxStationCommand;
            }
            set
            {
                _wxStationCommand = value;
            }
        }

        public String mobileID
        {
            get
            {
                return _mobileID;
            }
            set
            {
                _mobileID = value;
            }
        }



        public AsyncCallback callback
        {
            get
            {
                return _callback;
            }
            set
            {
                _callback = value;
            }
        }

    }
}

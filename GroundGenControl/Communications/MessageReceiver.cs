using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using GroundGenControl.Configuration;
using GroundGenControl.SkyWave;

namespace GroundGenControl.Communications
{
	/// <summary>
	/// Summary description for ReceiveMessageThread.
	/// </summary>
	public class MessageReceiver
	{


		AsyncCallback	_callback	= null;
        String           _gatewayTimeAtLastCommand = "";
        String          _mobileID   = "";
        //  Timeout in milliseconds
        int             _timeoutMS = 0;

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

		public static Thread activateThread(String strMobileID, String strGatewayTimeAtLastCommand, int timeoutMS, AsyncCallback callback)
		{
			//	Create an instance of the DailyCallRecordDownloader class
			MessageReceiver receiverInstance = new MessageReceiver();

			receiverInstance.mobileID	= strMobileID;
            receiverInstance.gatewayTimeAtLastCommand = strGatewayTimeAtLastCommand;
            receiverInstance.callback	= callback;
            receiverInstance.timeoutMS  = timeoutMS;
			//	Bind this instance to a new thread
			Thread receiverThread = new Thread( new ThreadStart(receiverInstance.StartThread ));
			//	Start it
			receiverThread.Start();

			return receiverThread;

		}

		public void StartThread()
		{

			try
			{
               
                 
                //  Configuraiton class which holds program settings
                GlobalConfiguration globalConfiguration = GlobalConfiguration.instance;

                String strBuildResponse = "";

                // The stopwatch will be used to measure the timeout interval
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();

                //  In the timeout value allow for the expected response time from the ground gen unit + the gateway polling delay time
                int timeout = DateTime.Now.Millisecond + (timeoutMS + globalConfiguration.configuration.SkyWaveInfo.gatewayPollingIntervalMS);
                //  Message ID starts at 0 to use the last command time as a reference
                long lMessageID = 0;


                // Keep waiting for a response until we receive a valid response string or the command times out
                while (strBuildResponse.Length < 4096 && stopWatch.ElapsedMilliseconds<timeout )
				{
                    //reading 1 byte at a time
                    //int i=n.Read(bytes,totalbytes,1);

                    strBuildResponse += MessageTransceiver.instance.GetReturnSerialMessages(mobileID,
                                                                                            ref _gatewayTimeAtLastCommand,
                                                                                            ref lMessageID);


                    if ( strBuildResponse.Contains("\r") )
                    {

                        //  Only return the string up to (but not including) the first teminating character // This is no longer valid - single-response messages are coming across with valid data but no \r from Skywave
                        //  Command validity checks are performed minus the \r
                        //strBuildResponse = strBuildResponse.Substring(0, strBuildResponse.IndexOf('\r')); // This is no longer the case - new SkyWave devices are bundling return messages. not sure how best to handle this yet.

                        ReceiveResult result = new ReceiveResult();
                        result.exceptionState = false;
                        result.connected = true;
                        result.receiveString = strBuildResponse;
                        result.nextGatewayTime = _gatewayTimeAtLastCommand;
                        //	Call the caller
                        _callback(result);
                        //	Exit the thread
                        return;
                    }

                    //  Required delay by skywave in between each request
                    Thread.Sleep(globalConfiguration.configuration.SkyWaveInfo.gatewayPollingIntervalMS);


                }


                //  If we made it to this point, we never received a valid message and need to indicate that a valid message was not recieved
                ReceiveResult errResult = new ReceiveResult();
                errResult.exceptionState = true;
                errResult.errorString = "No valid message received";
                errResult.connected = true;
                errResult.receiveString = "";
                //	Call the caller
                _callback(errResult);
                //	Exit the thread
                return;

            }
			catch(	Exception ex )
			{

                ReceiveResult result = new ReceiveResult();
                result.errorString = ex.Message;
                result.exceptionState = true;
                result.connected = true;
                result.receiveString = "";

                //	Call the caller
                _callback(result);
                return;
 

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

        public String gatewayTimeAtLastCommand
        {
            get
            {
                return _gatewayTimeAtLastCommand;
            }
            set
            {
                _gatewayTimeAtLastCommand = value;
            }
        }

        public int timeoutMS
        {
            get
            {
                return _timeoutMS;
            }
            set
            {
                _timeoutMS = value;
            }
        }

        public AsyncCallback callback
		{
			get
			{
				return	_callback;
			}
			set
			{
				_callback = value;
			}
		}

	}
}

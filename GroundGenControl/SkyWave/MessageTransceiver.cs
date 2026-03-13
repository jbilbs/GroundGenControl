using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Globalization;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using SkyWave;
using GroundGenControl.Configuration;

namespace GroundGenControl.SkyWave
{
    public sealed class MessageTransceiver
    {


        private static readonly MessageTransceiver _instance = new MessageTransceiver();

        GlobalConfiguration _globalConfiguration = GlobalConfiguration.instance;

        //  URL must be HTTPS
        public const String SERVICE_URL = @"https://isatdatapro.skywave.com/GLGW/GWServices_v1/RestMessages.svc"; 


        private MessageService_JSON _gatewayInterface;
        private String _startUTC;
        private long _nextReturnID = 0;
        private Dictionary<int, ErrorInfo> _errorInfos;


        public MessageTransceiver()
        {

            string gatewayURL = SERVICE_URL;
            // prevent error due to lack of authentication certificate
            CustomBinding customBinding;

            ServicePointManager.ServerCertificateValidationCallback = TrustAllCertificatesCallback;
            var webHttpBinding = new WebHttpBinding(WebHttpSecurityMode.Transport);
            webHttpBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Windows;
            customBinding = new CustomBinding(webHttpBinding);

            var endpointAddress = new EndpointAddress(new Uri(gatewayURL));
            _gatewayInterface = new MessageService_JSON(customBinding, endpointAddress);

            _startUTC = InfoUTC();

            //  Retrieve error information for detailed display of error information to users 
            GetErrorInfos();

        }

        public static MessageTransceiver instance
        {
            get
            {
                return _instance;
            }
        }

        // prevent error due to lack of authentication certificate
        private static bool TrustAllCertificatesCallback(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }

        public String InfoUTC()
        {
            return _gatewayInterface.InfoUTC_J();
        }

        /// <summary>
        /// 
        /// </summary>
        public void GetErrorInfos()
        {
            Console.WriteLine("Get error information...");
            _errorInfos = new Dictionary<int, ErrorInfo>();
            var response = _gatewayInterface.GetErrorInfos_J();
            if (response != null)
            {
                foreach (ErrorInfo errorInfo in response)
                {
                    _errorInfos.Add(errorInfo.ID, errorInfo);
                }
            }
        }


        /// <summary>
        /// Gets skywave returned error information
        /// </summary>
        /// <param name="errorID"></param>
        /// <returns></returns>
        private string GetSkyWaveErrorString(int errorID)
        {
            if (_errorInfos != null && _errorInfos.ContainsKey(errorID))
            {
                var errorInfo = _errorInfos[errorID];
                return String.Format("ErrorID={0}, Name={1}, Description=\"{2}\"", errorID, errorInfo.Name, errorInfo.Description);
            }
            return String.Format("ErrorID={0}", errorID);
        }

        /// <summary>
        /// Returns date/time from skywave string
        /// </summary>
        /// <param name="gatewayDate"></param>
        /// <returns></returns>
        private DateTime GetSkyWaveDateFromString(string gatewayDate)
        {
            DateTime date;
            if (DateTime.TryParseExact(gatewayDate, "yyyy-MM-dd HH:mm:ss", DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out date))
                return date;
            return DateTime.MinValue;
        }


    

        public void SubmitForwardMessages(String strMobileID, String strParamMessage)
        {

            //  Create a list to store the message to send
            List<ForwardMessage> fwdMessages = new List<ForwardMessage>();

            // Sennd message
            //  SIN 129 which is the serial streaming service
            //  Min 3 which is TxRaw data
//            CommonMessage message = new CommonMessage { SIN = 129, MIN = 3 };


            //  Build SIN and MIN
            byte[] buildService = { 0x81, 0x3 };
            //  Build message to send as byte array
            byte[] buildMessage = Encoding.UTF8.GetBytes(strParamMessage);
            //  Concat build message onto SIN and MIN 
            byte[] buildPayload = new byte[buildService.Length + buildMessage.Length];
            System.Buffer.BlockCopy(buildService, 0, buildPayload, 0, buildService.Length);
            System.Buffer.BlockCopy(buildMessage, 0, buildPayload, buildService.Length, buildMessage.Length);



            //            string raw64 = Convert.ToBase64String(buildPaylod);


            fwdMessages.Add(new ForwardMessage { DestinationID = strMobileID,
                                                     UserMessageID = 1,
                                                     RawPayload = buildPayload
            });


            SubmitMessagesResult msgResult = _gatewayInterface.SubmitForwardMessages_J(_globalConfiguration.configuration.SkyWaveInfo.accessID,
                                                                                       _globalConfiguration.configuration.SkyWaveInfo.getDecryptedPassword(),
                                                                                       fwdMessages.ToArray<ForwardMessage>());
            if (msgResult != null)
            {
                if (msgResult.ErrorID == 0)
                {
                    if (msgResult.Submissions != null)
                    {
                        foreach (ForwardSubmission submissionInfo in msgResult.Submissions)
                        {
                            if (msgResult.ErrorID == 0)
                                Console.WriteLine("  Forward message submitted: UserMessageID={0}, ForwardMessageID={1}", submissionInfo.UserMessageID, submissionInfo.ForwardMessageID);
                            else
                                Console.WriteLine("  Forward message rejected: UserMessageID={0}, {1}", submissionInfo.UserMessageID, GetSkyWaveErrorString(msgResult.ErrorID));
                        }
                    }
                }
                else
                    Console.WriteLine("  Error in SubmitForwardMessages(): {0}", GetSkyWaveErrorString(msgResult.ErrorID));
            }

            /*
            Console.WriteLine("Submitting forward messages...");
            var forwardMessages = new List<ForwardMessage>();

            // terminal getTerminalInfo message
            var getTerminalInfoMessage = new CommonMessage { SIN = 16, MIN = 1 };
            forwardMessages.Add(new ForwardMessage { DestinationID = _globalConfiguration.configuration.SkyWaveInfo.mobileID, UserMessageID = 1, Payload = getTerminalInfoMessage });

            // terminal getPosition message
            var getPositionMessage = new CommonMessage { SIN = 20, MIN = 1 };
            getPositionMessage.Fields.Add(new CommonMessageField { Name = "fixType", Value = "3D" });
            getPositionMessage.Fields.Add(new CommonMessageField { Name = "timeout", Value = "120" });
            forwardMessages.Add(new ForwardMessage { DestinationID = WebServiceAccount.MobileID, UserMessageID = 2, Payload = getPositionMessage });

            var response = _gatewayInterface.SubmitForwardMessages_J(WebServiceAccount.AccessID, WebServiceAccount.Password, forwardMessages.ToArray<ForwardMessage>());
            if (response != null)
            {
                if (response.ErrorID == 0)
                {
                    if (response.Submissions != null)
                    {
                        foreach (ForwardSubmission submissionInfo in response.Submissions)
                        {
                            if (response.ErrorID == 0)
                                Console.WriteLine("  Forward message submitted: UserMessageID={0}, ForwardMessageID={1}", submissionInfo.UserMessageID, submissionInfo.ForwardMessageID);
                            else
                                Console.WriteLine("  Forward message rejected: UserMessageID={0}, {1}", submissionInfo.UserMessageID, GetErrorString(submissionInfo.ErrorID));
                        }
                    }
                }
                else
                    Console.WriteLine("  Error in SubmitForwardMessages(): {0}", GetErrorString(response.ErrorID));
            }
            */
        }

        /// <summary>
        /// This method returns serial message data from the gateway
        /// </summary>
        public String GetReturnSerialMessages(String strMobileID, ref string strFilterUTC, ref long lMessageID)
        {


            /*


            string startUTC = null;
            long startID = 0;

            startUTC = strFilterUTC;
            

            if (_nextReturnID > 0)
            {
                startID = _nextReturnID;
            }
            else
            {
                startUTC = _startUTC;
            }
            */



            String strBuilder = "";

            //            public GetReturnMessagesResult GetReturnMessages_J(String accessID, String password, long fromID, String startUTC, String endUTC, String mobileID, bool includeRawPayload, String subAccountID, bool includeType, bool includeSubaccounts)


            GetReturnMessagesResult msgResult = _gatewayInterface.GetReturnMessages_J(_globalConfiguration.configuration.SkyWaveInfo.accessID,
                                                                                      _globalConfiguration.configuration.SkyWaveInfo.getDecryptedPassword(),
                                                                                      lMessageID,
                                                                                      strFilterUTC,
                                                                                      String.Empty,
                                                                                      strMobileID,
                                                                                      true,
                                                                                      String.Empty,
                                                                                      false,
                                                                                      false);


            if (msgResult != null)
            {
                if (msgResult.ErrorID == 0)
                {
                    if (msgResult.Messages != null && msgResult.Messages.Length > 0)
                    {
                        strFilterUTC = msgResult.NextStartUTC;

                        foreach (var message in msgResult.Messages)
                        {
                            DateTime gatewayTime = GetSkyWaveDateFromString(message.MessageUTC);
                            Console.WriteLine("Return message received: ID={0}, MobileID={1}, Time={2}", message.ID, message.MobileID, gatewayTime);

                            //  Make sure the message we are reading is of a valid type
                            if ( message.SIN == 129 && message.RawPayload != null && message.RawPayload[1] == 3 )
                            { 

                                PayloadContainer payloadContainer = new PayloadContainer(message.RawPayload);
                                strBuilder += payloadContainer.stringPayload;

                            }

                        }

                        


                        if (msgResult.NextStartID > 0)
                        {
                            lMessageID = msgResult.NextStartID;
                        }
                        else
                        {
                            lMessageID = msgResult.Messages[msgResult.Messages.Length - 1].ID;
                        }

                        return strBuilder;

                    }
                    else
                    { 
                        Console.WriteLine("No return messages received");
                    }
                }
                else
                { 
                    Console.WriteLine("Error in GetReturnMessages(): {0}", GetSkyWaveErrorString(msgResult.ErrorID));
                }


            }

            return strBuilder;

            /*


            if (msgResult != null)
            {
                if (msgResult.ErrorID == 0)
                {
                    if (msgResult.Messages != null && msgResult.Messages.Length > 0)
                    {
                        foreach (var message in msgResult.Messages)
                        {
                            Console.WriteLine("Return message received: ID={0}, MobileID={1}, Time={2}", message.ID, message.MobileID, GetSkyWaveDateFromString(message.MessageUTC));
                            if (message.RawPayload != null)
                                PrintRawPayload(message.RawPayload);
                            if (message.Payload != null)
                                PrintPayload(message.Payload);
                        }
                        if (response.NextStartID > 0)
                            _nextReturnID = response.NextStartID;
                        else
                            _nextReturnID = response.Messages[response.Messages.Length - 1].ID;
                    }
                    else
                        Console.WriteLine("No return messages received");
                }
                else
                    Console.WriteLine("Error in GetReturnMessages(): {0}", GetErrorString(response.ErrorID));
            }
            */
        }

    }
}

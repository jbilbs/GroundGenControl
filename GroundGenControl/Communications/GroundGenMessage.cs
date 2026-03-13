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
using GroundGenControl.SkyWave;


using GroundGenControl.Configuration;

namespace GroundGenControl.Communications
{
    class GroundGenMessage
    {



        GlobalConfiguration _globalConfiguration = GlobalConfiguration.instance;

        //  URL must be HTTPS
        public const String SERVICE_URL = @"https://isatdatapro.skywave.com/GLGW/GWServices_v1/RestMessages.svc"; 


        private MessageService_JSON _gatewayInterface;
        private String _startUTC;
        private String _nextStatusUTC;
        private long _nextReturnID = 0;

        private Dictionary<int, ErrorInfo> _errorInfos;
        private Dictionary<String, SubaccountInfo> _subaccountInfos;
        private Dictionary<String, MobileExInfo> _mobileInfos;

        public GroundGenMessage()
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


        }


        // prevent error due to lack of authentication certificate
        protected static bool TrustAllCertificatesCallback(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }

        private String InfoUTC()
        {
            return _gatewayInterface.InfoUTC_J();
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
        public String GetReturnSerialMessages(String strMobileID)
        {

            string startUTC = null;
            long startID = 0;
            
            if (_nextReturnID > 0)
            {
                startID = _nextReturnID;
            }
            else
            {
                startUTC = _startUTC;
            }

            String strResult = "";

            //            public GetReturnMessagesResult GetReturnMessages_J(String accessID, String password, long fromID, String startUTC, String endUTC, String mobileID, bool includeRawPayload, String subAccountID, bool includeType, bool includeSubaccounts)


            GetReturnMessagesResult msgResult = _gatewayInterface.GetReturnMessages_J(_globalConfiguration.configuration.SkyWaveInfo.accessID,
                                                                                      _globalConfiguration.configuration.SkyWaveInfo.getDecryptedPassword(),
                                                                                      startID,
                                                                                      startUTC,
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
                        foreach (var message in msgResult.Messages)
                        {
                            Console.WriteLine("Return message received: ID={0}, MobileID={1}, Time={2}", message.ID, message.MobileID, GetSkyWaveDateFromString(message.MessageUTC));
                            if (message.RawPayload != null)
                            {
                                PayloadContainer payloadContainer = new PayloadContainer(message.RawPayload);
                                

                                //  Make sure the message we are reading is of a valid type
                                if (payloadContainer.SIN == 129 && payloadContainer.MIN == 3)
                                {
                                    return payloadContainer.stringPayload;

                                }

                            }

                        }
                        if (msgResult.NextStartID > 0)
                            _nextReturnID = msgResult.NextStartID;
                        else
                            _nextReturnID = msgResult.Messages[msgResult.Messages.Length - 1].ID;
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

            return strResult;

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

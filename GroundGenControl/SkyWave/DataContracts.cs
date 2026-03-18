using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.IO;
using System.Xml;
using System.Net;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace SkyWave
{
    #region Forward messages

    [DataContract(Namespace = "", Name = "PriorityLevel")]
    public enum PriorityLevel
    {
        [EnumMember]
        HIGH = 2,
        [EnumMember]
        NORMAL = 3,
        [EnumMember]
        LOW = 4
    }
    [DataContract(Namespace = "", Name = "ForwardMessageArray")]
    [XmlType("ForwardMessageArray")]
    public class ForwardMessageArray
    {
        [DataMember(Name = "Messages", Order = 0, IsRequired = false)]
        [Newtonsoft.Json.JsonProperty("Messages")]
        public ForwardMessage[] SubmitMessages = null;
    }


    /// <summary>
    /// Forward link message sent to the Gateway from customers
    /// </summary>
    [DataContract(Namespace = "", Name = "ForwardMessage")]
    [XmlType("ForwardMessage")]
    public class ForwardMessage
    {
        /// <summary>
        /// Destination terminal's ID 
        /// </summary>
        [DataMember(Name = "DestinationID", Order = 0, IsRequired = false)]
        public String DestinationID = null;

        /// <summary>
        /// User supplies this ID in order to be able to pair Gateway generated message ID to his own message ID
        /// </summary>
        [DataMember(Name = "UserMessageID", Order = 1, IsRequired = false, EmitDefaultValue = true)]
        public int UserMessageID = -1;
        //[XmlIgnore]
        //public bool UserMessageIDSpecified { get { return UserMessageID != -1; } }        

        /// <summary>
        /// Raw payload 
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")]
        [DataMember(Name = "RawPayload", Order = 3, IsRequired = false, EmitDefaultValue = false)]
        public System.Byte[] RawPayload = null;
        /// <summary>
        /// Message content
        /// </summary>
        [DataMember(Name = "Payload", Order = 4, IsRequired = false, EmitDefaultValue = false)]
        public CommonMessage Payload = null;

        public String ToXMLString()
        {
            XmlSerializer serializer = new XmlSerializer(this.GetType());
            StringWriter sw = new StringWriter();
            serializer.Serialize(sw, this);

            return sw.ToString();
        }

        public String ToXMLString(int maxSize)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(this.GetType());
                StringWriter sw = new StringWriter();
                serializer.Serialize(sw, this);

                String resStr = sw.ToString();
                if (resStr.Length > maxSize)
                    return resStr.Substring(0, maxSize);
                else
                    return resStr;
            }
            catch { }

            return "";
        }

        public static ForwardMessage NewInstance(String xmlContent)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ForwardMessage));
            StringReader sr = new StringReader(xmlContent);
            return (ForwardMessage)serializer.Deserialize(sr);
        }

        public String ToShortString()
        {
            int sin = -1, min = -1;
            if (RawPayload != null)
            {
                int size = RawPayload.Length;
                if (RawPayload.Length >= 2)
                {
                    sin = RawPayload[0];
                    min = RawPayload[1];
                }
                return String.Format("{0},{1}, raw:{2}/{3}/{4}", DestinationID, UserMessageID, sin, min, size);
            }
            else if (Payload != null)
            {
                sin = Payload.SIN;
                min = Payload.MIN;
                return String.Format("{0},{1}, {2}/{3}", DestinationID, UserMessageID, sin, min);
            }
            else
            {
                return String.Format("{0},{1}, (null)", DestinationID, UserMessageID);
            }
        }

        public String ToString(int maxSize)
        {
            return ToXMLString(maxSize);
        }
    }

    /// <summary>
    /// This class is used to notify customers about success or failure of their forward message submition to the Gateway
    /// The whole message submition request can faile or succeed, and in case it succeeds, individual forward messages can 
    /// fail or succeed on submission. In case they do succeed, an ID is generated and paired with a user supplied message ID.
    /// </summary>
    [DataContract(Namespace = "", Name = "SubmitMessagesResult")]
    public class SubmitMessagesResult
    {
        /// <summary>
        /// Error code related to the ERROR Status
        /// </summary>
        [DataMember(Name = "ErrorID", Order = 0, IsRequired = true)]
        public int ErrorID = 0;

        /// <summary>
        /// Array of statuses related to individual forward messages sent by a customer
        /// </summary>
        [DataMember(Name = "Submissions", Order = 1, IsRequired = false, EmitDefaultValue = false)]
        public ForwardSubmission[] Submissions = null;
    }

    /// <summary>
    /// Send status of a single forward message. If the 'Send' operation for the messages succeeds, an ID will be generated
    /// and associated to the customer supplied UserMessageID
    /// </summary>
    [DataContract(Namespace = "", Name = "ForwardSubmission")]
    public class ForwardSubmission
    {
        /// <summary>
        /// Forward message ID generated by the Gateway
        /// </summary>
        [DataMember(Name = "ForwardMessageID", Order = 0, IsRequired = true)]
        public int ForwardMessageID = -1;

        [DataMember(Name = "DestinationID", Order = 1, IsRequired = true)]
        public String DestinationID = "";

        [DataMember(Name = "StateUTC", Order = 2, IsRequired = true)]
        public String StateUTC = "";

        /// <summary>
        /// Error code related to the ERROR Status of the message (in case the Gateway rejects the message)
        /// </summary>
        [DataMember(Name = "ErrorID", Order = 3, IsRequired = false, EmitDefaultValue = true)]
        public int ErrorID = 0;
        //[XmlIgnore]
        //public bool ErrorIDSpecified { get { return ErrorID != 0; } }

        /// <summary>
        /// Forward message ID supplied by the customer. This ID is used to map customer's message IDs to Gateway message IDs
        /// </summary>
        [DataMember(Name = "UserMessageID", Order = 4, IsRequired = false, EmitDefaultValue = true)]
        public int UserMessageID = 0;
        //[XmlIgnore]
        //public bool UserMessageIDSpecified { get { return UserMessageID != 0; } }

        [DataMember(Name = "ScheduledSendUTC", Order = 5, IsRequired = false)]
        public String ScheduledSendUTC = "";

        [DataMember(Name = "TerminalWakeupPeriod", Order = 6, IsRequired = false, EmitDefaultValue = true)]
        public int TerminalWakeupPeriod = 0;
        //[XmlIgnore]
        //public bool TerminalWakeupPeriodSpecified { get { return TerminalWakeupPeriod != 0; } }    

        [DataMember(Name = "OTAMessageSize", Order = 7, IsRequired = false, EmitDefaultValue = true)]
        public int? OTAMessageSize = null;
        [XmlIgnore]
        public bool OTAMessageSizeSpecified { get { return (OTAMessageSize.HasValue && OTAMessageSize != 0); } }
    }

    [DataContract(Namespace = "", Name = "ForwardStateEnum")]
    public enum ForwardStateEnum
    {
        [EnumMember]
        SUBMITTED = 0,           // FWMessageStateEnum.MSG_SENT_TO_PPC
        [EnumMember]
        RECEIVED = 1,       // MSG_RECEIVED_BY_MT, MSG_REPLY_RECEIVED, FW_SYSMSG_RECEIVED
        [EnumMember]
        ERROR = 2,          // MSG_MT_ERROR, MSG_PPC_ERROR, MSG_GW_ERROR
        [EnumMember]
        DELIVERY_FAILED = 3,// MSG_PPC_FAILED_TO_DELIVER
        [EnumMember]
        TIMED_OUT = 4,      // MSG_TIMEOUT
        [EnumMember]
        CANCELLED = 5,       // MSG_CANCELED
        [EnumMember]
        WAITING = 6,        // Message saved, but not sent to MPC (DND terminal)
        [EnumMember]
        NA = 7              // Not Applicable
    }

    [DataContract(Namespace = "", Name = "GetForwardStatusesResult")]
    public class GetForwardStatusesResult
    {
        [DataMember(Name = "ErrorID", Order = 0, IsRequired = true)]
        public int ErrorID = 0;

        [DataMember(Name = "More", Order = 1, IsRequired = true)]
        public bool More = false;

        [DataMember(Name = "NextStartUTC", Order = 2, IsRequired = false)]
        public String NextStartUTC = null;

        [DataMember(Name = "Statuses", Order = 3, IsRequired = false, EmitDefaultValue = true)]
        public ForwardStatus[] Statuses = null;
    }

    [DataContract(Namespace = "", Name = "ForwardStatusFilter")]
    public class ForwardStatusFilter
    {
        [DataMember(Name = "ForwardMessageIDs", Order = 0, IsRequired = false)]
        public int[] ForwardMessageIDs = null;

        [DataMember(Name = "StartUTC", Order = 1, IsRequired = false)]
        public String StartUTC = null;

        [DataMember(Name = "EndUTC", Order = 2, IsRequired = false)]
        public String EndUTC = null;

        [DataMember(Name = "SubaccountID", Order = 3, IsRequired = false)]
        public String SubaccountID = null;

        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (ForwardMessageIDs == null)
                sb.Append("(null)");
            else
            {
                foreach (int id in ForwardMessageIDs)
                    sb.Append(id.ToString() + ",");
            }
            return String.Format("{0},{1},{2}, fwIDs:{3}", StartUTC, EndUTC, SubaccountID, sb.ToString());
        }
    }

    [DataContract(Namespace = "", Name = "ForwardStatus")]
    public class ForwardStatus
    {
        [DataMember(Name = "ForwardMessageID", Order = 0, IsRequired = true)]
        public int ForwardMessageID = 0;

        [DataMember(Name = "IsClosed", Order = 1, IsRequired = true)]
        public bool IsClosed = false;

        [DataMember(Name = "State", Order = 2, IsRequired = true)]
        public ForwardStateEnum State = ForwardStateEnum.NA;

        [DataMember(Name = "ErrorID", Order = 3, IsRequired = false, EmitDefaultValue = true)]
        public int ErrorID = 0;
        //[XmlIgnore]
        //public bool ErrorIDSpecified { get { return ErrorID != 0; } }

        [DataMember(Name = "StateUTC", Order = 4, IsRequired = true)]
        public String StateUTC = null;

        [DataMember(Name = "ReferenceNumber", Order = 5, IsRequired = false, EmitDefaultValue = true)]
        public int ReferenceNumber = -1;
        //[XmlIgnore]
        //public bool ReferenceNumberSpecified { get { return ReferenceNumber != -1; } }  
    }

    [DataContract(Namespace = "", Name = "GetForwardMessagesResult")]
    public class GetForwardMessagesResult
    {
        [DataMember(Name = "ErrorID", Order = 0, IsRequired = true)]
        public int ErrorID = 0;

        [DataMember(Name = "Messages", Order = 1, IsRequired = false)]
        public ForwardMessageRecord[] Messages = null;
    }

    [DataContract(Namespace = "", Name = "ForwardMessageRecord")]
    public class ForwardMessageRecord
    {
        [DataMember(Name = "ID", Order = 0, IsRequired = true)]
        public int ID = 0;

        [DataMember(Name = "StatusUTC", Order = 1, IsRequired = false)]
        public String StatusUTC = null;

        [DataMember(Name = "CreateUTC", Order = 2, IsRequired = false)]
        public String CreateUTC = null;

        [DataMember(Name = "IsClosed", Order = 3, IsRequired = true)]
        public bool IsClosed = false;

        [DataMember(Name = "State", Order = 4, IsRequired = true)]
        public ForwardStateEnum State = ForwardStateEnum.NA;

        [DataMember(Name = "DestinationID", Order = 6, IsRequired = true)]
        public String DestinationID;

        [DataMember(Name = "ErrorID", Order = 7, IsRequired = false, EmitDefaultValue = true)]
        public int ErrorID = 0;
        //[XmlIgnore]
        //public bool ErrorIDSpecified { get { return ErrorID != 0; } }

        [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")]
        [DataMember(Name = "RawPayload", Order = 8, IsRequired = false, EmitDefaultValue = false)]
        public System.Byte[] RawPayload = null;

        [DataMember(Name = "Payload", Order = 9, IsRequired = false, EmitDefaultValue = false)]
        public CommonMessage Payload = null;

        [DataMember(Name = "ReferenceNumber", Order = 10, IsRequired = false, EmitDefaultValue = true)]
        public int ReferenceNumber = -1;
        //[XmlIgnore]
        //public bool ReferenceNumberSpecified { get { return ReferenceNumber != -1; } }   
    }

    #endregion

    #region Return messages

    [DataContract(Namespace = "", Name = "ReturnMessageFilter")]
    public class ReturnMessageFilter
    {
        [DataMember(Name = "StartUTC", Order = 0, IsRequired = true)]
        public String StartUTC;

        [DataMember(Name = "EndUTC", Order = 1, IsRequired = false)]
        public String EndUTC = null;

        [DataMember(Name = "MobileID", Order = 2, IsRequired = false)]
        public String MobileID = null;

        [DataMember(Name = "IncludeRawPayload", Order = 3, IsRequired = true)]
        public bool IncludeRawPayload = false;

        [DataMember(Name = "SubAccountID", Order = 4, IsRequired = false)]
        public String SubAccountID = null;

        [DataMember(Name = "StartMessageID", Order = 5, IsRequired = false, EmitDefaultValue = true)]
        public long StartMessageID = -1;

        [DataMember(Name = "IncludeType", Order = 6, IsRequired = false, EmitDefaultValue = false)]
        public bool? IncludeType = null;

        [XmlIgnore]
        public bool IncludeTypeSpecified { get { return (IncludeType.HasValue && IncludeType != false); } }

        [DataMember(Name = "IncludeAllSubaccounts", Order = 7, IsRequired = false, EmitDefaultValue = false)]
        public bool? IncludeAllSubaccounts = null;
        [XmlIgnore]
        public bool IncludeAllSubaccountsSpecified { get { return (IncludeAllSubaccounts.HasValue && IncludeAllSubaccounts != false); } }

        public override String ToString()
        {
            return String.Format("{0},{1},{2},{3},{4},{5},{6},{7}",
                StartUTC, EndUTC, MobileID, IncludeRawPayload, SubAccountID, StartMessageID, IncludeType, IncludeAllSubaccounts);
        }
    }

    /// <summary>
    /// This class defines the return value of GetMessages operation.
    /// </summary>
    [DataContract(Namespace = "", Name = "GetReturnMessagesResult")]
    public class GetReturnMessagesResult
    {
        [DataMember(Name = "ErrorID", Order = 0, IsRequired = true)]
        public int ErrorID = 0;

        [DataMember(Name = "More", Order = 1, IsRequired = true)]
        public bool More = false;

        [DataMember(Name = "NextStartUTC", Order = 2, IsRequired = true)]
        public String NextStartUTC = "";

        [DataMember(Name = "Messages", Order = 3, IsRequired = false, EmitDefaultValue = true)]
        public ReturnMessage[] Messages = null;

        [DataMember(Name = "NextStartID", Order = 4, IsRequired = true, EmitDefaultValue = true)]
        public long NextStartID = -1;
    }

    [XmlType("ReturnMessage")]
    [DataContract(Namespace = "", Name = "ReturnMessage")]
    public class ReturnMessage
    {
        /// <summary>
        /// ID of a response message 
        /// </summary>
        [DataMember(Name = "ID", Order = 0, IsRequired = true)]
        public long ID = 0;

        /// <summary>
        /// A messag timestamp in a MPC format (created by the Gateway)
        /// </summary>
        [DataMember(Name = "MessageUTC", Order = 1, IsRequired = true)]
        public String MessageUTC = "";

        /// <summary>
        /// A messag timestamp in a MPC format (created by the MPC)
        /// </summary>
        [DataMember(Name = "ReceiveUTC", Order = 2, IsRequired = false)]
        public String ReceiveUTC = "";

        /// <summary>
        /// Message's Service Identification number
        /// </summary>
        [DataMember(Name = "SIN", Order = 3, IsRequired = true)]
        public int SIN = 0;

        /// <summary>
        /// Terminal's serial number
        /// </summary>
        [DataMember(Name = "MobileID", Order = 4, IsRequired = true)]
        public String MobileID = "";

        /// <summary>
        /// Raw payload 
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")]
        [DataMember(Name = "RawPayload", Order = 5, IsRequired = false, EmitDefaultValue = false)]
        public System.Byte[] RawPayload = null;

        /// <summary>
        /// Message content
        /// </summary>
        [DataMember(Name = "Payload", Order = 6, IsRequired = false, EmitDefaultValue = false)]
        public CommonMessage Payload = null;

        [DataMember(Name = "RegionName", Order = 7, IsRequired = false)]
        public String RegionName = "";
    }
    #endregion

    #region Administration

    [DataContract(Namespace = "", Name = "MobileInfo")]
    public class MobileInfo
    {
        public MobileInfo(String mtsn, String description)
        {
            ID = mtsn;
            Description = description;
        }

        public MobileInfo() { } // Required by the WS

        [DataMember(Name = "ID", Order = 0, IsRequired = true)]
        public String ID = "";

        [DataMember(Name = "Description", Order = 1, IsRequired = true)]
        public string Description = "";
    }

    [DataContract(Namespace = "", Name = "MobileExInfo")]
    public class MobileExInfo
    {
        public MobileExInfo(String mtsn, String description)
        {
            ID = mtsn;
            Description = description;
        }

        public MobileExInfo() { } // Required by the WS

        [DataMember(Name = "ID", Order = 0, IsRequired = true)]
        public String ID = "";

        [DataMember(Name = "Description", Order = 1, IsRequired = false, EmitDefaultValue = false)]
        public string Description = null;

        [DataMember(Name = "LastRegistrationUTC", Order = 2, IsRequired = false, EmitDefaultValue = false)]
        public string LastRegistrationUTC = null;

        [DataMember(Name = "RegionName", Order = 3, IsRequired = false, EmitDefaultValue = false)]
        public string RegionName = null;
    }

    [DataContract(Namespace = "", Name = "BroadcastInfo")]
    public class BroadcastInfo
    {
        public BroadcastInfo(String id, String description)
        {
            ID = id;
            Description = description;
        }

        public BroadcastInfo() { } // Required by the WS

        [DataMember(Name = "ID", Order = 0, IsRequired = true)]
        public String ID = "";

        [DataMember(Name = "Description", Order = 1, IsRequired = true)]
        public string Description = "";
    }

    [DataContract(Namespace = "", Name = "GetMobileInfosResult")]
    public class GetMobileInfosResult
    {
        [DataMember(Name = "ErrorID", Order = 0, IsRequired = true)]
        public int ErrorID = 0;

        [DataMember(Name = "Mobiles", Order = 1, IsRequired = false, EmitDefaultValue = false)]
        public MobileInfo[] Mobiles = null;
    }

    [DataContract(Namespace = "", Name = "GetMobilesPagedResult")]
    public class GetMobilesPagedResult
    {
        [DataMember(Name = "ErrorID", Order = 0, IsRequired = true)]
        public int ErrorID = 0;

        [DataMember(Name = "Mobiles", Order = 1, IsRequired = false, EmitDefaultValue = false)]
        public MobileExInfo[] Mobiles = null;
    }

    [DataContract(Namespace = "", Name = "GetBroadcastIDsResult")]
    public class GetBroadcastIDsResult
    {
        [DataMember(Name = "ErrorID", Order = 0, IsRequired = true)]
        public int ErrorID = 0;

        [DataMember(Name = "BroadcastIDs", Order = 1, IsRequired = false, EmitDefaultValue = false)]
        public String[] BroadcastIDs = null;
    }

    [DataContract(Namespace = "", Name = "GetBroadcastInfosResult")]
    public class GetBroadcastInfosResult
    {
        [DataMember(Name = "ErrorID", Order = 0, IsRequired = true)]
        public int ErrorID = 0;

        [DataMember(Name = "BroadcastInfos", Order = 1, IsRequired = false, EmitDefaultValue = false)]
        public BroadcastInfo[] BroadcastInfos = null;
    }

    [DataContract(Namespace = "", Name = "SubaccountInfo")]
    public class SubaccountInfo
    {
        public SubaccountInfo(String id, String name)
        {
            AccountID = id;
            Name = name;
        }

        public SubaccountInfo() { } // Required by the WS

        [DataMember(Name = "AccountID", Order = 0, IsRequired = true)]
        public String AccountID = "";

        [DataMember(Name = "Name", Order = 1, IsRequired = true)]
        public string Name = "";
    }

    [DataContract(Namespace = "", Name = "GetSubaccountInfosResult")]
    public class GetSubaccountInfosResult
    {
        [DataMember(Name = "ErrorID", Order = 0, IsRequired = true)]
        public int ErrorID = 0;

        [DataMember(Name = "Subaccounts", Order = 1, IsRequired = false, EmitDefaultValue = false)]
        public SubaccountInfo[] Subaccounts = null;
    }

    [DataContract(Namespace = "", Name = "ErrorInfo")]
    public class ErrorInfo
    {
        [DataMember(Name = "ID", Order = 0, IsRequired = true)]
        public int ID;

        [DataMember(Name = "Name", Order = 1, IsRequired = true)]
        public string Name;

        [DataMember(Name = "Description", Order = 2, IsRequired = true)]
        public string Description;
    }

    #endregion

}
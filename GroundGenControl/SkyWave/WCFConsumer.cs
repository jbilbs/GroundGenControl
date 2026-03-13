using System.ServiceModel.Web;
using System.ServiceModel;
using System.Net;
using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using System.Runtime.Serialization.Json;

using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Channels;

namespace SkyWave
{

    [ServiceContract(Namespace = "IGWS")]
    public interface IMessageServiceClient_JSON
    {
        [DataContractFormat()]
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "submit_messages.json/",
            BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        SubmitMessagesResult SubmitForwardMessages_J(String accessID, String password, ForwardMessage[] messages);

        [DataContractFormat()]
        [OperationContract]
        [WebGet(UriTemplate = "get_return_messages.json/?access_id={accessID}&password={password}&from_id={fromID}&start_utc={startUTC}&end_utc={endUTC}&mobile_id={mobileID}&include_raw_payload={includeRawPayload}&sub_account_id={subAccountID}&include_type={includeType}&include_subaccounts={includeSubaccounts}",
            ResponseFormat = WebMessageFormat.Json)]
        GetReturnMessagesResult GetReturnMessages_J(String accessID, String password, long fromID, String startUTC, String endUTC, String mobileID, bool includeRawPayload, String subAccountID, bool includeType, bool includeSubaccounts);

        [DataContractFormat()]
        [OperationContract]
        [WebGet(UriTemplate = "get_forward_statuses.json/?access_id={accessID}&password={password}&start_utc={startUTC}&end_utc={endUTC}&fwIDs={fwMessageIDs}&subaccount={subaccountID}",
            ResponseFormat = WebMessageFormat.Json)]
        GetForwardStatusesResult GetForwardStatuses_J(String accessID, String password, String startUTC, String endUTC, String fwMessageIDs, String subaccountID);

        [DataContractFormat()]
        [OperationContract]
        [WebGet(UriTemplate = "get_broadcast_infos.json/?access_id={accessID}&password={password}&subaccount={subaccountID}",
            ResponseFormat = WebMessageFormat.Json)]
        GetBroadcastInfosResult GetBroadcastInfos_J(String accessID, String password, String subaccountID);

        [DataContractFormat()]
        [OperationContract]
        [WebGet(UriTemplate = "get_mobiles_paged.json/?access_id={accessID}&password={password}&subaccount={subaccountID}&since_mobile={sinceMobile}&page_size={pageSize}",
            ResponseFormat = WebMessageFormat.Json)]
        GetMobilesPagedResult GetMobilesPaged_J(String accessID, String password, String subaccountID, String sinceMobile, int pageSize);

        [DataContractFormat()]
        [OperationContract]
        [WebGet(UriTemplate = "info_errors.json/",
            ResponseFormat = WebMessageFormat.Json)]
        ErrorInfo[] GetErrorInfos_J();

        [DataContractFormat()]
        [OperationContract]
        [WebGet(UriTemplate = "info_utc_time.json/",
            ResponseFormat = WebMessageFormat.Json)]
        String InfoUTC_J();

        [DataContractFormat()]
        [OperationContract]
        [WebGet(UriTemplate = "get_subaccount_infos.json/?access_id={accessID}&password={password}",
            ResponseFormat = WebMessageFormat.Json)]
        GetSubaccountInfosResult GetSubaccountInfos_J(String accessID, String password);
    }

    public class MessageService_JSON : ClientBase<IMessageServiceClient_JSON>, IMessageServiceClient_JSON
    {
        public MessageService_JSON()
        {
        }

        public MessageService_JSON(String name)
            : base(name)
        {
        }

        public MessageService_JSON(Binding binding, EndpointAddress epa)
            : base(binding, epa)
        {
            this.Endpoint.Behaviors.Add(new WebHttpBehavior());
        }

        public SubmitMessagesResult SubmitForwardMessages_J(String accessID, String password, ForwardMessage[] messages)
        {
            return this.Channel.SubmitForwardMessages_J(accessID, password, messages);
        }

        public GetReturnMessagesResult GetReturnMessages_J(String accessID, String password, long fromID, String startUTC, String endUTC, String mobileID, bool includeRawPayload, String subAccountID, bool includeType, bool includeSubaccounts)
        {
            return this.Channel.GetReturnMessages_J(accessID, password, fromID, startUTC, endUTC, mobileID, includeRawPayload, subAccountID, includeType, includeSubaccounts);
        }

        public GetForwardStatusesResult GetForwardStatuses_J(String accessID, String password, String startUTC, String endUTC, String fwMessageIDs, String subAccountID)
        {
            return this.Channel.GetForwardStatuses_J(accessID, password, startUTC, endUTC, fwMessageIDs, subAccountID);
        }

        public GetBroadcastInfosResult GetBroadcastInfos_J(String accessID, String password, String subAccountID)
        {
            return this.Channel.GetBroadcastInfos_J(accessID, password, subAccountID);
        }

        public ErrorInfo[] GetErrorInfos_J()
        {
            return this.Channel.GetErrorInfos_J();
        }

        public String InfoUTC_J()
        {
            return this.Channel.InfoUTC_J();
        }

        public GetMobilesPagedResult GetMobilesPaged_J(String accessID, String password, String subAccountID, String sinceMobile, int pageSize)
        {
            return this.Channel.GetMobilesPaged_J(accessID, password, subAccountID, sinceMobile, pageSize);
        }

        public GetSubaccountInfosResult GetSubaccountInfos_J(String accessID, String password)
        {
            return this.Channel.GetSubaccountInfos_J(accessID, password);
        }
    }

}
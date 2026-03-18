using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SkyWave
{
    // ── Service interface (unchanged — callers see the same API) ─────────────

    public interface IMessageServiceClient_JSON
    {
        SubmitMessagesResult SubmitForwardMessages_J(String accessID, String password, ForwardMessage[] messages);
        GetReturnMessagesResult GetReturnMessages_J(String accessID, String password, long fromID, String startUTC, String endUTC, String mobileID, bool includeRawPayload, String subAccountID, bool includeType, bool includeSubaccounts);
        GetForwardStatusesResult GetForwardStatuses_J(String accessID, String password, String startUTC, String endUTC, String fwMessageIDs, String subaccountID);
        GetBroadcastInfosResult GetBroadcastInfos_J(String accessID, String password, String subAccountID);
        GetMobilesPagedResult GetMobilesPaged_J(String accessID, String password, String subAccountID, String sinceMobile, int pageSize);
        ErrorInfo[] GetErrorInfos_J();
        String InfoUTC_J();
        GetSubaccountInfosResult GetSubaccountInfos_J(String accessID, String password);
    }

    // ── HttpClient-based implementation ──────────────────────────────────────

    public class MessageService_JSON : IMessageServiceClient_JSON, IDisposable
    {
        private readonly HttpClient _client;
        private readonly string _baseUrl;

        public MessageService_JSON()
        {
            _client = new HttpClient();
            _baseUrl = string.Empty;
        }

        public MessageService_JSON(string baseUrl)
        {
            _client = new HttpClient();
            _baseUrl = baseUrl.TrimEnd('/');
        }

        public MessageService_JSON(string baseUrl, HttpClientHandler handler)
        {
            _client = new HttpClient(handler);
            _baseUrl = baseUrl.TrimEnd('/');
        }

        // ── Helper: GET and deserialize ───────────────────────────────────────

        private T Get<T>(string relativeUrl)
        {
            string url = _baseUrl + "/" + relativeUrl.TrimStart('/');
            HttpResponseMessage response = _client.GetAsync(url).GetAwaiter().GetResult();
            response.EnsureSuccessStatusCode();
            string json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            // Try to unwrap outer envelope (e.g. {"GetReturnMessages_JResult":{...}})
            // If it's a plain value (string, array) just deserialize directly
            try
            {
                var outer = JsonConvert.DeserializeObject<Dictionary<string, JToken>>(json);
                if (outer != null && outer.Count == 1)
                {
                    string inner = outer.Values.First().ToString();
                    return JsonConvert.DeserializeObject<T>(inner);
                }
            }
            catch { }

            return JsonConvert.DeserializeObject<T>(json);
        }

        // ── Helper: POST and deserialize ──────────────────────────────────────

        private TResult Post<TResult>(string relativeUrl, object body)
        {
            string url = _baseUrl + "/" + relativeUrl.TrimStart('/');
            string jsonBody = SerializeJson(body);
            StringContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _client.PostAsync(url, content).GetAwaiter().GetResult();
            response.EnsureSuccessStatusCode();
            string json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            // Unwrap the outer envelope (e.g. {"SubmitForwardMessages_JResult":{...}})
            var outer = JsonConvert.DeserializeObject<Dictionary<string, Newtonsoft.Json.Linq.JToken>>(json);
            string inner = outer.Values.First().ToString();
            return JsonConvert.DeserializeObject<TResult>(inner);
        }

        // ── Helper: JSON serialization ────────────────────────────────────────

        private static T DeserializeJson<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        private static string SerializeJson(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        // ── API methods ───────────────────────────────────────────────────────

        public SubmitMessagesResult SubmitForwardMessages_J(String accessID, String password, ForwardMessage[] messages)
        {
            var body = new
            {
                accessID = accessID,
                password = password,
                messages = messages.Select(m => new
                {
                    DestinationID = m.DestinationID,
                    UserMessageID = m.UserMessageID,
                    RawPayload = m.RawPayload != null ? m.RawPayload.Select(b => (int)b).ToArray() : null
                }).ToArray()
            };
            return Post<SubmitMessagesResult>("submit_messages.json/", body);
        }

        public GetReturnMessagesResult GetReturnMessages_J(String accessID, String password, long fromID,
            String startUTC, String endUTC, String mobileID, bool includeRawPayload,
            String subAccountID, bool includeType, bool includeSubaccounts)
        {
            string url = $"get_return_messages.json/?access_id={accessID}&password={password}" +
                         $"&from_id={fromID}&start_utc={startUTC}&end_utc={endUTC}" +
                         $"&mobile_id={mobileID}&include_raw_payload={includeRawPayload}" +
                         $"&sub_account_id={subAccountID}&include_type={includeType}" +
                         $"&include_subaccounts={includeSubaccounts}";
            return Get<GetReturnMessagesResult>(url);
        }

        public GetForwardStatusesResult GetForwardStatuses_J(String accessID, String password,
            String startUTC, String endUTC, String fwMessageIDs, String subaccountID)
        {
            string url = $"get_forward_statuses.json/?access_id={accessID}&password={password}" +
                         $"&start_utc={startUTC}&end_utc={endUTC}&fwIDs={fwMessageIDs}&subaccount={subaccountID}";
            return Get<GetForwardStatusesResult>(url);
        }

        public GetBroadcastInfosResult GetBroadcastInfos_J(String accessID, String password, String subAccountID)
        {
            string url = $"get_broadcast_infos.json/?access_id={accessID}&password={password}&subaccount={subAccountID}";
            return Get<GetBroadcastInfosResult>(url);
        }

        public GetMobilesPagedResult GetMobilesPaged_J(String accessID, String password,
            String subAccountID, String sinceMobile, int pageSize)
        {
            string url = $"get_mobiles_paged.json/?access_id={accessID}&password={password}" +
                         $"&subaccount={subAccountID}&since_mobile={sinceMobile}&page_size={pageSize}";
            return Get<GetMobilesPagedResult>(url);
        }

        public ErrorInfo[] GetErrorInfos_J()
        {
            return Get<ErrorInfo[]>("info_errors.json/");
        }

        public String InfoUTC_J()
        {
            return Get<String>("info_utc_time.json/");
        }

        public GetSubaccountInfosResult GetSubaccountInfos_J(String accessID, String password)
        {
            string url = $"get_subaccount_infos.json/?access_id={accessID}&password={password}";
            return Get<GetSubaccountInfosResult>(url);
        }

        public void Dispose()
        {
            _client?.Dispose();
        }
    }
}
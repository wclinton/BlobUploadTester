using System;
using System.Configuration;
using System.IO;
using System.Net;
using BlobUploadTester.ConnectorContracts;
using Newtonsoft.Json;

namespace BlobUploadTester
{
    public class BlobService
    {

    //    private static readonly Uri ConnectorServiceUri = new Uri("http://127.255.0.1:82");
        private readonly Guid tenantId;// = new Guid("3813fccf-4946-43e8-ac72-0c00d2df9f6f"); //Sage 300 TenantId
        private readonly Guid sessionId;


        public BlobService(Guid tenantId)
        {
            this.tenantId = tenantId;
            sessionId = OpenSession(tenantId);
        }

        public Uri GetBlobUploadUrl(Guid uploadId)
        {
            
            var url = GetConnectorServiceUri() + "api/messages/requests/startuploadrequest/" + sessionId;

            var message = new ConnectorMessage()
            {
                Id = uploadId,
                BodyType = null,
                UploadSessionInfo = null,
                Type = MessageType.UploadSession,
            };

            var json = JsonConvert.SerializeObject(message);
            var result = PostJson(tenantId, url, json);

            if (result.StatusCode != HttpStatusCode.OK)
                throw new Exception("Bad request");

            var responsString = GetResponseString(result);

            var dmResponse = JsonConvert.DeserializeObject<ConnectorMessage>(responsString);

            return dmResponse.UploadSessionInfo.ContainerUri;
        }

        public void CompleteBlobUpload(Guid uploadId)
        {
            var url = String.Format("{0}api/messages/responses/enduploadrequest/{1}/{2}", GetConnectorServiceUri(), sessionId, uploadId);

            var message = new ConnectorMessage
            {
                BodyType = null,
                UploadSessionInfo = null,
                Type = MessageType.Request,
            };

            var json = JsonConvert.SerializeObject(message);
            var result = PostJson(tenantId,url, json);

            if (result.StatusCode != HttpStatusCode.OK)
                throw new Exception("Bad request");
        }

        public void CompleteAllUploads()
        {
            var url = String.Format("{0}api/messages/responses/{1}", GetConnectorServiceUri(),sessionId);

            var body = new DomainMediationResponse
            {
                DomainMediationEntry = new DomainMediation
                {
                    DomainFeatureRequest = "SyncCustomers",
                    
                }
            };

            var message = new ConnectorMessage
            {
                Body = JsonConvert.SerializeObject(body),
            };


            var json = JsonConvert.SerializeObject(message);
            var result = PostJson(tenantId, url, json);

            if (result.StatusCode != HttpStatusCode.OK)
                throw new Exception("Bad request");
        }

        private static Guid OpenSession(Guid tenantId)
        {
            //Step 1 - POST a SyncCustomer request against your cloud deployment

            var url = GetConnectorServiceUri() + "api/messages/requests";

            var message = new ConnectorMessage
            {
                BodyType = "Sage.Connector.Cloud.Integration.Interfaces.Requests.DomainMediationRequest",
                UploadSessionInfo = null,
                Type = MessageType.Request,
            };

            var messageId = message.Id;

            var json = JsonConvert.SerializeObject(message);
            var result = PostJson(tenantId,url, json);

            if (result.StatusCode != HttpStatusCode.OK)
                throw new Exception("Bad request");

            return messageId;
        }

        private  static HttpWebResponse PostJson(Guid tenantId, string url, string json)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "text/json";
            httpWebRequest.Method = "POST";
            httpWebRequest.Headers["TenantId"] = tenantId.ToString();

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                return httpResponse;
            }
        }

        private static string GetResponseString(HttpWebResponse response)
        {
            if (response == null)
                return "";
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                return result;
            }
        }

        private static Uri GetConnectorServiceUri()
        {
            return new Uri(ConfigurationManager.AppSettings["ConnectorServiceAddress"]);
        }       
    }
}

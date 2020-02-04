using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using AutoTrashCartWebServiceApp.Models;

namespace AutoTrashCartWebServiceApp.CustomHandler
{
    public class RequestResponseHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {

            var requestedMethod = request.Method;
            var userHostAddress = HttpContext.Current != null ? HttpContext.Current.Request.UserHostAddress : "0.0.0.0";
            var useragent = request.Headers.UserAgent.ToString();
            var requestMessage = await request.Content.ReadAsByteArrayAsync();
            var uriAccessed = request.RequestUri.AbsoluteUri;

            var responseHeadersString = new StringBuilder();
            foreach (var header in request.Headers)
            {
                responseHeadersString.Append($"{header.Key}: {String.Join(", ", header.Value)}{Environment.NewLine}");
            }

            var messageLoggingHandler = new MessageLogging();

            var requestLog = new AutoTrashCartApiLog()
            {
                Headers = responseHeadersString.ToString(),
                AbsoluteUri = uriAccessed,
                Host = userHostAddress,
                RequestBody = Encoding.UTF8.GetString(requestMessage),
                UserHostAddress = userHostAddress,
                Useragent = useragent,
                RequestedMethod = requestedMethod.ToString(),
                StatusCode = string.Empty
            };

            messageLoggingHandler.IncomingMessageAsync(requestLog);

            var response = await base.SendAsync(request, cancellationToken);

            byte[] responseMessage;
            if (response.IsSuccessStatusCode)
                responseMessage = response.Content != null ? await response.Content.ReadAsByteArrayAsync() : new byte[10];
            else
                responseMessage = Encoding.UTF8.GetBytes(response.ReasonPhrase);

            var responseLog = new AutoTrashCartApiLog()
            {
                Headers = responseHeadersString.ToString(),
                AbsoluteUri = uriAccessed,
                Host = userHostAddress,
                RequestBody = Encoding.UTF8.GetString(responseMessage),
                UserHostAddress = userHostAddress,
                Useragent = useragent,
                RequestedMethod = requestedMethod.ToString(),
                StatusCode = string.Empty
            };

            messageLoggingHandler.OutgoingMessageAsync(responseLog);
            return response;
        }

    }

    public class MessageLogging
    {
        public void IncomingMessageAsync(AutoTrashCartApiLog apiLog)
        {
            apiLog.RequestType = "Request";
            var sqlErrorLogging = new AutoTrashCartApiLogging();
            sqlErrorLogging.InsertLog(apiLog);
        }

        public void OutgoingMessageAsync(AutoTrashCartApiLog apiLog)
        {
            apiLog.RequestType = "Response";
            var sqlErrorLogging = new AutoTrashCartApiLogging();
            sqlErrorLogging.InsertLog(apiLog);
        }
    }
}
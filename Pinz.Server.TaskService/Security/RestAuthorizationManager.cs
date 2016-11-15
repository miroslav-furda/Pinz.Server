using System;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Com.Pinz.Server.TaskService.Security
{
    public class RestAuthorizationManager : ServiceAuthorizationManager
    {
        protected override bool CheckAccessCore(OperationContext operationContext)
        {
            //Extract the Authorization header, and parse out the credentials converting the Base64 string:  
            var authHeader = WebOperationContext.Current.IncomingRequest.Headers["Authorization"];
            if (!string.IsNullOrEmpty(authHeader))
            {
                var svcCredentials = Encoding.ASCII
                    .GetString(Convert.FromBase64String(authHeader.Substring(6)))
                    .Split(':');
                var user = new
                {
                    Name = svcCredentials[0],
                    Password = svcCredentials[1]
                };
                if ((user.Name == "pinzTrialSub") && (user.Password == "lPzsdGF8D9IiUrrIr4gJ"))
                    return true;
                return false;
            }
            //No authorization header was provided, so challenge the client to provide before proceeding:  
            WebOperationContext.Current.OutgoingResponse.Headers.Add("WWW-Authenticate: Basic realm=\"MyWCFService\"");
            //Throw an exception with the associated HTTP status code equivalent to HTTP status 401  
            throw new WebFaultException(HttpStatusCode.Unauthorized);
        }
    }
}
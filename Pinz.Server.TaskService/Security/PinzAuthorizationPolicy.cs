using System;
using System.Collections.Generic;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace Com.Pinz.Server.TaskService.Security
{
    public class PinzAuthorizationPolicy : IAuthorizationPolicy
    {
        private string _id;
        public string Id
        {
            get { return this._id; }
        }


        public ClaimSet Issuer
        {
            get { return ClaimSet.System; }
        }

        public PinzAuthorizationPolicy()
        {
            _id = Guid.NewGuid().ToString();
        }

        // this method gets called after the authentication stage
        public bool Evaluate(EvaluationContext evaluationContext, ref object state)
        {
            // get the authenticated client identity
            IIdentity client = GetClientIdentity(evaluationContext);
            // set the custom principal
            evaluationContext.Properties["Principal"] = new PinzCustomPrincipal(client);
            return true;
        }

        private IIdentity GetClientIdentity(EvaluationContext evaluationContext)
        {
            object obj;
            if (!evaluationContext.Properties.TryGetValue("Identities", out obj))
                throw new Exception("No Identity found");
            IList<IIdentity> identities = obj as IList<IIdentity>;
            if (identities == null || identities.Count <= 0)
                throw new Exception("No Identity found");
            return identities[0];
        }
    }
}
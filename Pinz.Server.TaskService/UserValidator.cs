using System;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.ServiceModel;

namespace Com.Pinz.Server.TaskService
{
    public class UserValidator : UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                throw new SecurityTokenException("Username and password required");

            if (!"test".Equals(userName) || !"test".Equals(password))
                throw new FaultException(string.Format("Wrong username ({0}) or password ", userName));
        }
    }
}
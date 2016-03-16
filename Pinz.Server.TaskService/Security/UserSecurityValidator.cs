using Com.Pinz.Server.DataAccess.Db;
using System.Linq;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.ServiceModel;
using Com.Pinz.Server.DataAccess.Model;

namespace Com.Pinz.Server.TaskService.Security
{
    public class UserSecurityValidator : UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                throw new SecurityTokenException("Username and password required");


            PinzDbContext dbContext = new PinzDbContext();
            UserDO user = dbContext.Users.Where(u => u.EMail == userName && u.Password == password).SingleOrDefault();

            if (user == null )
                throw new FaultException(string.Format("Wrong username ({0}) or password ", userName));
        }
    }
}
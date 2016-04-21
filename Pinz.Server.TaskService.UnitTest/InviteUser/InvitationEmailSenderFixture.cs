using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Com.Pinz.Server.DataAccess.Model;

namespace Com.Pinz.Server.TaskService.InviteUser
{
    [TestClass]
    public class InvitationEmailSenderFixture
    {
        [TestMethod]
        public void TestMethod1()
        {

            UserDO invitingUser = new UserDO()
            {
                EMail = "test@test.com"
            };
            ProjectDO project = new ProjectDO()
            {
                Name = "testProject"
            };

            InvitationEmailSender.Send("miroslav.furda@senacor.com", invitingUser, project, "generatedPassword");
        }
    }
}

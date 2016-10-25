using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web.Hosting;
using Com.Pinz.Server.DataAccess.Model;

namespace Com.Pinz.Server.TaskService.InviteUser
{
    public class InvitationEmailSender
    {
        private static readonly string ProjectInviteEmailTemplate =
            File.ReadAllText(HostingEnvironment.MapPath("~/InviteUser/invitationEmail.html"));

        private static readonly string NewCustomerEmailTemplate =
            File.ReadAllText(HostingEnvironment.MapPath("~/InviteUser/newCustomerEmail.html"));

        public static void SendNewCustomerInvitation(string emailAdress, string generatedPassword)
        {
            var smtpClient = CreateSmtpClient();


            var mail = new MailMessage {From = new MailAddress("invitation@pinzonline.com", "Pinz online")};

            //Setting From , To and CC
            mail.To.Add(new MailAddress(emailAdress));

            //Content
            mail.Subject = "Your subscription for PinzOnline is ready!";
            mail.Body = string.Format(ProjectInviteEmailTemplate, emailAdress, generatedPassword);
            mail.IsBodyHtml = true;

            smtpClient.Send(mail);
        }

        public static void SendProjectInvitation(string emailAdress, UserDO invitingUser, ProjectDO project,
            string generatedPassword)
        {
            var smtpClient = CreateSmtpClient();


            var mail = new MailMessage {From = new MailAddress("invitation@pinzonline.com", "Pinz online")};

            //Setting From , To and CC
            mail.To.Add(new MailAddress(emailAdress));

            //Content
            var invitationFrom = invitingUser.EMail;
            if (!string.IsNullOrEmpty(invitingUser.FirstName) || !string.IsNullOrEmpty(invitingUser.FamilyName))
                invitationFrom = string.Format("{0} {1}", invitingUser.FirstName, invitingUser.FamilyName);
            mail.Subject = "You have been invited to join PinzOnline";
            mail.Body = string.Format(ProjectInviteEmailTemplate, invitationFrom, project.Name, emailAdress,
                generatedPassword);
            mail.IsBodyHtml = true;

            smtpClient.Send(mail);
        }

        private static SmtpClient CreateSmtpClient()
        {
            var smtpClient = new SmtpClient("mail.pinzonline.com", 25);

            smtpClient.Credentials = new NetworkCredential("invitation@pinzonline.com", "r3OFW^B0M^m1");
            //smtpClient.UseDefaultCredentials = true;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = false;
            return smtpClient;
        }
    }
}
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

        private static readonly string NewTrialEmailTemplate =
            File.ReadAllText(HostingEnvironment.MapPath("~/InviteUser/newTrialEmail.html"));

        private static readonly string GoodbyeEmailTemplate =
            File.ReadAllText(HostingEnvironment.MapPath("~/InviteUser/goodbyeEmail.html"));

        private static readonly string TrialToSubsciptionEmailTemplate =
            File.ReadAllText(HostingEnvironment.MapPath("~/InviteUser/trialToSubscriptionEmail.html"));


        public static void SendNewCustomerDuplicateInvitation(string originalEmail, string newUserEMail, string generatedPassword)
        {
            var smtpClient = CreateSmtpClient();
            var mail = new MailMessage { From = new MailAddress("subscribe@pinzonline.com", "PINZonline") };

            //Setting From , To and CC
            mail.To.Add(new MailAddress(originalEmail));

            //Content
            mail.Subject = "Your subscription for PinzOnline is ready!";
            mail.Body = string.Format(NewCustomerEmailTemplate, newUserEMail, generatedPassword);
            mail.IsBodyHtml = true;

            smtpClient.Send(mail);
        }

        public static void SendGoodbye(string emailAdress)
        {
            var smtpClient = CreateSmtpClient();
            var mail = new MailMessage {From = new MailAddress("invitation@pinzonline.com", "PINZonline")};

            //Setting From , To and CC
            mail.To.Add(new MailAddress(emailAdress));

            //Content
            mail.Subject = "This is goodbye!";
            mail.Body = GoodbyeEmailTemplate;
            mail.IsBodyHtml = true;

            smtpClient.Send(mail);
        }

        public static void SendTrialToSubscription(string emailAdress, string generatedPassword)
        {
            var smtpClient = CreateSmtpClient();
            var mail = new MailMessage {From = new MailAddress("subscribe@pinzonline.com", "PINZonline")};

            //Setting From , To and CC
            mail.To.Add(new MailAddress(emailAdress));

            //Content
            mail.Subject = "Your trial for PinzOnline has changed to regular subscription!";
            mail.Body = string.Format(TrialToSubsciptionEmailTemplate);
            mail.IsBodyHtml = true;

            smtpClient.Send(mail);
        }

        public static void SendTrialInvitation(string emailAdress, string generatedPassword)
        {
            var smtpClient = CreateSmtpClient();
            var mail = new MailMessage {From = new MailAddress("subscribe@pinzonline.com", "PINZonline")};

            //Setting From , To and CC
            mail.To.Add(new MailAddress(emailAdress));

            //Content
            mail.Subject = "Your trial subscription for PinzOnline is ready!";
            mail.Body = string.Format(NewTrialEmailTemplate, emailAdress, generatedPassword);
            mail.IsBodyHtml = true;

            smtpClient.Send(mail);
        }

        public static void SendNewCustomerInvitation(string emailAdress, string generatedPassword)
        {
            var smtpClient = CreateSmtpClient();
            var mail = new MailMessage {From = new MailAddress("subscribe@pinzonline.com", "PINZonline")};

            //Setting From , To and CC
            mail.To.Add(new MailAddress(emailAdress));

            //Content
            mail.Subject = "Your subscription for PinzOnline is ready!";
            mail.Body = string.Format(NewCustomerEmailTemplate, emailAdress, generatedPassword);
            mail.IsBodyHtml = true;

            smtpClient.Send(mail);
        }

        public static void SendProjectInvitation(string emailAdress, UserDO invitingUser, ProjectDO project,
            string generatedPassword)
        {
            var smtpClient = CreateSmtpClient();
            var mail = new MailMessage {From = new MailAddress("subscribe@pinzonline.com", "PINZonline")};

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

            smtpClient.Credentials = new NetworkCredential("subscribe@pinzonline.com", "IFlp^ai46Eir");
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = false;
            return smtpClient;
        }

    }
}
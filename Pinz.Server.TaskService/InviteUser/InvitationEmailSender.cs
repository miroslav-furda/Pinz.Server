
using Com.Pinz.Server.DataAccess.Model;
using System.IO;
using System.Net.Mail;

namespace Com.Pinz.Server.TaskService.InviteUser
{
    public class InvitationEmailSender
    {
        private static string EmailTemplate = File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath("~/InviteUser/invitationEmail.html"));

        public static void Send(string emailAdress, UserDO invitingUser, ProjectDO project, string generatedPassword)
        {
            SmtpClient smtpClient = new SmtpClient("mail.pinzonline.com", 25);

            smtpClient.Credentials = new System.Net.NetworkCredential("invitation@pinzonline.com", "r3OFW^B0M^m1");
            //smtpClient.UseDefaultCredentials = true;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = false;


            MailMessage mail = new MailMessage();

            //Setting From , To and CC
            mail.From = new MailAddress("invitation@pinzonline.com", "Pinz online");
            mail.To.Add(new MailAddress(emailAdress));
            //Content
            string invitationFrom = invitingUser.EMail;
            if( !string.IsNullOrEmpty(invitingUser.FirstName) || !string.IsNullOrEmpty(invitingUser.FamilyName))
            {
                invitationFrom = string.Format("{0} {1}", invitingUser.FirstName, invitingUser.FamilyName);
            }
            mail.Subject = "You have been invited to join PinzOnline";
            mail.Body = string.Format(EmailTemplate, invitationFrom, project.Name, emailAdress, generatedPassword);
            mail.IsBodyHtml = true;

            smtpClient.Send(mail);
        }
    }
}
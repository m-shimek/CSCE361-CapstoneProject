using MailKit.Net.Smtp;
using MimeKit;

/*
 * Description: This program's purpose is to sign in to a gmail address
 * and use that gmail to send emails to other email addresses in a command line.
 * 
 */

namespace MyVotingSystem.Engines
{
    class EmailSender
    {
        //Simple Mail Transfer Protocol port 465 used for sending emails across the internet.
        //Secure Sockets Layer allows for encrypted connection through client server handshake.
        private const int SmtpPort = 465;
        private const bool IsSslConnection = true;

        public string Send(string emailRecipient, string emailSubject, string emailBody)
        {
                string failedClientConnection = "Client Connection failed";
                string failedAuthentication = "Authentication failed. Message aborted";
                string failedMessage = "Message failed";
                string failedDisconnection = "Disconnection failed";
                string successMessage = "Message sent successfully";

                //MimeMessage does formatting & parsing for making a blank email.
                var mail = new MimeMessage();

                //this is client email (one making message).
                mail.From.Add(new MailboxAddress("Pacopolis Online Ballot", "pacopolisballot@gmail.com"));
                
                //who it is going to, its subject, and the email body
                mail.To.Add(new MailboxAddress("", emailRecipient));
                mail.Subject = emailSubject;
                mail.Body = new TextPart("plain") { Text = emailBody };


                using (var client = new SmtpClient())
                {
                    //connects to google's emailing service.
                    try
                    {
                        client.Connect("smtp.gmail.com", SmtpPort, IsSslConnection);
                    }
                    catch (Exception)
                    {
                        return failedClientConnection;
                    }


                    //Authentication requires a special app password along with the username of the client.
                    try
                    {
                        client.Authenticate("PacopolisBallot", "xfgc psfq lrty jzcs");
                    }
                    catch (Exception)
                    {
                        
                        client.Disconnect(true);
                        return failedAuthentication;
                    }


                    //email the message by the user.
                    try
                    {
                        client.Send(mail);
                    }
                    catch (Exception)
                    {
                        client.Disconnect(true);
                        return failedMessage;
                    }


                    //disconnecting aka signing out of gmail account to close the connection.
                    try
                    {
                        client.Disconnect(false);
                    }
                    catch (Exception)
                    {
                        return failedDisconnection;
                    }
                }

                return successMessage;
            }
        }
    }
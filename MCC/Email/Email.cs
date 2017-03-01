using System;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Web;

namespace MCC.Email
{
    public class Email
    {
        public static bool IsEmail(string inputEmail)
        {
            if (inputEmail.Length > 0)
            {
                Regex re = new Regex(@"^([a-zA-Z0-9_\-\.\#]+)@((\[[0-9]{1,3}" +
                      @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                      @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
                //HttpContext.Current.Trace.Warn("re.IsMatch(inputEmail)", re.IsMatch(inputEmail).ToString());
                return re.IsMatch(inputEmail);
            }
            else
                return false;
        }

        /// <summary>
        /// This function will send Email.
        /// </summary>
        /// <param name="to"></param>
        /// <param name="from"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <returns>True if email was send or False if email wasn't send.</returns>
        public static bool SendEmail(string to, EmailTemplates emailTemplate, object obj)
        {
            var lang = System.Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
            var template = Domain.EmailTemplateRepository.GetTemplate((int)emailTemplate, lang);
            var body = EmailFormatter.Format(template.Template, obj);
            return SendEmail(to, String.Empty, String.Empty, String.Empty, String.Empty, template.Subject, body, String.Empty);
        }

        /// <summary>
        /// This function will send Email.
        /// </summary>
        /// <param name="to"></param>
        /// <param name="from"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <returns>True if email was send or False if email wasn't send.</returns>
        public static bool SendEmail(string to, string from, string subject, string body)
        {
            return SendEmail(to, String.Empty, from, String.Empty, String.Empty, subject, body, String.Empty);
        }

        /// <summary>
        /// This function will send Email.
        /// </summary>
        /// <param name="to"></param>
        /// <param name="from"></param>
        /// <param name="cc"></param>
        /// <param name="bcc"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <returns>True if email was send or False if email wasn't send.</returns>
        public static bool SendEmail(string to, string toName, string from, string cc, string bcc, string subject, string body)
        {
            return SendEmail(to, toName, from, cc, bcc, subject, body, String.Empty);
        }

        /// <summary>
        /// This function will send Email.
        /// </summary>
        /// <param name="to"></param>
        /// <param name="toName"></param>
        /// <param name="from"></param>
        /// <param name="cc"></param>
        /// <param name="bcc"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="attachment"></param>
        /// <returns></returns>
        public static bool SendEmail(string to, string toName, string from, string cc, string bcc, string subject, string body, string attachment)
        {
            return SendEmail(to, toName, from, cc, bcc, subject, body, attachment, true);
        }

        /// <summary>
        /// This function will send Email.
        /// </summary>
        /// <param name="to"></param>
        /// <param name="from"></param>
        /// <param name="cc"></param>
        /// <param name="bcc"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="attachment"></param>
        /// <returns>True if email was send or False if email wasn't send.</returns>
        public static bool SendEmail(string to, string toName, string from, string cc, string bcc, string subject, string body, string attachment, bool isHtml)
        {
            return SendEmail(to, toName, from, String.Empty, cc, bcc, subject, body, attachment, true);
        }

        /// <summary>
        /// This function will send Email.
        /// </summary>
        /// <param name="to"></param>
        /// <param name="from"></param>
        /// <param name="fromName"></param>
        /// <param name="cc"></param>
        /// <param name="bcc"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="attachment"></param>
        /// <returns>True if email was send or False if email wasn't send.</returns>
        public static bool SendEmail(string to, string toName, string from, string fromName, string cc, string bcc, string subject, string body, string attachment, bool isHtml)
        {
            string emailServer = System.Configuration.ConfigurationManager.AppSettings["EmailServer"];
            string emailFromAddress =  System.Configuration.ConfigurationManager.AppSettings["EmailFromAddress"];

            bool results = false;
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = emailServer;

            MailMessage mail = new MailMessage();
            char[] separator = new char[] { ';', ',' };
            if (!String.IsNullOrEmpty(to))
            {
                string[] toList = to.Split(separator);
                foreach (string toItem in toList)
                {
                    if (IsEmail(toItem.Trim()))
                        mail.To.Add(new MailAddress(toItem.Trim(), String.IsNullOrEmpty(toName) == false ? toName : String.Empty));
                }
            }
            if (String.IsNullOrEmpty(from) || !IsEmail(from)) from = emailFromAddress;

            if (!String.IsNullOrEmpty(fromName))
                mail.From = new MailAddress(from, fromName);
            else
                mail.From = new MailAddress(from);

            if (!String.IsNullOrEmpty(cc))
            {
                string[] carbonCopy = cc.Split(separator);
                foreach (string copyCc in carbonCopy)
                {
                    string ccEmail = copyCc.Trim();
                    if (!String.IsNullOrEmpty(ccEmail) && IsEmail(ccEmail))
                        mail.CC.Add(new MailAddress(ccEmail));
                }
            }
            if (!String.IsNullOrEmpty(bcc))
            {
                string[] blindCarbonCopy = bcc.Split(separator);
                foreach (string copyBcc in blindCarbonCopy)
                {
                    string bcEmail = copyBcc.Trim();
                    if (!String.IsNullOrEmpty(bcEmail) && IsEmail(bcEmail))
                        mail.Bcc.Add(new MailAddress(bcEmail));
                }
            }

            if (!String.IsNullOrEmpty(attachment))
                mail.Attachments.Add(new Attachment(HttpContext.Current.Server.MapPath(attachment)));

            mail.Subject = subject;
            mail.IsBodyHtml = isHtml;// true;
            mail.Body = body;
            mail.Priority = MailPriority.Normal;

            try
            {
                smtpClient.Send(mail);
                results = true;
            }
            catch (Exception ex)
            {
                HttpContext.Current.Trace.Warn("FormManager.SendEmail", ex.Message);
            }

            return results;
        }

    }
}
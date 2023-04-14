using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HC.Core
{
    /// <summary>
    /// Summary description for FlexiMail
    /// </summary>
    public class FlexiMail
    {
        #region Constructors-Destructors
        public FlexiMail()
        {
            //set defaults 
            myEmail = new MailMessage();
            _MailBodyManualSupply = false;
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;
        }
        //public void Dispose()
        //{
        //    base.Finalize();
        //    myEmail.Dispose();
        //}
        #endregion

        #region  Class Data
        private string _From;
        private string _FromName;
        private string _To;
        private string _ToList;
        private string _Subject;
        private string _CC;
        private string _CCList;
        private string _BCC;
        private string _TemplateDoc;
        private string[] _ArrValues;
        private string _BCCList;
        private bool _MailBodyManualSupply;
        private string _MailBody;
        //private string _Attachment;
        private string[] _Attachment;
        private System.Net.Mail.MailMessage myEmail;

        #endregion

        #region Propertie
        public string From
        {
            set { _From = value; }
        }
        public string FromName
        {
            set { _FromName = value; }
        }
        public string To
        {
            set { _To = value; }
        }
        public string Subject
        {
            set { _Subject = value; }
        }
        public string CC
        {
            set { _CC = value; }
        }
        public string BCC
        {

            set { _BCC = value; }
        }
        public bool MailBodyManualSupply
        {

            set { _MailBodyManualSupply = value; }
        }
        public string MailBody
        {
            set { _MailBody = value; }
        }
        public string EmailTemplateFileName
        {
            //FILE NAME OF TEMPLATE ( MUST RESIDE IN ../EMAILTEMPLAES/ FOLDER ) 
            set { _TemplateDoc = value; }
        }
        public string[] ValueArray
        {
            //ARRAY OF VALUES TO REPLACE VARS IN TEMPLATE 
            set { _ArrValues = value; }
        }

        public string[] AttachFile
        {
            set { _Attachment = value; }
        }

        #endregion

        #region SEND EMAIL

        public void Send()
        {
            myEmail.IsBodyHtml = true;
            //set mandatory properties 
            if (_FromName == "")
                _FromName = _From;
            myEmail.From = new MailAddress(_From, _FromName);
            myEmail.Subject = _Subject;

            //---Set recipients in To List 
            _ToList = _To.Replace(";", ",");
            if (_ToList != "")
            {
                string[] arr = _ToList.Split(',');
                myEmail.To.Clear();
                if (arr.Length > 0)
                {
                    foreach (string address in arr)
                    {
                        myEmail.To.Add(new MailAddress(address));
                    }
                }
                else
                {
                    myEmail.To.Add(new MailAddress(_ToList));
                }
            }

            //---Set recipients in CC List 
            if (_CC != null)
            {
                _CCList = _CC.Replace(";", ",");

                if (_CCList != "")
                {
                    string[] arr = _CCList.Split(',');
                    myEmail.CC.Clear();
                    if (arr.Length > 0)
                    {
                        foreach (string address in arr)
                        {
                            myEmail.CC.Add(new MailAddress(address));
                        }
                    }
                    else
                    {
                        myEmail.CC.Add(new MailAddress(_CCList));
                    }
                }
            }
            //---Set recipients in BCC List 
            if (_BCC != null)
            {
                _BCCList = _BCC.Replace(";", ",");
                if (_BCCList != "")
                {
                    string[] arr = _BCCList.Split(',');
                    myEmail.Bcc.Clear();
                    if (arr.Length > 0)
                    {
                        foreach (string address in arr)
                        {
                            myEmail.Bcc.Add(new MailAddress(address));
                        }
                    }
                    else
                    {
                        myEmail.Bcc.Add(new MailAddress(_BCCList));
                    }
                }
            }

            //set mail body 
            if (_MailBodyManualSupply)
            {
                myEmail.Body = _MailBody;
            }
            else
            {
                //myEmail.Body = GetHtml(_TemplateDoc);
                //& GetHtml("EML_Footer.htm") 
            }

            // set attachment 
            if (_Attachment != null)
            {
                for (int i = 0; i < _Attachment.Length; i++)
                {
                    if (_Attachment[i] != null)
                        myEmail.Attachments.Add(new Attachment(_Attachment[i]));
                }
            }




            //using (var client1 = new SmtpClient())
            //{
            //    client1.EnableSsl = true;
            //    client1.Host = "smtp.office365.com";
            //    client1.Port = 587;
            //    client1.DeliveryMethod = SmtpDeliveryMethod.Network;
            //    client1.Credentials = new NetworkCredential("jakharravi124@gmail.com", "is12wer34");
            //    try
            //    {
            //        client1.SendMailAsync(myEmail);
            //    }
            //    catch
            //    {

            //    }

            //}

            SmtpClient client = new SmtpClient();
            client.Host = "mail.24livehost.com";
            client.EnableSsl = true;
            if (client.Host != "localhost")
            {
                client.Host = "mail.24livehost.com";
                client.Credentials = new NetworkCredential("wds12@24livehost.com", "wds#2020WS");
            }
            client.Port = 587;
            try
            {
                client.SendMailAsync(myEmail);
            }
            catch (Exception ex)
            {
                string mm = ex.Message;
            }
        }

        #endregion

        #region GetHtml

        //public string GetHtml(string argTemplateDocument)
        //{
        //    int i;
        //    StreamReader filePtr;
        //    string fileData = "";

        //    if (!string.IsNullOrEmpty(Convert.ToString(ConfigurationManager.AppSettings["EmailTemplate"])))
        //    {
        //        filePtr = File.OpenText(Convert.ToString(ConfigurationManager.AppSettings["EmailTemplate"]) + argTemplateDocument);
        //    }
        //    else
        //    {
        //        filePtr = File.OpenText(HttpContext.Current.Server.MapPath("~/EmailTemplate/") + argTemplateDocument);
        //    }
        //    fileData = filePtr.ReadToEnd();
        //    filePtr.Close();
        //    filePtr = null;
        //    if ((_ArrValues == null))
        //    {
        //        fileData = fileData.Replace("@copyrightyear@", DateTime.Now.Year.ToString());
        //        return fileData;
        //    }
        //    else
        //    {
        //        for (i = _ArrValues.GetLowerBound(0); i <= _ArrValues.GetUpperBound(0); i++)
        //        {
        //            fileData = fileData.Replace("@v" + i.ToString() + "@", (string)_ArrValues[i]);
        //        }
        //        fileData = fileData.Replace("@copyrightyear@", DateTime.Now.Year.ToString());
        //        return fileData;
        //    }
        //}

        #endregion
    }
}

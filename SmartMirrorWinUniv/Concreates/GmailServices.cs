namespace SmartMirrorWinUniv.Concreates
{
    using System;
    using System.Threading.Tasks;
    using System.Net;
    using System.Threading;

    public class GmailServices
    {
        #region Fields

        private Timer timer;

        private int elapseTime = 6000; //every 10 minutes

        private string accessToken = string.Empty;

        private Windows.Web.Http.HttpClient client;

        private string userId = "kahizer241237@gmail.com";

        #endregion

        #region Constructor

        public GmailServices(string token)
        {
            this.accessToken = token;
            this.timer = new Timer(this.GetLatesEmail, null, 0, Timeout.Infinite);
        }

        #endregion

        #region Public Events

        public event EventHandler<EmailStatus> LatestEmailsEvent;

        #endregion

        #region Private Methods

        private void GetLatesEmail(object state)
        {
            var mails = this.GetEmails().Result;
            this.LatestEmailsEvent?.Invoke(this, mails);
            this.timer.Change(this.elapseTime, Timeout.Infinite);
        }

        private async Task<EmailStatus> GetEmails()
        {
            var mailstatus = new EmailStatus();
            using (var httpRequest = new Windows.Web.Http.HttpRequestMessage())
            {
                var client = new Windows.Web.Http.HttpClient();
                string mailListApi = $"https://www.googleapis.com/gmail/v1/users/{this.userId}/messages?q=is%3Aunread";

                httpRequest.Method = Windows.Web.Http.HttpMethod.Get;
                httpRequest.RequestUri = new Uri(mailListApi);
                httpRequest.Headers.Authorization = new Windows.Web.Http.Headers.HttpCredentialsHeaderValue("Bearer", this.accessToken);

                var response = await client.SendRequestAsync(httpRequest);
                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    dynamic jsonObject = Newtonsoft.Json.JsonConvert.DeserializeObject(jsonData);

                    var emailIds = jsonObject.messages;
                    var counter = 0;
                    foreach (dynamic emailId in emailIds)
                    {
                        if(counter > 10)
                            break;

                        counter++;
                        string id = emailId.id;
                        using (var innerHttpRequest = new Windows.Web.Http.HttpRequestMessage())
                        {
                            var innerClient = new Windows.Web.Http.HttpClient();
                            string mailApi = $"https://www.googleapis.com/gmail/v1/users/{this.userId}/messages/{id}";

                            innerHttpRequest.Method = Windows.Web.Http.HttpMethod.Get;
                            innerHttpRequest.RequestUri = new Uri(mailApi);
                            innerHttpRequest.Headers.Authorization =
                                new Windows.Web.Http.Headers.HttpCredentialsHeaderValue("Bearer", this.accessToken);

                            var innerResponse = await innerClient.SendRequestAsync(innerHttpRequest);
                            if (innerResponse.IsSuccessStatusCode)
                            {
                                dynamic mailObject = Newtonsoft.Json.JsonConvert.DeserializeObject(innerResponse.Content.ToString());
                                dynamic payload = mailObject.payload;
                                dynamic headers = payload.headers;

                                var mailMessage = new EmailMessage();
                                mailMessage.Snippet = WebUtility.HtmlDecode(mailObject.snippet.Value.ToString());

                                foreach (dynamic header in headers)
                                {
                                    if (header.name == "From")
                                    {
                                        var rawFrom = header.value.ToString();
                                        mailMessage.From = rawFrom.Replace("\"", "");
                                    }

                                    if (header.name == "Subject")
                                    {
                                        mailMessage.Subject = header.value;
                                    }

                                    if (header.name == "Date")
                                    {
                                        try
                                        {
                                            mailMessage.TimeStamp = DateTime.Parse(header.value.ToString());
                                        }
                                        catch (Exception){ }
                                    }
                                }

                                mailstatus.EmailMessages.Add(mailMessage);
                            }
                        }
                    }
                }

                return mailstatus;
            }
        }

        #endregion

    }
}

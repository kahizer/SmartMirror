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

        private int elapseTime = 60000; //every 1 minutes

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

        public event EventHandler FailedToAuthenticate;

        #endregion

        #region Private Methods

        private void GetLatesEmail(object state)
        {
            try
            {
                var mails = this.GetEmails().Result;
                this.LatestEmailsEvent?.Invoke(this, mails);
                this.timer.Change(this.elapseTime, Timeout.Infinite);
            }
            catch (Exception ex)
            {
                this.timer.Dispose();
                this.timer = null;
                this.FailedToAuthenticate?.Invoke(this, null);
            }
        }

        private async Task<EmailStatus> GetEmails()
        {
            try
            {
                var mailstatus = new EmailStatus();
                using (var httpRequest = new Windows.Web.Http.HttpRequestMessage())
                {
                    var client = new Windows.Web.Http.HttpClient();
                    //string mailListApi = $"https://www.googleapis.com/gmail/v1/users/{this.userId}/messages?q=is%3Aunread";
                    string mailListApi = $"https://www.googleapis.com/gmail/v1/users/me/messages?q=in%3Ainbox%20is%3Aunread%20-category%3A%7BCATEGORY_PERSONAL%7D";

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
                                //string mailApi = $"https://www.googleapis.com/gmail/v1/users/{this.userId}/messages/{id}";
                                string mailApi = $"https://www.googleapis.com/gmail/v1/users/me/messages/{id}";

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
                                            mailMessage.From = this.CleanFromField(rawFrom);
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
                    else
                    {
                        var errorRaw = await response.Content.ReadAsStringAsync();
                        dynamic jsonObject = Newtonsoft.Json.JsonConvert.DeserializeObject(errorRaw);

                        dynamic error = jsonObject.error;
                        dynamic errors = error.errors;
                        dynamic firstItem = errors[0];
                        dynamic reasonObject = firstItem.reason;
                        string reason = reasonObject.Value;

                        if (reason == "authError")
                        {
                            throw new Exception();
                        }
                    }

                    return mailstatus;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                this.FailedToAuthenticate?.Invoke(this, null);
                throw;
            }
        }

        private string CleanFromField(string rawSender)
        {
            rawSender = rawSender.Replace("\"", "");
            int startingIndex = rawSender.IndexOf('<');

            if (startingIndex > 0)
            {
                return rawSender.Substring(0, startingIndex - 1);
            }
            
            return rawSender;
        }

        #endregion

    }
}

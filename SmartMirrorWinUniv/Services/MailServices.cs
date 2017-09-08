
namespace SmartMirrorWinUniv.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;

    using Google.Apis.Auth.OAuth2;
    using Google.Apis.Gmail.v1;
    using Google.Apis.Gmail.v1.Data;
    using Google.Apis.Services;
    using Google.Apis.Util;

    using SmartMirrorWinUniv.Concreates;

    public class MailServices
    {
        #region Fields

        private UserCredential _credential;

        const string AppName = "SmartMirror";

        private Timer timer;

        private int elapseTime = 6000;

        #endregion

        #region Constructor

        public MailServices()
        {
            var worker = Task.Factory.StartNew(
               () =>
                {
                    var credential = this.GetCredential().Result;
                });
            worker.Wait();
        }

        #endregion

        #region Public Events

        public event EventHandler<EmailStatus> LatestEmailsEvent;

        #endregion

        #region Private Methods

        private async Task<UserCredential> GetCredential()
        {
            var scopes = new[] { GmailService.Scope.GmailModify, GmailService.Scope.GmailReadonly, GmailService.Scope.GmailCompose, GmailService.Scope.MailGoogleCom };
            var uri = new Uri("ms-appx:///client_id.json");

            _credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(uri, scopes, "kahizer241237@hotmail.com", CancellationToken.None);

            this.timer = new Timer(this.GetEmails, null, 0, Timeout.Infinite);
            //this.GetEmails();
            return _credential;

            
        }

        private async void GetEmails(object state)
        {
            var service = new GmailService(new BaseClientService.Initializer()
                                               {
                                                   HttpClientInitializer = _credential,
                                                   ApplicationName = AppName,
                                               });
            var sizeEstimate = 0L;
            IList<Message> messages = null;
            //var emailMessages = new List<EmailMessage>();

            var mailstatus = new EmailStatus();


            await Task.Run(async () =>
                {
                    UsersResource.MessagesResource.ListRequest request =
                        service.Users.Messages.List("me");
                    request.Q = "is:unread ";//"larger:5M";
                    request.MaxResults = 10;
                    messages = request.Execute().Messages;

                    for (int index = 0; index < messages.Count; index++)
                    {
                        var message = messages[index];
                        var getRequest = service.Users.Messages.Get("me", message.Id);
                        getRequest.Format =
                            UsersResource.MessagesResource.GetRequest.FormatEnum.Metadata;
                        getRequest.MetadataHeaders = new Repeatable<string>(
                            new[] { "Subject", "Date", "From" });
                        messages[index] = getRequest.Execute();
                        sizeEstimate += messages[index].SizeEstimate ?? 0;

                        var mailMessage = new EmailMessage();
                        mailMessage.From = messages[index].Payload.Headers.FirstOrDefault(h => h.Name == "From").Value;
                        mailMessage.Subject = messages[index].Payload.Headers.FirstOrDefault(h => h.Name == "Subject").Value;
                        mailMessage.Snippet = WebUtility.HtmlDecode(messages[index].Snippet);
                        try
                        {
                            mailMessage.TimeStamp = DateTime.Parse(messages[index].Payload.Headers.FirstOrDefault(h => h.Name == "Date").Value);
                        }
                        catch (Exception ex)
                        {
                            
                        }
                        
                        mailstatus.EmailMessages.Add(mailMessage);

                    }
                });

            this.OnLatestEmailsEvent(mailstatus);
            this.timer.Change(this.elapseTime, Timeout.Infinite);
        }
        #endregion

        #region
        #endregion

        protected virtual void OnLatestEmailsEvent(EmailStatus e)
        {
            this.LatestEmailsEvent?.Invoke(this, e);
        }
    }
}

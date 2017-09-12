using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMirrorWinUniv.Services
{
    using System.Threading;

    using Google.Apis.Auth.OAuth2;
    //using Google.Apis.Calendar.v3;
    using Google.Apis.Gmail.v1;
    using Google.Apis.Services;

    public class CalendarApiService
    {
        #region Fields

        private UserCredential _credential;

        const string AppName = "SmartMirror";

        private Timer timer;

        private int elapseTime = 6000;

        #endregion

        #region Constructor

        public CalendarApiService()
        {
            var worker = Task.Factory.StartNew(
                () =>
                    {
                        var credential = this.GetCredential().Result;
                    });
            worker.Wait();
        }

        #endregion

        #region Private Methods

        private async Task<UserCredential> GetCredential()
        {
            try
            {
                //if (ServiceManager.GoogleCredential == null)
                //{
                //    var scopes = new[] { GmailService.Scope.GmailModify, GmailService.Scope.GmailReadonly, GmailService.Scope.GmailCompose, GmailService.Scope.MailGoogleCom, CalendarService.Scope.CalendarReadonly, CalendarService.Scope.Calendar,
                //    CalendarService.Scope.CalendarReadonly, CalendarService.Scope.Calendar };
                //var uri = new Uri("ms-appx:///client_id.json");

                //ServiceManager.GoogleCredential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                //                       uri,
                //                       scopes,
                //                       "jose",
                //                       CancellationToken.None);
                //}

                

                //var service = new CalendarService(new BaseClientService.Initializer()
                //                                  {
                //                                      HttpClientInitializer = ServiceManager.GoogleCredential,
                //                                      ApplicationName = AppName,
                //                                  });

                //var request = service.Calendars.Get("kahizer241237@gmail.com").Execute();
                //var result = request.Summary;


                return this._credential;
            }
            catch (Exception ex)
            {
                var msgg = ex.Message;
            }

            return null;


            //var scopes = new[] { GmailService.Scope.GmailModify, GmailService.Scope.GmailReadonly, GmailService.Scope.GmailCompose, GmailService.Scope.MailGoogleCom };
            //var uri = new Uri("ms-appx:///client_id.json");

            //_credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(uri, scopes, "kahizer241237@hotmail.com", CancellationToken.None);

            //this.timer = new Timer(this.GetEmails, null, 0, Timeout.Infinite);
            //return _credential;
        }

        #endregion

    }
}

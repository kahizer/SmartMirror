using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Security.Authentication.Web;
using Windows.Foundation;
using SmartMirrorWinUniv.Concreates;

namespace SmartMirrorWinUniv.Services
{
    public class CalendarServices
    {
        #region Fields

        static string[] Scopes = { CalendarService.Scope.CalendarReadonly };
        static string ApplicationName = "Google Calendar API .NET Quickstart";

        private string clientId = "425431929822-l1l3lt2e2ao9jttnq5gkl77td6gvvmuq.apps.googleusercontent.com";

        private string clientSecret = "zYbEBUcjnRJfH0a88FIcaSlW";

        private string userName = "kahizer241237@gmail.com";

        private string serviceAccountEmail = "kahizer241237@gmail.com";

        private string googleAuthenticationCode = string.Empty;

        private string redirectURI = "http://localhost";//"pw.oauth2:/oauth2redirect";

        private string scope = "https://www.googleapis.com/auth/calendar.readonly";

        private Timer timer;

        private int elapseTime = 60000; //every 10 minutes

        #endregion

        #region Constructor

        public CalendarServices()
        {
            this.timer = new Timer(this.UpdateCalendar, null, 0, Timeout.Infinite);
            //this.GetEvents();
            //var newre = result;
        }


        #endregion

        #region Public Properties

        public CalendarStatus GetCalendaeStatus { get; set; }

        #endregion

        #region Public Events

        public event EventHandler<CalendarStatus> LatestCalendarEvent;

        #endregion

        #region Private Methods

        private void UpdateCalendar(object state)
        {
            var calendar = this.GetCalendar().Result;
            this.LatestCalendarEvent?.Invoke(this, calendar);
            this.timer.Change(this.elapseTime, Timeout.Infinite);
        }

        private async Task<CalendarStatus> GetCalendar()
        {
            CalendarStatus calendar = null;
            //var getCalendarTask = new TaskFactory().StartNew(() =>
            //{
                var result = this.GetEvents();
                //calendar = new CalendarStatus(result);
            //});

            //getCalendarTask.Wait();

            return calendar;
        }

        private string GetEvents()
        {
            var clientId = this.clientId;
            var redirectURI = "http://localhost";//"pw.oauth2:/oauth2redirect";
            var scope = "https://www.googleapis.com/auth/calendar.readonly";
            var SpotifyUrl = $"https://accounts.google.com/o/oauth2/auth?client_id={clientId}&redirect_uri={Uri.EscapeDataString(redirectURI)}&response_type=code&scope={Uri.EscapeDataString(scope)}";
            var StartUri = new Uri(SpotifyUrl);
            var EndUri = new Uri(redirectURI);

            string result = string.Empty;

            var authenticateWorker = new TaskFactory().StartNew(async() => {
                WebAuthenticationResult WebAuthenticationResult = await WebAuthenticationBroker.AuthenticateAsync(WebAuthenticationOptions.None, StartUri, EndUri);
                if (WebAuthenticationResult.ResponseStatus == WebAuthenticationStatus.Success)
                {
                    var decoder = new WwwFormUrlDecoder(new Uri(WebAuthenticationResult.ResponseData).Query);
                    if (decoder[0].Name != "code")
                    {
                        System.Diagnostics.Debug.WriteLine($"OAuth authorization error: {decoder.GetFirstValueByName("error")}.");
                        //return string.Empty;
                    }

                    var autorizationCode = decoder.GetFirstValueByName("code");
                    this.googleAuthenticationCode = decoder.GetFirstValueByName("code");

                    var data = this.GetRawEvents().Result;
                    result = data;
                    //return data;

                }

                var msg = "testing msg";
            });


            authenticateWorker.Wait();
            // Get Authorization code


            return result;
        }



        private async Task<string> GetRawEvents()
        {
            var SpotifyUrl = $"https://accounts.google.com/o/oauth2/auth?client_id={this.clientId}&redirect_uri={Uri.EscapeDataString(this.redirectURI)}&response_type=code&scope={Uri.EscapeDataString(scope)}";
            var StartUri = new Uri(SpotifyUrl);
            var EndUri = new Uri(redirectURI);

            //Get Access Token
            var pairs = new Dictionary<string, string>();
            pairs.Add("code", this.googleAuthenticationCode);
            pairs.Add("client_id", clientId);
            pairs.Add("client_secret", clientSecret);
            pairs.Add("redirect_uri", redirectURI);
            pairs.Add("grant_type", "authorization_code");
            //pairs.Add("grant_type", "authorization_code");

            var formContent = new Windows.Web.Http.HttpFormUrlEncodedContent(pairs);

            var client = new Windows.Web.Http.HttpClient();
            var httpResponseMessage = await client.PostAsync(new Uri("https://www.googleapis.com/oauth2/v4/token"), formContent);
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                System.Diagnostics.Debug.WriteLine($"OAuth authorization error: {httpResponseMessage.StatusCode}.");
                return string.Empty;
            }

            string jsonString = await httpResponseMessage.Content.ReadAsStringAsync();
            var jsonObject = Windows.Data.Json.JsonObject.Parse(jsonString);
            var accessToken = jsonObject["access_token"].GetString();


            //Call Google Calendar API
            using (var httpRequest = new Windows.Web.Http.HttpRequestMessage())
            {
                string calendarAPI = "https://www.googleapis.com/calendar/v3/calendars/kahizer241237@gmail.com/events";//"https://www.googleapis.com/calendar/v3/users/me/calendarList";

                httpRequest.Method = Windows.Web.Http.HttpMethod.Get;
                httpRequest.RequestUri = new Uri(calendarAPI);
                httpRequest.Headers.Authorization = new Windows.Web.Http.Headers.HttpCredentialsHeaderValue("Bearer", accessToken);

                var response = await client.SendRequestAsync(httpRequest);

                if (response.IsSuccessStatusCode)
                {
                    var listString = await response.Content.ReadAsStringAsync();
                    return listString;
                    //TODO
                }
            }

            return string.Empty;
        }

        #endregion
    }
}

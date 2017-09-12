using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Windows.Security.Authentication.Web;
using Windows.Foundation;
using SmartMirrorWinUniv.Concreates;

namespace SmartMirrorWinUniv.Services
{
    public class CalendarServices
    {
        #region Fields

        private string clientId = "425431929822-l1l3lt2e2ao9jttnq5gkl77td6gvvmuq.apps.googleusercontent.com";

        private string clientSecret = "zYbEBUcjnRJfH0a88FIcaSlW";

        private string redirectURI = "http://localhost";

        private string scope = "https://www.googleapis.com/auth/calendar.readonly";

        private Timer timer;

        private int elapseTime = 6000; //every 10 minutes

        private bool gotAccessToken = false;

        private string accessToken = string.Empty;

        private Windows.Web.Http.HttpClient client;

        private string holydaysCalendarId = "en.usa%23holiday%40group.v.calendar.google.com";

        #endregion

        #region Constructor

        public CalendarServices()
        {
            this.Authenticate();
        }


        #endregion

        #region Public Properties

        #endregion

        #region Public Events

        public event EventHandler<CalendarStatus> LatestCalendarEvent;

        #endregion

        #region Private Methods

        private async void Authenticate()
        {
            var SpotifyUrl = $"https://accounts.google.com/o/oauth2/auth?client_id={this.clientId}&redirect_uri={Uri.EscapeDataString(this.redirectURI)}&response_type=code&scope={Uri.EscapeDataString(this.scope)}";
            var StartUri = new Uri(SpotifyUrl);
            var EndUri = new Uri(redirectURI);

            if (this.gotAccessToken)
            {
                var calendar = this.GetRawEvents().Result;
                //var calendar = new CalendarStatus(data);
                this.LatestCalendarEvent?.Invoke(this, calendar);
                this.timer.Change(this.elapseTime, Timeout.Infinite);
            }
            else
            {
                WebAuthenticationResult WebAuthenticationResult = await WebAuthenticationBroker.AuthenticateAsync(WebAuthenticationOptions.None, StartUri, EndUri);
                if (WebAuthenticationResult.ResponseStatus == WebAuthenticationStatus.Success)
                {
                    var decoder = new WwwFormUrlDecoder(new Uri(WebAuthenticationResult.ResponseData).Query);
                    if (decoder[0].Name != "code")
                    {
                        System.Diagnostics.Debug.WriteLine($"OAuth authorization error: {decoder.GetFirstValueByName("error")}.");
                        return;
                    }

                    var autorizationCode = decoder.GetFirstValueByName("code");

                    var pairs = new Dictionary<string, string>();
                    pairs.Add("code", autorizationCode);
                    pairs.Add("client_id", this.clientId);
                    pairs.Add("client_secret", this.clientSecret);
                    pairs.Add("redirect_uri", this.redirectURI);
                    pairs.Add("grant_type", "authorization_code");

                    var formContent = new Windows.Web.Http.HttpFormUrlEncodedContent(pairs);

                    var client = new Windows.Web.Http.HttpClient();
                    var httpResponseMessage = await client.PostAsync(new Uri("https://www.googleapis.com/oauth2/v4/token"), formContent);
                    if (!httpResponseMessage.IsSuccessStatusCode)
                    {
                        System.Diagnostics.Debug.WriteLine($"OAuth authorization error: {httpResponseMessage.StatusCode}.");
                        return;
                    }

                    string jsonString = await httpResponseMessage.Content.ReadAsStringAsync();
                    var jsonObject = Windows.Data.Json.JsonObject.Parse(jsonString);
                    var accessToken = jsonObject["access_token"].GetString();
                    this.accessToken = accessToken;

                    this.gotAccessToken = true;
                    this.timer = new Timer(this.GetEvents, null, 0, Timeout.Infinite);

                }
                else
                {
                    this.gotAccessToken = false;
                }
            }
        }

        private void GetEvents(object state)
        {
            var calendar = this.GetRawEvents().Result;
            this.LatestCalendarEvent?.Invoke(this, calendar);
            this.timer.Change(this.elapseTime, Timeout.Infinite);
        }

        private async Task<CalendarStatus> GetRawEvents()
        {
            //Call Google Calendar API
            var yesterdayDateTime = DateTime.Now.AddDays(-1);
            var minDateString = yesterdayDateTime.ToString("O");

            var currentCalendar = new CalendarStatus();
            using (var httpRequest = new Windows.Web.Http.HttpRequestMessage())
            {
                var client = new Windows.Web.Http.HttpClient();
                string calendarAPI = $"https://www.googleapis.com/calendar/v3/calendars/kahizer241237@gmail.com/events?timeMin={minDateString}";

                httpRequest.Method = Windows.Web.Http.HttpMethod.Get;
                httpRequest.RequestUri = new Uri(calendarAPI);
                httpRequest.Headers.Authorization = new Windows.Web.Http.Headers.HttpCredentialsHeaderValue("Bearer", this.accessToken);

                var response = await client.SendRequestAsync(httpRequest);
                if (response.IsSuccessStatusCode)
                {
                    var listString = await response.Content.ReadAsStringAsync();
                    currentCalendar.AddRawCalendarItems(listString, "personal");
                }
            }

            using (var httpRequest = new Windows.Web.Http.HttpRequestMessage())
            {
                var client = new Windows.Web.Http.HttpClient();
                string holydayCalendarAPI =
                    $"https://www.googleapis.com/calendar/v3/calendars/{this.holydaysCalendarId}/events?timeMin={minDateString}";

                httpRequest.Method = Windows.Web.Http.HttpMethod.Get;
                httpRequest.RequestUri = new Uri(holydayCalendarAPI);
                httpRequest.Headers.Authorization =
                    new Windows.Web.Http.Headers.HttpCredentialsHeaderValue("Bearer", this.accessToken);

                var holidayresponse = await client.SendRequestAsync(httpRequest);

                
                if (holidayresponse.IsSuccessStatusCode)
                {
                    var listString = await holidayresponse.Content.ReadAsStringAsync();
                    currentCalendar.AddRawCalendarItems(listString, "public");
                }
            }

            return currentCalendar;
        }

        #endregion
    }
}

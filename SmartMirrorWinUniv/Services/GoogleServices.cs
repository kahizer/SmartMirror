using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Windows.Security.Authentication.Web;
using Windows.Foundation;
using SmartMirrorWinUniv.Concreates;



namespace SmartMirrorWinUniv.Services
{
    using Windows.Foundation;

    public class GoogleServices
    {
        #region Fields

        private string clientId = "425431929822-l1l3lt2e2ao9jttnq5gkl77td6gvvmuq.apps.googleusercontent.com";

        private string clientSecret = "zYbEBUcjnRJfH0a88FIcaSlW";

        private string redirectURI = "http://localhost";

        private string scope = "https://www.googleapis.com/auth/calendar.readonly https://www.googleapis.com/auth/gmail.readonly";

        private bool gotAccessToken = false;

        private string accessToken = string.Empty;

        private Windows.Web.Http.HttpClient client;

        private string holydaysCalendarId = "en.usa%23holiday%40group.v.calendar.google.com";

        public CalendarServices CalendarServices;

        public GmailServices GMailServices;

        #endregion

        #region Constructor

        public GoogleServices()
        {
            this.Authenticate();
        }

        #endregion

        #region
        #endregion

        #region Private Method

        private async void Authenticate()
        {
            var SpotifyUrl = $"https://accounts.google.com/o/oauth2/auth?client_id={this.clientId}&redirect_uri={Uri.EscapeDataString(this.redirectURI)}&response_type=code&scope={Uri.EscapeDataString(this.scope)}";
            var StartUri = new Uri(SpotifyUrl);
            var EndUri = new Uri(redirectURI);

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

                this.GMailServices = new GmailServices(this.accessToken);
                //this.CalendarServices = new CalendarServices(this.accessToken);

            }
        }

        #endregion
    }
}

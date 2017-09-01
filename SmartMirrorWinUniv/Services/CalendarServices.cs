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

        string[] scopes = new string[] {
             CalendarService.Scope.Calendar, // Manage your calendars
 	        CalendarService.Scope.CalendarReadonly // View your Calendars
         };

        #endregion

        #region Constructor

        public CalendarServices()
        {
            this.GetEvents();
        }


        private async void GetEvents()
        {
            var clientId = this.clientId;
            var redirectURI = "http://localhost";//"pw.oauth2:/oauth2redirect";
            var scope = "https://www.googleapis.com/auth/calendar.readonly";
            var SpotifyUrl = $"https://accounts.google.com/o/oauth2/auth?client_id={clientId}&redirect_uri={Uri.EscapeDataString(redirectURI)}&response_type=code&scope={Uri.EscapeDataString(scope)}";
            var StartUri = new Uri(SpotifyUrl);
            var EndUri = new Uri(redirectURI);

            // Get Authorization code
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


                //Get Access Token
                var pairs = new Dictionary<string, string>();
                pairs.Add("code", autorizationCode);
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
                    return;
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
                        //TODO
                    }
                }
            }
        
            //var path = @"temp1";//@"C:\Users\j.villanueva\Source\Repos\SmartMirror\SmartMirror\SmartMirrorWinUniv\bin\x86\Debug\temp";// @"C:\CalendarCsharp";

            //var tast = Task.Run(() =>
            //{
            //    UserCredential credential = default(UserCredential);
            //    FileDataStore store = new FileDataStore(path, true);

            //    credential =  GoogleWebAuthorizationBroker.AuthorizeAsync(
            //    new ClientSecrets
            //    {
            //        ClientId = this.clientId,
            //        ClientSecret = this.clientSecret
            //    }, new[] { CalendarService.Scope.Calendar }, "kahizer241237@gmail.com", CancellationToken.None, store).Result;


            //    BaseClientService.Initializer initializer = new BaseClientService.Initializer();
            //    initializer.HttpClientInitializer = credential;

            //    initializer.ApplicationName = "CalendarSample";
            //    CalendarService service = new CalendarService(initializer);

            //    IList<CalendarListEntry> list = service.CalendarList.List().Execute().Items;

            //    DisplayList(list);
            //    var cals = new List<CalendarListEntry>();
            //    foreach (Google.Apis.Calendar.v3.Data.CalendarListEntry calendar in list)
            //    {
            //        cals.Add(calendar);
            //        //DisplayFirstCalendarEvents(calendar);
            //    }

            //    Console.WriteLine("Press any key to continue...");
            //    Console.ReadKey();
            //});
        }

       

        private static void DisplayList(IList<CalendarListEntry> list)
        {
            Console.WriteLine("Lists of calendars:");
            foreach (CalendarListEntry item in list)
            {
                Console.WriteLine(item.Summary + ". Location: " + item.Location + ", TimeZone: " + item.TimeZone);
            }
        }

        #endregion

        #region Public Properties
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        #endregion
    }
}

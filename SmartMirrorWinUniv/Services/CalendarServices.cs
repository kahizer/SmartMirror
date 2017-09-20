namespace SmartMirrorWinUniv.Services
{
    using System;
    using System.Threading.Tasks;
    using System.Threading;
    using SmartMirrorWinUniv.Concreates;
    public class CalendarServices
    {
        #region Fields

        private Timer timer;

        private int elapseTime = 60000; //every 10 minutes

        private string accessToken = string.Empty;

        private Windows.Web.Http.HttpClient client;

        private string holydaysCalendarId = "en.usa%23holiday%40group.v.calendar.google.com";

        #endregion

        #region Constructor

        public CalendarServices(string token)
        {
            this.accessToken = token;
            this.timer = new Timer(this.GetEvents, null, 0, Timeout.Infinite);
        }


        #endregion

        #region Public Properties

        #endregion

        #region Public Events

        public event EventHandler<CalendarStatus> LatestCalendarEvent;

        public event EventHandler FailedToAuthenticate;

        #endregion

        #region Private Methods

        private void GetEvents(object state)
        {
            try
            {
                var calendar = this.GetRawEvents().Result;
                this.LatestCalendarEvent?.Invoke(this, calendar);
                this.timer.Change(this.elapseTime, Timeout.Infinite);
            }
            catch (Exception)
            {
                this.timer.Dispose();
                this.timer = null;
                this.FailedToAuthenticate?.Invoke(this, null);
            }
        }

        private async Task<CalendarStatus> GetRawEvents()
        {
            //Call Google Calendar API
            var yesterdayDateTime = DateTime.Now.Date;//DateTime.Now.AddDays(-1);
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
                else
                {
                    var errorRaw = await holidayresponse.Content.ReadAsStringAsync();
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
            }

            return currentCalendar;
        }

        #endregion
    }
}

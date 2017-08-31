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


            var path = @"temp1";//@"C:\Users\j.villanueva\Source\Repos\SmartMirror\SmartMirror\SmartMirrorWinUniv\bin\x86\Debug\temp";// @"C:\CalendarCsharp";

            var tast = Task.Run(() =>
            {
                UserCredential credential = default(UserCredential);
                FileDataStore store = new FileDataStore(path, true);

                credential =  GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets
                {
                    ClientId = this.clientId,
                    ClientSecret = this.clientSecret
                }, new[] { CalendarService.Scope.Calendar }, "kahizer241237@gmail.com", CancellationToken.None, store).Result;


                BaseClientService.Initializer initializer = new BaseClientService.Initializer();
                initializer.HttpClientInitializer = credential;

                initializer.ApplicationName = "CalendarSample";
                CalendarService service = new CalendarService(initializer);

                IList<CalendarListEntry> list = service.CalendarList.List().Execute().Items;

                DisplayList(list);
                var cals = new List<CalendarListEntry>();
                foreach (Google.Apis.Calendar.v3.Data.CalendarListEntry calendar in list)
                {
                    cals.Add(calendar);
                    //DisplayFirstCalendarEvents(calendar);
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            });

            



            

  
            
            


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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SmartMirrorWinUniv
{
    using Windows.UI.Core;
    using Windows.UI.Xaml.Controls;
    using SmartMirrorWinUniv.Concreates;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        #region Fields

        private ServiceManager serviceManager;

        #endregion

        #region

        public MainPage()
        {
            this.InitializeComponent();

            this.serviceManager = new ServiceManager();
            this.serviceManager.LatestEmailsEvent += this.ServiceManagerOnLatestEmailsEvent;
            this.serviceManager.CalendarStatusEvent += this.UpdateLatestCalendarEventsDisplay;
            this.serviceManager.TimerUpdateEvent += this.UpdaCurrentTimeDisplay;
            this.serviceManager.WeatherUpdateEvent += this.UpdateWeatherStatusDisplay;
            this.serviceManager.QuoteUpdateEvent += this.UpdateLatesQuoteDisplay;
            this.serviceManager.TrafficUpdateEvent += this.UpdateLatestTrafficDisplay;
            this.serviceManager.LatestNewsEvent += this.UpdateLatestNewsDisplay;
        }

        private void ServiceManagerOnLatestEmailsEvent(object sender, EmailStatus emailStatus)
        {
            Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                CoreDispatcherPriority.Normal,
                () =>
                    {
                        if (emailStatus.EmailMessages.Count > 0)
                        {
                            this.MailSubjectTextBlock00.Text = emailStatus.EmailMessages[0].Subject;
                            this.MailSenderTextBlock00.Text = emailStatus.EmailMessages[0].From;
                            this.MailSnipTextBlock00.Text = emailStatus.EmailMessages[0].Snippet;
                        }
                        if (emailStatus.EmailMessages.Count > 1)
                        {
                            this.MailSubjectTextBlock01.Text = emailStatus.EmailMessages[1].Subject;
                            this.MailSenderTextBlock01.Text = emailStatus.EmailMessages[1].From;
                            this.MailSnipTextBlock01.Text = emailStatus.EmailMessages[1].Snippet;
                        }
                        if (emailStatus.EmailMessages.Count > 2)
                        {
                            this.MailSubjectTextBlock02.Text = emailStatus.EmailMessages[2].Subject;
                            this.MailSenderTextBlock02.Text = emailStatus.EmailMessages[2].From;
                            this.MailSnipTextBlock02.Text = emailStatus.EmailMessages[2].Snippet;
                        }
                        if (emailStatus.EmailMessages.Count > 3)
                        {
                            this.MailSubjectTextBlock03.Text = emailStatus.EmailMessages[3].Subject;
                            this.MailSenderTextBlock03.Text = emailStatus.EmailMessages[3].From;
                            this.MailSnipTextBlock03.Text = emailStatus.EmailMessages[3].Snippet;
                        }
                        if (emailStatus.EmailMessages.Count > 4)
                        {
                            this.MailSubjectTextBlock04.Text = emailStatus.EmailMessages[4].Subject;
                            this.MailSenderTextBlock04.Text = emailStatus.EmailMessages[4].From;
                            this.MailSnipTextBlock04.Text = emailStatus.EmailMessages[4].Snippet;
                        }
                    }
            );
        }

        private void UpdateLatestCalendarEventsDisplay(object sender, CalendarStatus calendarStatus)
        {
            Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                CoreDispatcherPriority.Normal,
                () =>
                    {
                        if(calendarStatus.CalenderItems.Count > 0)
                            this.CalendarGroup00.DataContext = calendarStatus.CalenderItems[0];

                        if (calendarStatus.CalenderItems.Count > 1)
                            this.CalendarGroup01.DataContext = calendarStatus.CalenderItems[1];

                        if (calendarStatus.CalenderItems.Count > 2)
                            this.CalendarGroup02.DataContext = calendarStatus.CalenderItems[2];

                        if (calendarStatus.CalenderItems.Count > 3)
                            this.CalendarGroup03.DataContext = calendarStatus.CalenderItems[3];

                        if (calendarStatus.CalenderItems.Count > 4)
                            this.CalendarGroup04.DataContext = calendarStatus.CalenderItems[4];

                        if (calendarStatus.CalenderItems.Count > 5)
                            this.CalendarGroup05.DataContext = calendarStatus.CalenderItems[5];

                        if (calendarStatus.CalenderItems.Count > 6)
                            this.CalendarGroup06.DataContext = calendarStatus.CalenderItems[6];

                        if (calendarStatus.CalenderItems.Count > 7)
                            this.CalendarGroup07.DataContext = calendarStatus.CalenderItems[7];

                        if (calendarStatus.CalenderItems.Count > 8)
                            this.CalendarGroup08.DataContext = calendarStatus.CalenderItems[8];

                        if (calendarStatus.CalenderItems.Count > 9)
                            this.CalendarGroup09.DataContext = calendarStatus.CalenderItems[9];
                    }
            );
        }

        private void UpdateLatestNewsDisplay(object sender, NewsModel newsModel)
        {
            Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                CoreDispatcherPriority.Normal,
                () =>
                    {
                        this.NewsTextBlock01.Text = newsModel.LatestNews[0].Title;
                        this.NewsTextBlock02.Text = newsModel.LatestNews[1].Title;
                        this.NewsTextBlock03.Text = newsModel.LatestNews[2].Title;
                        this.NewsTextBlock04.Text = newsModel.LatestNews[3].Title;
                        this.NewsTextBlock05.Text = newsModel.LatestNews[4].Title;
                        this.NewsTextBlock06.Text = newsModel.LatestNews[5].Title;
                        this.NewsTextBlock07.Text = newsModel.LatestNews[6].Title;
                        this.NewsTextBlock08.Text = newsModel.LatestNews[7].Title;
                        this.NewsTextBlock09.Text = newsModel.LatestNews[8].Title;
                        
                    }
            );
        }

        private void UpdateLatestTrafficDisplay(object sender, TrafficStatus trafficStatus)
        {
            Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                CoreDispatcherPriority.Normal,
                () =>
                    {
                        this.TrafficInformationTextBlock.Text = $"{trafficStatus.Duration} min ({trafficStatus.Distance} mi)";
                        this.ArrivalTimeTextBlock.Text = $"ETA " + trafficStatus.ArrivalTime.ToString("h:mm tt");
                    }
            );
        }

        private void UpdateLatesQuoteDisplay(object sender, QuoteModel quoteModel)
        {
            Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                CoreDispatcherPriority.Normal,
                () =>
                    {
                        this.QuoteTextBlock.Text = quoteModel.Quote;
                        this.AuthorTextBlock.Text = quoteModel.Author;
                    }
            );
        }

        #endregion

        #region MyRegion

        private void UpdateWeatherStatusDisplay(object sender, WeatherStatus weather)
        {
            Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                CoreDispatcherPriority.Normal,
                () =>
                    {
                        this.SunsetTimeTextBlock.Text = weather.SunsetTime.ToString("h:mm tt");

                        this.WindSpeedTextBlock.Text = weather.WindSpeed.ToString();

                        this.MainIconTextBlock.Text = weather.IconCode;
                        this.TempTextBlock.Text = weather.Temperature.ToString() + "°";
                        this.DetailedSumaryTextBlock.Text = weather.DetailedSummary;

                        this.DayTextBlock01.Text = weather.DailySummary[1].ShortDayName;
                        this.IconTextBlock01.Text = weather.DailySummary[1].IconCode;
                        this.HighTempDay01.Text = weather.DailySummary[1].HightTemp.ToString();
                        this.LowTempDay01.Text = weather.DailySummary[1].LowTemp.ToString();

                        this.DayTextBlock02.Text = weather.DailySummary[2].ShortDayName;
                        this.IconTextBlock02.Text = weather.DailySummary[2].IconCode;
                        this.HighTempDay02.Text = weather.DailySummary[2].HightTemp.ToString();
                        this.LowTempDay02.Text = weather.DailySummary[2].LowTemp.ToString();

                        this.DayTextBlock03.Text = weather.DailySummary[3].ShortDayName;
                        this.IconTextBlock03.Text = weather.DailySummary[3].IconCode;
                        this.HighTempDay03.Text = weather.DailySummary[3].HightTemp.ToString();
                        this.LowTempDay03.Text = weather.DailySummary[3].LowTemp.ToString();

                        this.DayTextBlock04.Text = weather.DailySummary[4].ShortDayName;
                        this.IconTextBlock04.Text = weather.DailySummary[4].IconCode;
                        this.HighTempDay04.Text = weather.DailySummary[4].HightTemp.ToString();
                        this.LowTempDay04.Text = weather.DailySummary[4].LowTemp.ToString();

                        this.DayTextBlock05.Text = weather.DailySummary[5].ShortDayName;
                        this.IconTextBlock05.Text = weather.DailySummary[5].IconCode;
                        this.HighTempDay05.Text = weather.DailySummary[5].HightTemp.ToString();
                        this.LowTempDay05.Text = weather.DailySummary[5].LowTemp.ToString();

                        this.DayTextBlock06.Text = weather.DailySummary[6].ShortDayName;
                        this.IconTextBlock06.Text = weather.DailySummary[6].IconCode;
                        this.HighTempDay06.Text = weather.DailySummary[6].HightTemp.ToString();
                        this.LowTempDay06.Text = weather.DailySummary[6].LowTemp.ToString();

                        this.DayTextBlock07.Text = weather.DailySummary[7].ShortDayName;
                        this.IconTextBlock07.Text = weather.DailySummary[7].IconCode;
                        this.HighTempDay07.Text = weather.DailySummary[7].HightTemp.ToString();
                        this.LowTempDay07.Text = weather.DailySummary[7].LowTemp.ToString();
                    }
            );
        }

        private void UpdaCurrentTimeDisplay(object sender, CurrentTime currentTime)
        {
            Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                CoreDispatcherPriority.Normal,
                () =>
                    {
                        this.DayInfoTextBlock.Text = currentTime.DayInfo;
                        this.CurrentTimeTextBlock.Text = currentTime.ShortTime;
                    }
            );
        }


        //private void UpdateUI<T>(Func<T> func)
        //{
        //    //this.UpdateUI(() => this.UpdateTimeUI(currentTime));
        //    object result = null;
        //    Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
        //        CoreDispatcherPriority.Normal,
        //        () =>
        //            {
        //                 result = func();
        //            }
        //    );

        //    return result;
        //}


        #endregion
    }
}

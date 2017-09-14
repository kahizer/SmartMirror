using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.ServiceModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SmartMirrorWinUniv
{
    using Windows.UI.Core;
    using Windows.UI.Xaml.Media.Imaging;

    using SmartMirrorWinUniv.Services;
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
                        this.MailSubjectTextBlock00.Text = emailStatus.EmailMessages[0].Subject;
                        this.MailSenderTextBlock00.Text = emailStatus.EmailMessages[0].From;
                        this.MailSnipTextBlock00.Text = emailStatus.EmailMessages[0].Snippet;

                        this.MailSubjectTextBlock01.Text = emailStatus.EmailMessages[1].Subject;
                        this.MailSenderTextBlock01.Text = emailStatus.EmailMessages[1].From;
                        this.MailSnipTextBlock01.Text = emailStatus.EmailMessages[1].Snippet;

                        this.MailSubjectTextBlock02.Text = emailStatus.EmailMessages[2].Subject;
                        this.MailSenderTextBlock02.Text = emailStatus.EmailMessages[2].From;
                        this.MailSnipTextBlock02.Text = emailStatus.EmailMessages[2].Snippet;

                        this.MailSubjectTextBlock03.Text = emailStatus.EmailMessages[3].Subject;
                        this.MailSenderTextBlock03.Text = emailStatus.EmailMessages[3].From;
                        this.MailSnipTextBlock03.Text = emailStatus.EmailMessages[3].Snippet;

                        this.MailSubjectTextBlock04.Text = emailStatus.EmailMessages[3].Subject;
                        this.MailSenderTextBlock04.Text = emailStatus.EmailMessages[3].From;
                        this.MailSnipTextBlock04.Text = emailStatus.EmailMessages[3].Snippet;

                    }
            );
        }

        private void UpdateLatestCalendarEventsDisplay(object sender, CalendarStatus calendarStatus)
        {
            Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                CoreDispatcherPriority.Normal,
                () =>
                    {
                        this.CalendarSummaryTextBlock00.Text = calendarStatus.CalenderItems[0].Title;
                        this.CalendarDueTextBlock00.Text = calendarStatus.CalenderItems[0].EasyDueDate;

                        this.CalendarSummaryTextBlock01.Text = calendarStatus.CalenderItems[1].Title;
                        this.CalendarDueTextBlock01.Text = calendarStatus.CalenderItems[1].EasyDueDate;

                        this.CalendarSummaryTextBlock02.Text = calendarStatus.CalenderItems[2].Title;
                        this.CalendarDueTextBlock02.Text = calendarStatus.CalenderItems[2].EasyDueDate;

                        this.CalendarSummaryTextBlock03.Text = calendarStatus.CalenderItems[3].Title;
                        this.CalendarDueTextBlock03.Text = calendarStatus.CalenderItems[3].EasyDueDate;

                        this.CalendarSummaryTextBlock04.Text = calendarStatus.CalenderItems[4].Title;
                        this.CalendarDueTextBlock04.Text = calendarStatus.CalenderItems[4].EasyDueDate;

                        this.CalendarSummaryTextBlock05.Text = calendarStatus.CalenderItems[5].Title;
                        this.CalendarDueTextBlock05.Text = calendarStatus.CalenderItems[5].EasyDueDate;

                        this.CalendarSummaryTextBlock06.Text = calendarStatus.CalenderItems[6].Title;
                        this.CalendarDueTextBlock06.Text = calendarStatus.CalenderItems[6].EasyDueDate;

                        this.CalendarSummaryTextBlock07.Text = calendarStatus.CalenderItems[7].Title;
                        this.CalendarDueTextBlock07.Text = calendarStatus.CalenderItems[7].EasyDueDate;

                        this.CalendarSummaryTextBlock08.Text = calendarStatus.CalenderItems[8].Title;
                        this.CalendarDueTextBlock08.Text = calendarStatus.CalenderItems[8].EasyDueDate;

                        this.CalendarSummaryTextBlock09.Text = calendarStatus.CalenderItems[9].Title;
                        this.CalendarDueTextBlock09.Text = calendarStatus.CalenderItems[9].EasyDueDate;

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

        #region Public Properties

        public CurrentTime Time { get; set; }
        public NewsModel News { get; set; }
        public QuoteModel Quote { get; set; }
        public TrafficStatus Traffic { get; set; }
        public WeatherStatus Weather { get; set; }

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

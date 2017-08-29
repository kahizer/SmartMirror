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
            this.DataContext = this;

            this.serviceManager = new ServiceManager();
            this.serviceManager.TimerUpdateEvent += this.UpdaCurrentTimeDisplay;
            this.serviceManager.WeatherUpdateEvent += this.UpdateWeatherStatusDisplay;
            this.serviceManager.QuoteUpdateEvent += this.UpdateLatesQuoteDisplay;
            
            //this.serviceManager.LatestNewsEvent += this.UpdateLatestNewsDisplay;
            
            //this.serviceManager.TrafficUpdateEvent += this.UpdateLatestTrafficDisplay;



            //this.Traffic = this.serviceManager.GetTrafficInformation();
            //this.News = this.serviceManager.GetNews();
            //this.Weather = this.serviceManager.GetWeather();
            //this.TempTextBlock.Text = this.Weather.Temperature.ToString()+ "°";
            //this.Quote = this.serviceManager.GetQuoteOfTheDay();
            //this.QuoteTextBlock.DataContext = this.Quote;
            //this.AuthorTextBlock.DataContext = this.Quote;
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
                        this.TempTextBlock.Text = weather.Temperature.ToString() + "°";
                        this.SumaryTextBlock.Text = weather.Summary;

                        this.DayTextBlock01.Text = weather.DailySummary[1].ShortDayName;
                        this.HighTempDay01.Text = weather.DailySummary[1].HightTemp.ToString();
                        this.LowTempDay01.Text = weather.DailySummary[1].LowTemp.ToString();

                        this.DayTextBlock02.Text = weather.DailySummary[2].ShortDayName;
                        this.HighTempDay02.Text = weather.DailySummary[2].HightTemp.ToString();
                        this.LowTempDay02.Text = weather.DailySummary[2].LowTemp.ToString();

                        this.DayTextBlock03.Text = weather.DailySummary[3].ShortDayName;
                        this.HighTempDay03.Text = weather.DailySummary[3].HightTemp.ToString();
                        this.LowTempDay03.Text = weather.DailySummary[3].LowTemp.ToString();

                        this.DayTextBlock04.Text = weather.DailySummary[4].ShortDayName;
                        this.HighTempDay04.Text = weather.DailySummary[4].HightTemp.ToString();
                        this.LowTempDay04.Text = weather.DailySummary[4].LowTemp.ToString();

                        this.DayTextBlock05.Text = weather.DailySummary[5].ShortDayName;
                        this.HighTempDay05.Text = weather.DailySummary[5].HightTemp.ToString();
                        this.LowTempDay05.Text = weather.DailySummary[5].LowTemp.ToString();

                        this.DayTextBlock06.Text = weather.DailySummary[6].ShortDayName;
                        this.HighTempDay06.Text = weather.DailySummary[6].HightTemp.ToString();
                        this.LowTempDay06.Text = weather.DailySummary[6].LowTemp.ToString();
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

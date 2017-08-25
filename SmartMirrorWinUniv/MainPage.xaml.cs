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
using System.ServiceModel.

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SmartMirrorWinUniv
{
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

            this.Time = new CurrentTime(DateTime.Now);
            this.DayInfoTextBlock.Text = this.Time.DayInfo;
            this.CurrentTimeTextBlock.Text = this.Time.ShortTime;

            

            //this.Traffic = this.serviceManager.GetTrafficInformation();
            //this.News = this.serviceManager.GetNews();
            this.Weather = this.serviceManager.GetWeather();
            this.TempTextBlock.Text = this.Weather.Temperature.ToString()+ "°";
            this.Quote = this.serviceManager.GetQuoteOfTheDay();
            this.QuoteTextBlock.DataContext = this.Quote;
            this.AuthorTextBlock.DataContext = this.Quote;
        }

        #endregion

        #region Public Properties

        public CurrentTime Time { get; set; }
        public NewsModel News { get; set; }
        public QuoteModel Quote { get; set; }
        public TrafficStatus Traffic { get; set; }
        public WeatherStatus Weather { get; set; }

        #endregion
    }
}

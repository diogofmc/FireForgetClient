using System;
using System.ComponentModel;
using System.Threading.Tasks;
using FireForgert.Responses;
using FireForgert.TaskMonitor;
using Xamarin.Forms;

namespace FireForgetClient
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private MainPageViewModel _vm;

        public MainPage()
        {
            InitializeComponent();
            _vm = new MainPageViewModel();
            BindingContext = _vm;
        }

    }
}

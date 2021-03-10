using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ScanDemo.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "Parametre";
            //OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://xamarin.com"));
        }

        //public ICommand OpenWebCommand { get; }
    }
}
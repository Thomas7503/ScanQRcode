using ScanDemo.Models;
using ScanDemo.Services;
using System;
using System.ComponentModel;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ScanDemo.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        public void LogoutOut(object sender, EventArgs e)
        {
            DataStore.Logout();
            DataStore.client = new HttpClient();
            DataStore.user = new User();
            Navigation.PopModalAsync();
        }
    }
}
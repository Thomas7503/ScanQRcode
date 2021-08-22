using System;
using System.ComponentModel;
using Xamarin.Forms;
using ScanDemo.Models;
using ScanDemo.Services;
using Newtonsoft.Json.Linq;

using System.Net;
using System.Net.Http;


namespace ScanDemo.Views
{
    [DesignTimeVisible(false)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        public async void LoginClicked(object sender, EventArgs e)
        {
            string mail = this.Email.Text;
            string pass = this.Pass.Text;

            HttpResponseMessage response = await DataStore.Login(mail, pass);

            if (response.StatusCode == HttpStatusCode.Found)
            {
                string message = await response.Content.ReadAsStringAsync();
                JObject o = JObject.Parse(message);
                DataStore.user = o.ToObject<User>();
                //Constante.utilisateur.Connecter = true;
                await Navigation.PushModalAsync(new NavigationPage(new MainPage()));
            }
            else
            {
                await DisplayAlert("Echec d'authentification", "Vos identifiants ne sont pas valide.", "OK");
            }
        }

        public async void RegisterClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new RegisterPage()));
        }
    }
}
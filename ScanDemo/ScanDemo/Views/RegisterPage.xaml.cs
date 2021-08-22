using System;
using System.ComponentModel;
using Xamarin.Forms;
using ScanDemo.Models;
using ScanDemo.Services;
using Newtonsoft.Json.Linq;

using System.Net;
using System.Net.Http;
using Xamarin.Forms.Xaml;

namespace ScanDemo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        HttpResponseMessage response;

        public RegisterPage()
        {
            InitializeComponent();
        }

        public async void RegisterClicked(object sender, EventArgs e)
        {
            string mail = this.Email.Text;
            string pass = this.Pass.Text;
            string passConf = this.PassConfirm.Text;

            if (mail.Contains("@") && mail.Contains("."))
            {
                if (pass.Length > 5 && pass.Equals(passConf))
                {
                    User user = new User(mail, pass);

                    response = await DataStore.AddUser(user);
                }
                else
                {
                    await DisplayAlert("Echec d'authentification", "Mot de passe trop court ou different.", "OK");
                }
            }
            else
            {
                await DisplayAlert("Echec d'authentification", "Email invalide.", "OK");
            }
        }

        public async void ValideCodeMailClicked(object sender, EventArgs e)
        {
            string mail = this.Email.Text;
            string pass = this.Pass.Text;
            string codeMail = this.CodeMail.Text;

            response = await DataStore.ConfirmUser(mail,pass,codeMail);

            if (response.StatusCode == HttpStatusCode.OK) 
            {
                string message = await response.Content.ReadAsStringAsync();
                JObject o = JObject.Parse(message);
                DataStore.user = o.ToObject<User>();
                //Constante.utilisateur.Connecter = true;
                await Navigation.PushModalAsync(new NavigationPage(new MainPage()));
            }
            else
            {
                await DisplayAlert("Echec d'authentification", "Le code est incorrect.", "OK");
            }
        }
    }
}
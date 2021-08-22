using ScanDemo.Services;
using ScanDemo.Views;
using System;
using System.IO;
using System.Net.Http;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ScanDemo
{
    public partial class App : Application
    {
        private string dbPath = Path.Combine(FileSystem.AppDataDirectory, "database.db3");

        //public static PromotionRepository PromotionRepository { get; private set; }
        public App()
        {
            InitializeComponent();

            //PromotionRepository = new PromotionRepository(dbPath);

            DependencyService.Register<DataStore>();

            //HttpResponseMessage response = DataStore.Login(mail, pass);

            //if (response.StatusCode == HttpStatusCode.Found)
            //{
            //    string message = await response.Content.ReadAsStringAsync();
            //    JObject o = JObject.Parse(message);
            //    DataStore.user = o.ToObject<User>();
            //    //Constante.utilisateur.Connecter = true;
            //    await Navigation.PushModalAsync(new NavigationPage(new MainPage()));
            //}
            //else
            //{
            //    MainPage = new MainPage();
            //}

            MainPage = new LoginPage();
           // MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
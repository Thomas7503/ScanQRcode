using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ScanDemo.Models;
using ScanDemo.Services;
using ScanDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing;
using ZXing.Client.Result;

//namespace ScanDemo.Views
//{
//    [XamlCompilation(XamlCompilationOptions.Compile)]
//    public partial class ScanPage : ContentPage
//    {
//        ScanViewModel viewModel;

//        public ScanPage()
//        {
//            InitializeComponent();
//            BindingContext = viewModel = new ScanViewModel();
//        }

//        public void scanView_OnScanResult(Result result)
//        {
//            Device.BeginInvokeOnMainThread(async () =>
//            {
//                await DisplayAlert("Scanned result", "The barcode's text is " + result.Text + ". The barcode's format is " + result.BarcodeFormat, "OK");
//            });

//            //TraitementScan(result.Text);
//        }

//        //private void TraitementScan(string qrcode)
//        public void TestQrCode(object sender, EventArgs e)
//        {

//            //string StringReturnJson = "{'Titre':'First item','joe':'First item'}";
//            string StringReturnJson = "{'Titre':'First bob','Pourcentage':10,'Code':'uythgfgty','Adresse':'montpellier','DateDeFin':'08-10-2022'}";
//            Item item = JsonConvert.DeserializeObject<Item>(StringReturnJson);
//            if(item.Titre == null || item.Pourcentage == 0 || item.Code == null || item.Adresse == null || item.DateDeFin == null )
//            {
//                DisplayAlert("ERROR", "Code Invalide","OK!!!!!!!!!");
//            }
//            else
//            {
//                MessagingCenter.Send(this, "AddItem", item);
//            }
//        }
//    }
//}

namespace ScanDemo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScanPage : ContentPage
    {
        ScanViewModel viewModel;
        //private string myDate;
        //readonly List<Item> items;
        private readonly IDataStore<Item> data;

        public ScanPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new ScanViewModel();
        }

        public void TestQrCode(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                //string StringReturnJson = "{'Titre':'First item','joe':'First item'}";
                string StringReturnJson = "{'Titre':'First dfdfd','Pourcentage':10,'Code':'ff','Adresse':'montpellier','DateDeFin':'08-10-2022'}";
                Item item = JsonConvert.DeserializeObject<Item>(StringReturnJson);
                if (item.Titre == null || item.Pourcentage == 0 || item.Code == null || item.Adresse == null || item.DateDeFin == null)
                {
                    DisplayAlert("ERROR", "Code Invalide", "OK");
                }
                else
                {
                    bool confirm = await DisplayAlert("Coupon", "Voulez vous ajouter le coupon ?", "yes", "no");
                    if (confirm)
                    {
                        await DataStore.AddCoupon(DataStore.user.Id.ToString(), item.Id.ToString());
                    }
                    //MessagingCenter.Send(this, "AddItem", item);
                    ////Navigation.PushModalAsync(new NavigationPage(new ItemsPage()));
                    //Navigation.PushModalAsync(new NavigationPage(new ItemDetailPage(new ItemDetailViewModel(item))));
                }
            });
        }

        public void scanView_OnScanResult(Result result)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {

 
                DateTime myDate = DateTime.Now;
                var item = JsonConvert.DeserializeObject<Item>(result.Text);
                DateTime DateDeFin = DateTime.Parse(item.DateDeFin);

                if (DateDeFin != null)
                {
                    int dateResult = DateTime.Compare(DateDeFin, myDate);

                    if (dateResult > 0)
                    {
                        if (string.IsNullOrWhiteSpace(item.Titre) || string.IsNullOrEmpty(item.Titre) || item.Pourcentage < 0 || item.Pourcentage > 100 || string.IsNullOrWhiteSpace(item.Code) || string.IsNullOrEmpty(item.Code)
                        || string.IsNullOrWhiteSpace(item.Adresse) || string.IsNullOrEmpty(item.Adresse))
                        {
                            await DisplayAlert("Erreur", "Code Invalide.", "OK");
                        }
                        else
                        {
                            bool confirm = await DisplayAlert("Coupon", "Voulez vous ajouter le coupon ?", "yes", "no");
                            if (confirm)
                            {
                                await DataStore.AddCoupon(DataStore.user.Id.ToString(), item.Id.ToString());
                            }
                            //MessagingCenter.Send(this, "AddItem", item);
                            //await Navigation.PushModalAsync(new NavigationPage(new ItemDetailPage(new ItemDetailViewModel(item))));
                        }
                    }
                    else
                    {
                        // La date de péremption n'est pas bonne, on renvoie une erreur
                        await DisplayAlert("Erreur : Date dépassée", "La date de péremption est dépassée.", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Erreur", "La date est manquante.", "OK");
                }
            });
        }
    }
}
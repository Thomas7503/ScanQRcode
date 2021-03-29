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
            //string StringReturnJson = "{'Titre':'First item','joe':'First item'}";
            string StringReturnJson = "{'Titre':'First dfdfd','Pourcentage':10,'Code':'ff','Adresse':'montpellier','DateDeFin':'08-10-2022'}";
            Item item = JsonConvert.DeserializeObject<Item>(StringReturnJson);
            if (item.Titre == null || item.Pourcentage == 0 || item.Code == null || item.Adresse == null || item.DateDeFin == null)
            {
                DisplayAlert("ERROR", "Code Invalide", "OK");
            }
            else
            {
                MessagingCenter.Send(this, "AddItem", item);
                Navigation.PushModalAsync(new NavigationPage(new ItemsPage()));
            }
        }

        public void scanView_OnScanResult(Result result)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                //// On crée un nouvel objet Mock
                ////IDataStore<Item> data = new IDataStore<Item();

                //// On crée une nouvelle date du jour
                //DateTime myDate = DateTime.Now;

                //// On récupère l'objet en JSON
                //var item = JsonConvert.DeserializeObject<Item>(result.Text);

                //DateTime DateDeFin = DateTime.Parse(item.DateDeFin);
                //// On compare la date du jour avec la date récupérée dans le QR Code
                //int dateResult = DateTime.Compare(DateDeFin, myDate);

                //if (dateResult < 0)
                //{
                //    // Si la date de péremption est bonne on cherche l'item en base de données
                //    var objQR = data.GetItemByCodeAsync(item.Code);

                //    if (objQR != null)
                //    // Si l'objet existe
                //    {
                //        await DisplayAlert("Erreur : Ajout du QR Code", "Vous possédez déjà le QR Code scanné.", "OK");
                //        //return;
                //    }
                //    else
                //    {
                //        // Si l'objet est null on l'ajoute à la base de données
                //        //var addItem = data.AddItemAsync(item).Result;
                //        var addItem = data.AddItem(item).Result;

                //        if (addItem)
                //        {
                //            // L'ajout n'a pas réussi, on renvoit une erreur
                //            await DisplayAlert("Erreur : Ajout du QR Code", "L'ajout du scan a échoué.", "OK");
                //            //return;
                //        }
                //        else
                //        {
                //            await DisplayAlert("Succés : Ajout du QR Code", "L'ajout du scan a réussi.", "OK");
                //            //return;
                //        }

                //    }
                //}
                //else
                //{
                //    // La date de péremption n'est pas bonne, on renvoie une erreur
                //    await DisplayAlert("Erreur : Date dépassée", "La date de péremption est dépassée.", "OK");
                //    //return;
                //}

                DateTime myDate = DateTime.Now;
                var item = JsonConvert.DeserializeObject<Item>(result.Text);
                DateTime DateDeFin = DateTime.Parse(item.DateDeFin);
                int dateResult = DateTime.Compare(DateDeFin, myDate);

                if (dateResult < 0)
                {
                    if (item.Titre == null || item.Pourcentage == 0 || item.Code == null || item.Adresse == null || item.DateDeFin == null)
                    {
                        await DisplayAlert("ERROR", "Code Invalide", "OK");
                    }
                    else
                    {
                        MessagingCenter.Send(this, "AddItem", item);
                        await Navigation.PushModalAsync(new NavigationPage(new ItemsPage()));
                    }
                }
                else
                {
                    // La date de péremption n'est pas bonne, on renvoie une erreur
                    await DisplayAlert("Erreur : Date dépassée", "La date de péremption est dépassée.", "OK");
                }

            });
        }
    }
}
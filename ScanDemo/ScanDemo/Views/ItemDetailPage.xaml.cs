using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ScanDemo.Models;
using ScanDemo.ViewModels;
using Newtonsoft.Json;
using ScanDemo.Services;

namespace ScanDemo.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;
        public Item Item { get; set; }


        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public ItemDetailPage()
        {
            InitializeComponent();

            //Item = new Item
            //{
            //    Titre = "Item 1",
            //    Pourcentage = 50,
            //    Code = "ertf1",
            //    Adresse = "adresse 1",
            //    DateDeFin = "20/20/2555"
            //};

            viewModel = new ItemDetailViewModel(Item);
            BindingContext = viewModel;
        }

        async void DeleteItem_Clicked(object sender, EventArgs e)
        {
            //MessagingCenter.Send(this, "DeleteItem", viewModel.Item);
            //await Navigation.PushModalAsync(new NavigationPage(new MainPage()));
            bool confirm = await DisplayAlert("Coupon", "Voulez vous supprimer le coupon ?", "yes", "no");
            if (confirm)
            {
                await DataStore.DeleteCoupon(DataStore.user.Id.ToString(), viewModel.Item.Id.ToString());
                await Navigation.PushModalAsync(new NavigationPage(new ItemsPage()));
            }
        }


        //public async void DeleteItem(object sender, EventArgs e)
        //{
        //    bool confirm = await DisplayAlert("Coupon", "Voulez vous supprimer le coupon ?", "yes", "no");
        //    if (confirm)
        //    {
        //        var s = sender.ToString();
        //        //var x = dele
        //        var item = JsonConvert.DeserializeObject<Item>(sender.ToString());
        //        await DataStore.DeleteCoupon(DataStore.user.Id.ToString(), item.Id.ToString());
        //    }
        //}
    }
}
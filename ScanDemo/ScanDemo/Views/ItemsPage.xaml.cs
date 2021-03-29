using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ScanDemo.Models;
using ScanDemo.Views;
using ScanDemo.ViewModels;
using ScanDemo.Services;

namespace ScanDemo.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel viewModel;
        //IDataStore<Item> MockDataStore;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ItemsViewModel();
        }

        // public async Task Initialize()
        //{
        //    MockDataStore MockDataStore = new MockDataStore();
        //    await MockDataStore.GetAllItems();

        //    //foreach(Item Item in AllItems)
        //    //MessagingCenter.Send(this, "AddItem", Item);
        //}

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (!(args.SelectedItem is Item item))
                return;

            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        }

        protected override /*async*/ void OnAppearing()
        {
            base.OnAppearing();

            //if (viewModel.Items.Count == 0)
            //    viewModel.LoadItemsCommand.Execute(null);

            viewModel.LoadItemsCommand.Execute(null);



            //await MockDataStore.GetAllItems();
        }
    }
}
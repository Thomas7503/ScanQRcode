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
using Newtonsoft.Json;

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

//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Xamarin.Forms;
//using Xamarin.Forms.Xaml;
//using ScanDemo.Models;
//using ScanDemo.Views;
//using ScanDemo.ViewModels;
//using ScanDemo.Services;
//using Newtonsoft.Json;
//using System.Collections.ObjectModel;

//namespace ScanDemo.Views
//{
//    // Learn more about making custom code visible in the Xamarin.Forms previewer
//    // by visiting https://aka.ms/xamarinforms-previewer
//    [DesignTimeVisible(false)]
//    public partial class ItemsPage : ContentPage
//    {
//        ItemsViewModel viewModel;
//        //IDataStore<Item> MockDataStore;
//        //List<Item> Items = new List<Item>();
//        ObservableCollection<Item> Items { get; set; }
//        //List<Item> Items ;

//        //public ObservableCollection<Item> Items { get { return Items; } }

//        public ItemsPage()
//        {
//            InitializeComponent();

//            //ItemsListView.ItemsSource = Items;
//            //Items = new ObservableCollection<Item>();
//            //Items =  GetItems();

//            //BindingContext = this;


//            //BindingContext = viewModel = new ItemsViewModel();           
//        }

//        //public async Task<List<Item>> GetItems()
//        //{
//        //    List<Item> Items = await Task.Run(() => DataStore.GetAllItems(DataStore.user.Id.ToString()));
//        //    //var L = await Task.Run(() => DataStore.GetAllItems(DataStore.user.Id.ToString()));
//        //    return Items;
//        //}


//        // ObservableCollection allows items to be added after ItemsSource
//        // is set and the UI will react to changes
//        //Items.Add(new Employee { DisplayName = "Rob Finnerty" });
//        //Items.Add(new Employee { DisplayName = "Bill Wrestler" });
//        //Items.Add(new Employee { DisplayName = "Dr. Geri-Beth Hooper" });
//        //Items.Add(new Item { DisplayName = "Dr. Keith Joyce-Purdy" });
//        //Items.Add(new Item { DisplayName = "Sheri Spruce" });
//        //Items.Add(new Item { DisplayName = "Burt Indybrick" });


//        // public async Task Initialize()
//        //{
//        //    MockDataStore MockDataStore = new MockDataStore();
//        //    await MockDataStore.GetAllItems();

//        //    //foreach(Item Item in AllItems)
//        //    //MessagingCenter.Send(this, "AddItem", Item);
//        //}

//        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
//        {
//            if (!(args.SelectedItem is Item item))
//                return;

//            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

//            // Manually deselect item.
//            ItemsListView.SelectedItem = null;
//        }

//        async void AddItem_Clicked(object sender, EventArgs e)
//        {
//            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
//        }

//        protected override /*async*/ void OnAppearing()
//        {
//            base.OnAppearing();

//            //if (viewModel.Items.Count == 0)
//            //    viewModel.LoadItemsCommand.Execute(null);

//            viewModel.LoadItemsCommand.Execute(null);

//            //await MockDataStore.GetAllItems();
//        }

//    }
//}
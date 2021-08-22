using ScanDemo.Models;
using ScanDemo.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace ScanDemo.ViewModels
{
    public class ScanViewModel: BaseViewModel
    {
        public ObservableCollection<Item> Items { get; set; }
        public Command LoadScanCommand { get; set; }

        public ScanViewModel()
        {
            Title = "Scanner";

            Items = new ObservableCollection<Item>();
            LoadScanCommand = new Command(async () => await ExecuteLoadScanCommand());
                       

            //MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            MessagingCenter.Subscribe<ScanPage, Item>(this, "AddItem", async (obj, item) =>
            {

                bool res = false;
                if (Items.Count == 0)
                {
                    res = true;
                }
                else
                {
                    foreach (Item i in Items)
                    {
                        if (i.Code != item.Code)
                        {
                            res = true;
                        }
                        else
                        {
                            res = false;
                            break;
                        }
                    }
                }

                if (res)
                {
                    Items.Add(item);
                    DataStore.AddItem(item);
                }
            });
        }

        async Task ExecuteLoadScanCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                //var items = await DataStore.GetItemsAsync(true);
                var items = await DataStore.GetAllItems();
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}

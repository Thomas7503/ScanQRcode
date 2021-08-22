using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;
using ScanDemo.Services;

using ScanDemo.Models;
using ScanDemo.Views;

namespace ScanDemo.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ItemsViewModel()
        {
            Title = "Liste Promotion";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            

            //MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem",(obj, item) =>
            {
                bool res = false;
                if(Items.Count == 0)
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

            MessagingCenter.Subscribe<ItemDetailPage, Item>(this, "DeleteItem", (obj, item) =>
            {
                //bool res = false;
                //foreach (Item i in Items)
                //{
                //    if (i.Code != item.Code)
                //    {
                //        res = true;
                //    }
                //    else
                //    {
                //        res = false;
                //        break;
                //    }
                //}

                //if (res)
                //{
                    Items.Remove(item);
                    DataStore.DeleteItem(item);
                //}
            });
        }

        async Task ExecuteLoadItemsCommand()
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
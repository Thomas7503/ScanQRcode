﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ScanDemo.Models;

namespace ScanDemo.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        public List<Item> items;
        readonly HttpClient client = new HttpClient();

        public MockDataStore()
        {
            //items = new List<Item>()
            //{
            //    new Item { Id = 1, Titre = "First item", Pourcentage = 10, Code = "ABCD", Adresse = "montpellier", DateDeFin="08/10/2022" },
            //    new Item { Id = 2, Titre = "Second item", Pourcentage = 10, Code = "ABCD", Adresse = "montpellier", DateDeFin="08/10/2022" },
            //    new Item { Id = 3, Titre = "Third item", Pourcentage = 10, Code = "ABCD", Adresse = "montpellier", DateDeFin="08/10/2022" },
            //    new Item { Id = 4, Titre = "Fourth item", Pourcentage = 10, Code = "ABCD", Adresse = "montpellier", DateDeFin="08/10/2022" },
            //    new Item { Id = 5, Titre = "Fifth item", Pourcentage = 10, Code = "ABCD", Adresse = "montpellier", DateDeFin="08/10/2022" },
            //    new Item { Id = 6, Titre = "Sixth item", Pourcentage = 10, Code = "ABCD", Adresse = "montpellier", DateDeFin="08/10/2022" },
            //    new Item { Id = 7, Titre = "My item", Pourcentage = 10, Code = "ABCD", Adresse = "montpellier", DateDeFin="08/10/2022" }
            //};
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            //var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            var oldItem = items.FirstOrDefault((Item arg) => arg.Id == item.Id);

            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(long id)
        {
            var oldItem = items.FirstOrDefault((Item arg) => arg.Id == id);
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        //public async Task<Item> GetItemAsync(long id)
        //{
        //    return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        //}

        public async Task<Item> GetItemByCodeAsync(string code)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Code == code));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }

        public async Task<List<Item>> GetAllItems()
        {
            string message;
            Uri uri = new Uri("https://gostylemspr.herokuapp.com/getCoupon");
            HttpResponseMessage response = await client.GetAsync(uri);

            if (response.StatusCode == HttpStatusCode.Found)
            {
                message = await response.Content.ReadAsStringAsync();
                JArray coupons = JArray.Parse(message);

                items = coupons.Select(x => new Item
                {
                    Id = (long)x["id"],
                    Titre = (string)x["titre"],
                    Pourcentage = (int)x["pourcentage"],
                    Code = (string)x["code"],
                    Adresse = (string)x["adresse"],
                    DateDeFin = (string)x["dateDeFin"],
                }).ToList();
            }

            return  items;
        }

        public async Task<bool> AddItem(Item item)
        {
            Uri uri = new Uri("https://gostylemspr.herokuapp.com/addCoupon" +
                "?titre=" + item.Titre + "&pourcentage=" + item.Pourcentage + "&code=" +
                item.Code + "&adresse=" + item.Adresse + "&dateDeFin=" + item.DateDeFin);


            await client.PostAsync(uri, null);

            items.Add(item);

            return await Task.FromResult(true);
        }
    }
}
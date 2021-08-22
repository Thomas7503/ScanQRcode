using System;
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
    public class DataStore : IDataStore<Item>
    {
        public static List<Item> items;
        public static HttpClient client = new HttpClient();
        public static User user = new User();



        public DataStore()
        {
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
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
            string userId = user.Id.ToString();
            Uri uri = new Uri("https://gostylemspr.herokuapp.com/getCoupon?idUser=" + userId);
            HttpResponseMessage response = await client.GetAsync(uri);

            if (response.StatusCode == HttpStatusCode.OK)
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

            // get all coupon to user !!!

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

        public async void DeleteItem(Item item)
        {
            Uri uri = new Uri("https://gostylemspr.herokuapp.com/deleteCoupon" + "?id=" + item.Id);

            string uriString = uri.ToString();

            await client.DeleteAsync(uriString);

            items.Remove(item);
        }

        public static async Task<HttpResponseMessage> AddCoupon(string idUser, string idCoupon)
        {
            Uri uri = new Uri("https://gostylemspr.herokuapp.com/addCoupon?idUser=" + idUser + "&idCoupon=" + idCoupon);
            return await client.PostAsync(uri, null);
        }

        public static async Task<HttpResponseMessage> DeleteCoupon(string idUser, string idCoupon)
        {
            Uri uri = new Uri("https://gostylemspr.herokuapp.com/deleteCoupon?idUser=" + idUser + "&idCoupon=" + idCoupon);
            return await client.DeleteAsync(uri);
        }

        public static async Task<HttpResponseMessage> AddUser(User user)
        {
            Uri uri = new Uri("https://gostylemspr.herokuapp.com/addUser" +
                "?email=" + user.Email + "&password=" + user.Password);

            return await client.PostAsync(uri, null);
        }

        public static async Task<HttpResponseMessage> Login(string mail, string pass)
        {
            Uri uri = new Uri("https://gostylemspr.herokuapp.com/login" +
                "?email=" + mail + "&password=" + pass);


            return await client.GetAsync(uri);
        }

        public static void Logout()
        {
            Uri uri = new Uri("https://gostylemspr.herokuapp.com/logout");

            client.GetAsync(uri);
        }

        public static async Task<HttpResponseMessage> ConfirmUser(string mail, string pass, string code)
        {
            Uri uri = new Uri("https://gostylemspr.herokuapp.com/confirmUser" +
                "?email=" + mail + "&password=" + pass + "&code" + code);

            return await client.PostAsync(uri, null);
        }
    }
}
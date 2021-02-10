﻿using ScanDemo.Repositories;
using ScanDemo.Views;
using System;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ScanDemo
{
    public partial class App : Application
    {
        private string dbPath = Path.Combine(FileSystem.AppDataDirectory, "database.db3");

        public static PromotionRepository PromotionRepository { get; private set; }
        public App()
        {
            InitializeComponent();

            PromotionRepository = new PromotionRepository(dbPath);

            MainPage = new MainPage();
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
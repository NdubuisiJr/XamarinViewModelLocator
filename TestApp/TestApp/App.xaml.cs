﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TestApp.Views;

namespace TestApp {
    public partial class App : Application {
        public App() {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart() {
        }

        protected override void OnSleep() {
        }

        protected override void OnResume() {
        }
    }
}

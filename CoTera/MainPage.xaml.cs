﻿using CoTera.Systems;
using CoTera.ViewModels;

namespace CoTera
{
    public partial class MainPage : ContentPage
    {
        internal static MainViewModel? Instance;
        
        public MainPage()
        {
            InitializeComponent();
            Instance = new MainViewModel();
            BindingContext = Instance;

            DataLoaderSystem.Initialize();
        }

        void OnPreviousClicked(object sender, EventArgs e) => Instance!.ChosenDate = Instance.ChosenDate.AddDays(-1);

        void OnNextClicked(object sender, EventArgs e) => Instance!.ChosenDate = Instance.ChosenDate.AddDays(1);

        void OnOptionsClicked(object sender, EventArgs e) => AppControllerSystem.GoToOptionsAsync();

        void OnRefreshClicked(object sender, EventArgs e) => DataLoaderSystem.RefreshData();
    }
}
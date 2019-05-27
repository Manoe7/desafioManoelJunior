using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Models;
using App1.ViewModels;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace App1.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;
        List<History> StatusHistoryContent { get; set; } = new List<History>();


        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            viewModel.StatusHistoryContent = JsonConvert.DeserializeObject<List<History>>(viewModel.Item.StatusHistory);
            BindingContext = this.viewModel = viewModel;
        }

        public ItemDetailPage()
        {
            InitializeComponent();

            var item = new Item
            {
                Text = "Item 1",
                Description = "This is an item description."
            };

            viewModel = new ItemDetailViewModel(item);
            BindingContext = viewModel;
        }

        public void EditPageCallback(Item item)
        {
            var newViewModel = new ItemDetailViewModel(item);
            newViewModel.StatusHistoryContent = JsonConvert.DeserializeObject<List<History>>(newViewModel.Item.StatusHistory);
            BindingContext = this.viewModel = newViewModel;
        }

        async void Edit_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage(viewModel.Item, this)));
        }
    }
}